using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewCartographerTool", menuName = "Game Entities/Equipment/Tools/Artisan Tools/Cartographer Tool")]
    public class CartographerTool : ScriptableObject, ICartographerTool
    {
        public string DisplayName { get; set; }
        public string DisplayDescription { get; set; }
    }
}
