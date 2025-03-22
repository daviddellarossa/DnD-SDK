using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewForgeryKit", menuName = "Game Entities/Equipment/Tools/Forgery Kit")]
    public class ForgeryKitTool : ScriptableObject, IForgeryKitTool
    {
        public string DisplayName { get; set; }
        public string DisplayDescription { get; set; }
    }
}
