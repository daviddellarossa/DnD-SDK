using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewGamingSet", menuName = "Game Entities/Equipment/Tools/Gaming Set")]
    public class GamingSetTool : ScriptableObject, IGamingSetTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
