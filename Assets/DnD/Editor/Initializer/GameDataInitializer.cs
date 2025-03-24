using System;
using DnD.Code.Scripts.Helpers.PathHelper;
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
                Common.EnsureFolderExists(PathHelper.FolderPath, true);

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
                SpexInitializer.InitializeSpecies();
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