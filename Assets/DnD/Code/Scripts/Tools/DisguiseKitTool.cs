using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewDisguiseKit", menuName = "Game Entities/Equipment/Tools/Disguise Kit")]
    public class DisguiseKitTool : ScriptableObject, IDisguiseKitTool
    {
        public string Name;
        public string DisplayText => this.Name;
    }
}
