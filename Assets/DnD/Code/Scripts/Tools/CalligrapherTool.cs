using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewCalligrapherTool", menuName = "Game Entities/Equipment/Tools/Artisan Tools/Calligrapher Tool")]
    public class CalligrapherTool : ScriptableObject, ICalligrapherTool
    {
        public string DisplayName { get; set; }
        public string DisplayDescription { get; set; }
    }
}
