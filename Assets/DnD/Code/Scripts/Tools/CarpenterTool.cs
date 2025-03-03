using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewCarpenterTool", menuName = "Game Entities/Equipment/Tools/Artisan Tools/Carpenter Tool")]
    public class CarpenterTool : ScriptableObject, ICarpenterTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
