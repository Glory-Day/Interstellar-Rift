#if UNITY_EDITOR

using Core.Object.Service;
using GloryDay.Debug;
using GloryDay.Debug.Gizmos;
using UnityEngine;

namespace Core.Object.Module
{
    public class SearchedModuleVisualDebugger : LocalClientBehaviour
    {
        #region LOCAL SERVICE API

        private ModuleSearcher _moduleSearcher;

        #endregion

        public override void Install()
        {
            Console.LogProgress();

            _moduleSearcher = Resolver.GetLocalService<ModuleSearcher>();

            base.Install();
        }

        private void OnDrawGizmosSelected()
        {
            if (_moduleSearcher == null)
            {
                return;
            }

            var radius = _moduleSearcher.Radius;
            var cachedGizmosInformation = _moduleSearcher.CachedGizmosInformation;

            // Draws the search radius boundary.
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, radius);

            // Draws a line toward the found module.
            var color = cachedGizmosInformation.ColorForModule;
            var origin = cachedGizmosInformation.MainPosition;
            var position = cachedGizmosInformation.PositionForModule;
            if (position != null)
            {
                Gizmos.color = color;
                Gizmos.DrawLine(origin, position.Value);
            }

            // Draws lines toward each slot with its attachment state label.
            var style = new GUIStyle { richText = true };
            var builder = new LabelBuilder();
            var count = cachedGizmosInformation.PositionsForSlot.Count;
            for (var i = 0; i < count; i++)
            {
                color = cachedGizmosInformation.ColorsForSlot[i];
                position = cachedGizmosInformation.PositionsForSlot[i];
                Gizmos.color = color;
                Gizmos.DrawLine(origin, position.Value);

                builder.Color = $"#{ColorUtility.ToHtmlStringRGB(color)}";
                builder.FontSize = 12;

                if (color == Color.red)
                {
                    builder.Append("Unavailable");
                }
                else if (color == Color.yellow)
                {
                    builder.Append("Available");
                }
                else
                {
                    builder.Append("Connected");
                }

                UnityEditor.Handles.Label(position.Value, builder.ToString(), style);

                builder.Clear();
            }
        }
    }
}

#endif
