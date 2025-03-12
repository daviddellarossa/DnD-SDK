using System;
using UnityEditor;
using UnityEngine;

namespace DnD.Editor.Initializer
{
    public static class GameDataInitializer
    {
        
        [MenuItem("D&D Game/Game Data Initializer/Generate Game Data")]
        public static void InitializeGameData()
        {
            Debug.Log("Initializing Game Data");
            try
            {

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
                return;
                // BarbarianClassInitializer.InitializeBarbarianClass();

            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
            Debug.Log("Game Data initialized successfully");
        }

    }
}