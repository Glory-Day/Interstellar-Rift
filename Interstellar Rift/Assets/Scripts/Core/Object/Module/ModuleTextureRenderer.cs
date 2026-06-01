using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ModuleTextureRenderer : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [SerializeField] private SpriteRenderer spriteRenderer;

        #endregion

        private static readonly int TextureID = Shader.PropertyToID("_MainTex");
        private static readonly int ColorID = Shader.PropertyToID("_Color");

        private MaterialPropertyBlock _block;

        private void Awake()
        {
            _block = new MaterialPropertyBlock();
        }

        public void Apply(IColorProvider provider)
        {
            spriteRenderer.GetPropertyBlock(_block);

            _block.SetTexture(TextureID, spriteRenderer.sprite.texture);
            _block.SetColor(ColorID, provider.Color);

            spriteRenderer.SetPropertyBlock(_block);
        }
    }
}
