using Core.Object.Service;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ElectricArcVisualEffect : EnableableLocalService
    {
        #region LOCAL SERVICE API

        private ModuleConnectionPathCacher _moduleConnectionPathCacher;

        #endregion

        private GameObject _gameObject;
        private LineRenderer _lineRenderer;

        private GameObject[] _sparkEffectObjects;

        private readonly int _segments;
        private Vector3[] _positions;

        public ElectricArcVisualEffect(GameObject gameObject, GameObject[] sparkEffectObjects, LineRenderer lineRenderer,
                                       int segments,
                                       ModuleConnectionPathCacher moduleConnectionPathCacher)
        {
            Console.LogProgress();

            _gameObject = gameObject;
            _sparkEffectObjects = sparkEffectObjects;
            _lineRenderer = lineRenderer;

            _segments = Mathf.Max(segments, 1);
            _positions = new Vector3[_segments + 1];

            _lineRenderer.useWorldSpace = true;
            _lineRenderer.positionCount = _positions.Length;

            _moduleConnectionPathCacher = moduleConnectionPathCacher;

            Disable();
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Console.LogProgress();

            _gameObject.SetActive(false);
            _gameObject = null;
            _sparkEffectObjects = null;
            _lineRenderer = null;

            _positions = null;

            _moduleConnectionPathCacher = null;
        }

        protected override void Enable_Internal()
        {
            Console.LogProgress();

            _gameObject.SetActive(true);
            _sparkEffectObjects[0].SetActive(true);
            _sparkEffectObjects[1].SetActive(true);
        }

        protected override void Disable_Internal()
        {
            Console.LogProgress();

            _gameObject.SetActive(false);
            _sparkEffectObjects[0].SetActive(false);
            _sparkEffectObjects[1].SetActive(false);
        }

        public void UpdatePoints()
        {
            if (_moduleConnectionPathCacher.IsCached is false)
            {
                return;
            }

            var path = _moduleConnectionPathCacher.Fetch();
            for (var i = 0; i <= _segments; i++)
            {
                _positions[i] = path.Evaluate((float)i / _segments);
            }

            _lineRenderer.SetPositions(_positions);

            _sparkEffectObjects[0].transform.position = _positions[0];
            _sparkEffectObjects[1].transform.position = _positions[_segments];
        }
    }
}
