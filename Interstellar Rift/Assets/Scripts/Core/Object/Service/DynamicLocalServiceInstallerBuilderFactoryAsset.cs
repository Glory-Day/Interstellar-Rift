using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    [CreateAssetMenu(fileName = "Dynamic Local Service Installer Builder Factory Asset",
                     menuName = "Assets/Services/Local/Dynamic Local Service Installer Builder")]
    public class DynamicLocalServiceInstallerBuilderFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(DynamicLocalServiceInstallerBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new DynamicLocalServiceInstallerBuilder(resolver);
        }
    }
}
