using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class GameDataInitializer
    {
        
        [MenuItem("D&D Game/Game Data Initializer/Generate Game Data")]
        public static void InitializeGameData()
        {
            BarbarianClassInitializer.InitializeBarbarianClass();
        }

    }
}