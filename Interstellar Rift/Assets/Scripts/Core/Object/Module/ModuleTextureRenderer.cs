using Core.Object.Service;
using Sirenix.OdinInspector;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that assigns a color based on the module's rank and renders that color onto its texture.
    /// </summary>
    public class ModuleTextureRenderer : LocalServiceBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [Tooltip("The sprite renderers within the module.")]
        [SerializeField] private SpriteRenderer[] spriteRenderers;

        #endregion

        private static readonly int TextureID = Shader.PropertyToID("_MainTex");
        private static readonly int ColorID = Shader.PropertyToID("_Color");

        private MaterialPropertyBlock _block;

        private void Awake()
        {
            Console.LogProgress();

            _block = new MaterialPropertyBlock();
        }

        /// <summary>
        /// Starts applying the assigned color by subscribing to the applicator's color updates.
        /// </summary>
        public void Draw()
        {
            Applicator.OnColorChanged += Apply;
            Applicator.Apply();
        }

        /// <summary>
        /// Stops applying the assigned color by unsubscribing from the applicator's color updates.
        /// </summary>
        public void Cancel()
        {
            Applicator.OnColorChanged -= Apply;
            (Applicator as GradientColorApplicator)?.Cancel();
        }

        /// <summary>
        /// Applies the given color to every sprite renderer's material.
        /// </summary>
        /// <param name="color">The color to apply.</param>
        private void Apply(Color color)
        {
            for (var i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].GetPropertyBlock(_block);

                _block.SetTexture(TextureID, spriteRenderers[i].sprite.texture);
                _block.SetColor(ColorID, color);

                spriteRenderers[i].SetPropertyBlock(_block);
            }
        }

        /// <summary>
        /// The <see cref="IColorApplicator"/> that determines the module's color based on its rank.
        /// </summary>
        public IColorApplicator Applicator { get; set; }
    }
}
