using Core.Utility.Extension;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleTestbed : MonoBehaviour
    {
        #region SERIALIZABLE PROPERTY API

        [Title("Data")]
        [ReadOnly]
        [SerializeField] private ModuleRank rank;
        [SerializeField] private ModuleDataTable table;

        [Title("References")]
        [SerializeField] private ModuleTextureRenderer[] renderers;

        #endregion

        private void Start()
        {
            Console.LogProgress();

            for (var i = 0; i < renderers.Length; i++)
            {
                renderers[i].Apply(rank.ToColorProvider());
            }
        }
    }
}
