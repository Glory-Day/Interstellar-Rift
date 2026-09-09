using Core.Test;
using UnityEngine;
using VContainer;

namespace Core.Object.Module
{
    public readonly struct ModuleBootstrapConfiguration
    {
        public ModuleBootstrapConfiguration(IObjectResolver resolver,
                                      GameObject clone,
                                      ModuleSpawnSpecification specification)
        {
            ObjectResolver = resolver;

            Clone = clone;
            Name = clone.name[..^14];

            Specification = specification;
        }

        public IObjectResolver ObjectResolver { get; }

        public GameObject Clone { get; }

        public ModuleSpawnSpecification Specification { get; }

        public string Name { get; }
    }
}
