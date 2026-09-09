using Core.Object.Service;
using Core.Utility;
using Core.Utility.Exception;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ElectricArcVisualEffectBuilder : LocalService, IBuildable<ElectricArcVisualEffect>
    {
        private const int Minimum = 1;
        private const int Maximum = 10000;

        private Transform[] _transforms;

        private readonly float _amount;

        private readonly float _speed;
        private readonly float _range;

        private readonly float _seed;

        private GameObject _gameObject;

        public ElectricArcVisualEffectBuilder(float amount,
                                              float speed, float range,
                                              ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            _amount = amount;

            _speed = speed;
            _range = range;

            _seed = new System.Random().Next(Minimum, Maximum);
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _transforms = null;
        }

        public ElectricArcVisualEffectBuilder WithTransforms(Transform a, Transform b, Transform c, Transform d)
        {
            _transforms = new[] { a, b, c, d };

            return this;
        }

        public ElectricArcVisualEffectBuilder WithGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;

            return this;
        }

        public ElectricArcVisualEffect Build()
        {
            Console.LogProgress();

            if (_transforms == null)
            {
                throw new MissingRequiredComponentException(nameof(Transform));
            }

            if (_gameObject == null)
            {
                throw new MissingRequiredComponentException(nameof(GameObject));
            }

            Console.LogSuccess($"{nameof(ElectricArcVisualEffect).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new ElectricArcVisualEffect(_transforms, _amount, _speed, _range, _seed, _gameObject, Resolver);
        }
    }
}
