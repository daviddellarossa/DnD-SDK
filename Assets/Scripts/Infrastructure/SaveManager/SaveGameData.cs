using System.Collections.Generic;

namespace Infrastructure
{
    [System.Serializable]
    public class SaveGameData
    {
        public string characterName;
        public int level;
        public int health;
        public List<string> inventoryItems;
    }
}