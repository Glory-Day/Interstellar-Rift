using Core.Object.Service;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleTextureDrawerBuilder : LocalService, IBuildable<ModuleTextureDrawer>
    {
        private SpriteRenderer[] _renderers;

        private int _textureID;
        private int _colorID;

        public ModuleTextureDrawerBuilder(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();
        }

        public ModuleTextureDrawerBuilder WithSpriteRenderers(SpriteRenderer[] renderers)
        {
            _renderers = renderers;

            return this;
        }

        public ModuleTextureDrawerBuilder WithShaderIDs(int textureID, int colorID)
        {
            _textureID = textureID;
            _colorID = colorID;

            return this;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _renderers = null;
        }

        public ModuleTextureDrawer Build()
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleTextureDrawer).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new ModuleTextureDrawer(_renderers, _textureID, _colorID, Resolver);
        }
    }
}
