using UnityEngine;

namespace Core.Object.Service
{
    public abstract class LocalServiceInstalledVisualDebugger : LocalServiceInstallationBehaviour
    {
#if UNITY_EDITOR

        #region SERIALIZABLE FIELD API

        [SerializeField] private bool isDebugging;

        #endregion

        private void OnDrawGizmos()
        {
            if (isDebugging is false)
            {
                return;
            }

            DrawGizmos();
        }

        protected abstract void DrawGizmos();

#endif
    }
}
