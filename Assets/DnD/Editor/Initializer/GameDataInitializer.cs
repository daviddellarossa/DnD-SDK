using System;
using UnityEditor;
using UnityEngine;

namespace DnD.Editor.Initializer
{
    public static class GameDataInitializer
    {
        [MenuItem("D&D Game/Game Data Initializer/Initialize Game Data")]
        public static void InitializeGameData()
        {
            Debug.Log("Initializing Game Data");
            try
            {
                Common.EnsureFolderExists(Common.FolderPath, true);

                DiceInitializer.InitializeDice();
                DamageTypeInitializer.InitializeDamageTypes();
                CreatureTypesInitializer.InitializeCreatureTypes();
                StorageInitializer.InitializeStorage();
                EquipmentInitializer.InitializeEquipment();
                AbilitiesInitializer.InitializeAbilities();
                LanguagesInitializer.InitializeLanguages();
                FeatsInitializer.InitializeFeatsData();
                BackgroundInitializer.InitializeBackgrounds();
                WeaponsInitializer.InitializeWeapons();
                ArmoursInitializer.InitializeArmours();
                SpeciesInitializer.InitializeSpecies();
                ClassInitializer.InitializeClasses();
                
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
            Debug.Log("Game Data initialized successfully");
        }

    }
}