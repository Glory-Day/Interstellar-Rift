using Core.Object.Service;
using Core.Utility;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ElectricArcVisualEffectBuilder : LocalServiceBuilder<ElectricArcVisualEffect>
    {
        #region LOCAL SERVICE API

        private ModuleConnectionPathCacher _moduleConnectionPathCacher;

        #endregion

        private GameObject _gameObject;
        private GameObject[] _sparkEffectObjects;
        private LineRenderer _lineRenderer;

        private readonly int _segments;

        public ElectricArcVisualEffectBuilder(int segments,
                                              ModuleConnectionPathCacher moduleConnectionPathCacher)
        {
            Console.LogProgress();

            _segments = segments;

            _moduleConnectionPathCacher = moduleConnectionPathCacher;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            base.Dispose();

            _gameObject = null;
            _sparkEffectObjects = null;
            _lineRenderer = null;

            _moduleConnectionPathCacher = null;
        }

        public ElectricArcVisualEffectBuilder WithGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;

            return this;
        }

        public ElectricArcVisualEffectBuilder WithLineRenderer(LineRenderer lineRenderer)
        {
            _lineRenderer = lineRenderer;

            return this;
        }

        public ElectricArcVisualEffectBuilder WithSparkEffectObjects(GameObject[] sparkEffectObjects)
        {
            _sparkEffectObjects = sparkEffectObjects;

            return this;
        }

        protected override void Validate()
        {
            if (_gameObject == null)
            {
                throw new MissingBuildComponentException(typeof(ElectricArcVisualEffect), "GameObject");
            }

            if (_sparkEffectObjects == null)
            {
                throw new MissingBuildComponentException(typeof(ElectricArcVisualEffect), "GameObjects");
            }

            if (_lineRenderer == null)
            {
                throw new MissingBuildComponentException(typeof(ElectricArcVisualEffect), "LineRenderer");
            }
        }

        protected override ElectricArcVisualEffect Create()
        {
            Console.LogProgress();

            return new ElectricArcVisualEffect(_gameObject, _sparkEffectObjects, _lineRenderer,
                                               _segments,
                                               _moduleConnectionPathCacher);
        }
    }
}
