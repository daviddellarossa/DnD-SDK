using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewCartographerTool", menuName = "Game Entities/Equipment/Tools/Artisan Tools/Cartographer Tool")]
    public class CartographerTool : ScriptableObject, ICartographerTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
