using System.Collections.Generic;
using Core.Object.Service;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleSocketInitializer : LocalClientBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [DictionaryDrawerSettings(KeyLabel = "Direction", ValueLabel = "Slot")]
        [Tooltip("The list of slots that provide the connection functionality needed to connect a module.")]
        [SerializeField] private List<Slot> slots;
        [Tooltip("The slot used for this module to connect to another module.")]
        [SerializeField] private Slot joint;

        #endregion

        #region LOCAL SERVICE API

        private ModuleSocket _moduleSocket;

        #endregion

        public override void Install()
        {
            Console.LogProgress();

            _moduleSocket = Resolver.GetLocalService<ModuleSocketBuilder>()
                                       .WithSlots(slots)
                                       .WithJoint(joint)
                                       .Build();

            var installer = Resolver.GetLocalService<DynamicLocalServiceInstaller>();
            installer.Install(_moduleSocket);

            base.Install();
        }

        public void OnDestroy()
        {
            Console.LogProgress();

            _moduleSocket.Dispose();
            _moduleSocket = null;
        }
    }
}
