using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Drag Movement Factory Asset",
                     menuName = "Assets/Services/Local/Module Drag Movement")]
    public class ModuleDragMovementFactoryAsset : LocalServiceFactoryAsset
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [Tooltip("Rotation speed. If 0, the module rotates instantly instead of smoothly.")]
        [SerializeField] [Range(0f, 10f)] private float speed = 5f;

        #endregion

        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleDragMovement).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleDragMovement(speed, resolver);
        }
    }
}
