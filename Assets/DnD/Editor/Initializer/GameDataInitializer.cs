using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class GameDataInitializer
    {
        
        [MenuItem("D&D Game/Game Data Initializer/Generate Game Data")]
        public static void InitializeGameData()
        {
            DiceInitializer.InitializeDice();
            DamageTypeInitializer.InitializeDamageTypes();
            StorageInitializer.InitializeStorage();
            EquipmentInitializer.InitializeEquipment();
            AbilitiesInitializer.InitializeAbilities();
            LanguagesInitializer.InitializeLanguages();
            WeaponsInitializer.InitializeWeapons();
            ArmoursInitializer.InitializeArmours();
            // BarbarianClassInitializer.InitializeBarbarianClass();
        }

    }
}