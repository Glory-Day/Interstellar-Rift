using Sirenix.OdinInspector;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleTextureRenderer : ModuleServiceBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
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

        public void Draw()
        {
            Applicator.OnColorChanged += Apply;
            Applicator.Apply();
        }

        public void Cancel()
        {
            Applicator.OnColorChanged -= Apply;
            (Applicator as GradientColorApplicator)?.Cancel();
        }

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

        public IColorApplicator Applicator { get; set; }
    }
}
