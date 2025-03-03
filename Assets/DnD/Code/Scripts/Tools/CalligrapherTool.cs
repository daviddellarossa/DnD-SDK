using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewCalligrapherTool", menuName = "Game Entities/Equipment/Tools/Artisan Tools/Calligrapher Tool")]
    public class CalligrapherTool : ScriptableObject, ICalligrapherTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
