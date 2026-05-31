using Core.Utility.Extension;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleTestbed : MonoBehaviour
    {
        #region SERIALIZABLE PROPERTY API

        [field: Title("Data")]
        [field: SerializeField] private ModuleData Data { get; set; }

        [field: Title("References")]
        [field: SerializeField] private ModuleTextureRenderer Renderer { get; set; }

        #endregion

        private void Start()
        {
            Console.LogProgress();

            Renderer.Apply(Data.Rank.ToColorProvider());
        }
    }
}
