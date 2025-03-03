using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewHerbalismKit", menuName = "Game Entities/Equipment/Tools/Herbalism Kit")]
    public class HerbalismKitTool : ScriptableObject, IHerbalismKitTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
