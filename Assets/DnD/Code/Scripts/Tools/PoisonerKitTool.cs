using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewPoisonerKit", menuName = "Game Entities/Equipment/Tools/Poisoner Kit")]
    public class PoisonerKitTool : ScriptableObject, IPoisonerKitTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
