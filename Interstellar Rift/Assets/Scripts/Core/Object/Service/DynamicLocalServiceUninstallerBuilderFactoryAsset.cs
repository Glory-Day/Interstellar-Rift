using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    [CreateAssetMenu(fileName = "Dynamic Local Service Uninstaller Builder Factory Asset",
                     menuName = "Assets/Services/Local/Dynamic Local Service Uninstaller Builder")]
    public class DynamicLocalServiceUninstallerBuilderFactoryAsset : LocalServiceBuilderFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(DynamicLocalServiceUninstallerBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new DynamicLocalServiceUninstallerBuilder();
        }

        public override InstallationMode Mode => InstallationMode.Manual;
    }
}
