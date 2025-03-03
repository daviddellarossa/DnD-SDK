using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewNavigatorTool", menuName = "Game Entities/Equipment/Tools/Navigator Tool")]
    public class NavigatorTool : ScriptableObject, INavigatorTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
