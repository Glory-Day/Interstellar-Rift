using System;
using System.Threading;
using Core.Object.Service;
using Core.Utility.Mathematics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleConnectionMovement : LocalService
    {
        private const float Epsilon = 1e-6f;

        #region LOCAL SERVICE API

        private ModuleConnectionPathCacher _moduleConnectionPathCacher;
        private ModuleSocket _moduleSocket;

        private Transform _transform;

        #endregion

        private readonly float _speed;
        private readonly float _duration;

        private readonly Alignment _alignment;

        private AnimationCurve _easing;

        public ModuleConnectionMovement(float speed, float duration,
                                        Alignment alignment,
                                        AnimationCurve easing,
                                        ModuleConnectionPathCacher moduleConnectionPathCacher, ModuleSocket moduleSocket,
                                        Transform transform)
        {
            Console.LogProgress();

            _speed = speed;
            _duration = duration;

            _alignment = alignment;

            _easing = easing;

            _moduleConnectionPathCacher = moduleConnectionPathCacher;
            _moduleSocket = moduleSocket;

            _transform = transform;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _easing = null;

            _moduleConnectionPathCacher = null;
            _moduleSocket = null;

            _transform = null;
        }

        public void Rotate(SearchedSlotResult target)
        {
            if (_moduleConnectionPathCacher.IsCached is false)
            {
                return;
            }

            var joint = _moduleSocket.Joint.transform;
            var slot = target.Slot.transform;

            // Head-on and tangent directions cancelled out; keep the previous rotation.
            var direction = GetDirection(joint, slot);
            if (direction.sqrMagnitude < Epsilon)
            {
                return;
            }

            var local = Quaternion.Inverse(_transform.rotation) * joint.rotation;
            var facing = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.up, direction));

            var a = _transform.rotation;
            var b = facing * Quaternion.Inverse(local);

            // Frame-rate independent smoothing; the weight approaches 1 as speed grows.
            var weight = _speed > 0f ? 1f - Mathf.Exp(-_speed * Time.deltaTime) : 1f;

            _transform.rotation = Quaternion.Slerp(a, b, weight);
        }

        private Vector2 GetDirection(Transform from, Transform to)
        {
            var a = from.position;
            var b = to.position;

            Vector2 outward = to.up;
            var direction = -outward;
            var axial = Vector2.Dot(a - b, outward);
            var weight = Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(_alignment.near, _alignment.far, axial));
            if (weight <= 0f)
            {
                return direction;
            }

            var path = _moduleConnectionPathCacher.Fetch();
            var tangent = path.Tangent(0f);
            if (tangent.sqrMagnitude < Epsilon)
            {
                return direction;
            }

            return Vector2.Lerp(direction, tangent.normalized, weight);
        }

        public async UniTask<bool> Move(SearchedSlotResult target, CancellationToken token)
        {
            if (_moduleConnectionPathCacher.IsCached is false)
            {
                return false;
            }

            var joint = _moduleSocket.Joint;
            var slot = target.Slot;
            if (slot == null)
            {
                return false;
            }

            var path = _moduleConnectionPathCacher.Fetch().ToLocalSpace(slot.transform);
            var pose = new Pose(Quaternion.Inverse(slot.transform.rotation) * _transform.rotation,
                                Quaternion.Inverse(_transform.rotation) * joint.transform.rotation);

            var snapshot = new Snapshot(slot.transform, joint.transform, path, pose);

            var elapsed = 0f;
            while (elapsed < _duration)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);

                // The target slot may be destroyed while the module is still moving.
                if (target.Slot == null)
                {
                    return false;
                }

                elapsed += Time.deltaTime;

                Place(snapshot, elapsed / _duration);
            }

            Place(snapshot, 1f);

            return true;
        }

        private void Place(Snapshot snapshot, float ratio)
        {
            var facing = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.up, -(Vector2)snapshot.Slot.up));

            var from = snapshot.Slot.rotation * snapshot.Pose.Slot;
            var to = facing * Quaternion.Inverse(snapshot.Pose.Joint);
            var time = _easing.Evaluate(Mathf.Clamp01(ratio));

            _transform.rotation = Quaternion.Slerp(from, to, time);

            // Re-read the joint offset after rotating, so the joint (not the pivot) lands on the path.
            var point = snapshot.Slot.TransformPoint(snapshot.Path.Evaluate(time));
            _transform.position = point - (snapshot.Joint.position - _transform.position);
        }

        #region NESTED STRUCTURE API

        private readonly struct Pose
        {
            public readonly Quaternion Slot;  // The module's rotation relative to the target slot.
            public readonly Quaternion Joint; // The joint's rotation relative to the module.

            public Pose(Quaternion slot, Quaternion joint)
            {
                Slot = slot;
                Joint = joint;
            }
        }

        private readonly struct Snapshot
        {
            public readonly Transform Slot;
            public readonly Transform Joint;

            public readonly BezierCurve Path;
            public readonly Pose Pose;

            public Snapshot(Transform slot, Transform joint,
                            BezierCurve path, Pose pose)
            {
                Path = path;
                Pose = pose;

                Slot = slot;
                Joint = joint;
            }
        }

        #endregion

        #region NESTED SERIALIZABLE STRUCTURE API

        [Serializable]
        public struct Alignment
        {
            [SerializeField] public float near;
            [SerializeField] public float far;
        }

        #endregion
    }
}
