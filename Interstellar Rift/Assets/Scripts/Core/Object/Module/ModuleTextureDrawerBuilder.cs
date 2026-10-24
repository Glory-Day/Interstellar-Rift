using Core.Object.Service;
using Core.Utility;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Core.Object.Module
{
    public class ModuleTextureDrawerBuilder : LocalServiceBuilder<ModuleTextureDrawer>
    {
        #region LOCAL SERVICE API

        private ColorApplicator _colorApplicator;

        #endregion

        private SpriteRenderer[] _renderers;

        private int? _textureID;
        private int? _colorID;

        public ModuleTextureDrawerBuilder(ColorApplicator colorApplicator)
        {
            Console.LogProgress();

            _colorApplicator = colorApplicator;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            base.Dispose();

            _colorApplicator = null;

            _renderers = null;
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

        protected override void Validate()
        {
            if (_renderers == null)
            {
                throw new MissingBuildComponentException(typeof(ModuleTextureDrawer), "Sprite Renderers");
            }

            if (_textureID == null || _colorID == null)
            {
                throw new MissingBuildComponentException(typeof(ModuleTextureDrawer), "Shader IDs");
            }
        }

        protected override ModuleTextureDrawer Create()
        {
            Console.LogProgress();

            var textureID = _textureID ?? 0;
            var colorID = _colorID ?? 0;
            var result = new ModuleTextureDrawer(_renderers, textureID, colorID, _colorApplicator);

            return result;
        }
    }
}
