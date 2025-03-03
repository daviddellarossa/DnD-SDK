using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewForgeryKit", menuName = "Game Entities/Equipment/Tools/Forgery Kit")]
    public class ForgeryKitTool : ScriptableObject, IForgeryKitTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
