using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewCalligrapherTool", menuName = "Game Entities/Equipment/Tools/Artisan Tools/Calligrapher Tool")]
    public class CalligrapherTool : Tool, ICalligrapherTool
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string DisplayDescription
        {
            get => displayDescription;
            set => displayDescription = value;
        }
    }
}
