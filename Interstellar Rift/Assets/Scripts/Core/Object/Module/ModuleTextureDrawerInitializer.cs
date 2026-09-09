using Core.Object.Service;
using Sirenix.OdinInspector;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that assigns a color based on the module's rank and renders that color onto its texture.
    /// </summary>
    public class ModuleTextureDrawerInitializer : LocalClientBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [Tooltip("The sprite renderers within the module.")]
        [SerializeField] private SpriteRenderer[] spriteRenderers;

        #endregion

        public override void Install()
        {
            Console.LogProgress();

            var drawer = Resolver.GetLocalService<ModuleTextureDrawerBuilder>()
                                 .WithSpriteRenderers(spriteRenderers)
                                 .WithShaderIDs(Shader.PropertyToID("_MainTex"), Shader.PropertyToID("_Color"))
                                 .Build();

            var installer = Resolver.GetLocalService<DynamicLocalServiceInstaller>();
            installer.Install(drawer);

            base.Install();
        }
    }
}
