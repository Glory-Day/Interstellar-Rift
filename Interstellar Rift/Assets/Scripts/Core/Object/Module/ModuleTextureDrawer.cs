using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleTextureDrawer : LocalService
    {
        #region LOCAL SERVICE API

        private IColorApplicator _colorApplicator;

        #endregion

        private readonly MaterialPropertyBlock _block = new MaterialPropertyBlock();

        private readonly int _textureID;
        private readonly int _colorID;

        private SpriteRenderer[] _renderers;

        public ModuleTextureDrawer(SpriteRenderer[] renderers,
                                   int textureID, int colorID,
                                   ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            _renderers = renderers;

            _textureID = textureID;
            _colorID = colorID;

            _colorApplicator = resolver.GetLocalService<ColorApplicator>();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _renderers = null;

            _colorApplicator = null;
        }

        /// <summary>
        /// Starts applying the assigned color by subscribing to the applicator's color updates.
        /// </summary>
        public void Draw()
        {
            Console.LogProgress();

            _colorApplicator.OnColorChanged += Apply;
            _colorApplicator.Apply();
        }

        /// <summary>
        /// Stops applying the assigned color by unsubscribing from the applicator's color updates.
        /// </summary>
        public void Cancel()
        {
            Console.LogProgress();

            _colorApplicator.OnColorChanged -= Apply;
            (_colorApplicator as GradientColorApplicator)?.Cancel();
        }

        /// <summary>
        /// Applies the given color to every sprite renderer's material.
        /// </summary>
        /// <param name="color">The color to apply.</param>
        private void Apply(Color color)
        {
            for (var i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].GetPropertyBlock(_block);

                _block.SetTexture(_textureID, _renderers[i].sprite.texture);
                _block.SetColor(_colorID, color);

                _renderers[i].SetPropertyBlock(_block);
            }
        }
    }
}
