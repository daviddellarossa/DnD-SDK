﻿using System.Collections.Generic;
using DnD.Code.Scripts;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.FeatureProperties;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Weapons;
using Infrastructure.Helpers;
using UnityEditor;
using UnityEngine;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public abstract class BaseClassInitializer
    {
        protected Level InitializeLevel(string levelName, int levelNum, int proficiencyBonus, IClassFeatureStats classFeatureStats, string assetPath)
        {
            var level = ScriptableObjectHelper.CreateScriptableObject<Level>($"{levelName}.{levelNum:00}", assetPath);
            level.DisplayName = $"{levelName}.{levelNum:00}";
            level.DisplayDescription = $"{levelName}.{levelNum:00}.{NameHelper.Naming.Description}";
            level.LevelNum = levelNum;
            level.ProficiencyBonus = proficiencyBonus;
            level.ClassFeatureStats = classFeatureStats;

            EditorUtility.SetDirty(level);
            
            return level;
        }
    }
    public abstract class ClassInitializer : BaseClassInitializer
    {
        private const int NumOfLevels = 20;
        
        protected abstract string ClassName { get; }
        protected abstract string ClassPath { get; }
        protected abstract string ClassStartingEquipmentPath { get; }
        protected abstract string ClassLevelsPath { get; }
        protected abstract string ClassSubClassesPath { get; }

        protected Die[] Dice => DiceInitializer.GetAllDice();
        protected Ability[] Abilities => AbilitiesInitializer.GetAllAbilities();
        protected Skill[] Skills => AbilitiesInitializer.GetAllSkills();
        protected WeaponType[] WeaponTypes => WeaponsInitializer.GetAllWeaponTypes();
        protected BaseArmourType[] ArmourTypes => ArmoursInitializer.GetAllArmourTypes();
        
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Classes Data")]
        public static void InitializeClasses()
        {
            FileSystemHelper.EnsureFolderExists(PathHelper.Classes.ClassesPath);
            
            var barbarianInitializer = new ClassBarbarianInitializer();
            barbarianInitializer.InitializeBarbarian();
        }

        protected void InitializeClass()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                FileSystemHelper.EnsureFolderExists(ClassPath);
                FileSystemHelper.EnsureFolderExists(ClassLevelsPath);
                FileSystemHelper.EnsureFolderExists(ClassStartingEquipmentPath);
                FileSystemHelper.EnsureFolderExists(ClassSubClassesPath);
                
                var classInstance = CreateClassInstance();

                // Create Starting Equipment
                var startingEquipmentArray = InitializeStartingEquipment();

                classInstance.StartingEquipmentOptions.AddRange(startingEquipmentArray);
                
                // Create Levels
                var levelsArray = InitializeLevels(classInstance);
                for (int i = 0; i < NumOfLevels; i++)
                {
                    classInstance.Levels[i] = levelsArray[i];
                }
                
                // Create Subclasses
                var subclasses = InitializeSubClasses();
            
                foreach (var subclass in subclasses)
                {
                    classInstance.SubClasses.Add(subclass);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        protected abstract IEnumerable<SubClass> InitializeSubClasses();

        protected abstract Class CreateClassInstance();

        protected StartingEquipment CreateStartingEquipmentOption(string optionName, string assetPath, StartingEquipment.EquipmentWithAmount[] itemsWithAmount)
        {
            var startingEquipment = ScriptableObjectHelper.CreateScriptableObject<StartingEquipment>(optionName, assetPath);

            startingEquipment.DisplayName = $"{nameof(NameHelper.StartingEquipmentOptions)}.{optionName}";
            startingEquipment.DisplayDescription = $"{nameof(NameHelper.StartingEquipmentOptions)}.{optionName}.{NameHelper.Naming.Description}";

            foreach (var item in itemsWithAmount)
            {
                startingEquipment.EquipmentsWithAmountList.Add(item);
            }
            EditorUtility.SetDirty(startingEquipment);
            
            return startingEquipment;
        }
        
        protected Level[] InitializeLevels(Object parent)
        {
            var levels = new List<Level>
            {
                InitializeLevel01(),
                InitializeLevel02(),
                InitializeLevel03(),
                InitializeLevel04(),
                InitializeLevel05(),
                InitializeLevel06(),
                InitializeLevel07(),
                InitializeLevel08(),
                InitializeLevel09(),
                InitializeLevel10(),
                InitializeLevel11(),
                InitializeLevel12(),
                InitializeLevel13(),
                InitializeLevel14(),
                InitializeLevel15(),
                InitializeLevel16(),
                InitializeLevel17(),
                InitializeLevel18(),
                InitializeLevel19(),
                InitializeLevel20()
            };

            return levels.ToArray();
        }
        
        protected StartingEquipment[] InitializeStartingEquipment()
        {
            var weapons = WeaponsInitializer.GetAllWeapons();
            var coins = EquipmentInitializer.GetAllCoinValues();
            
            var optionA = InitializeStartingEquipmentOptionA(weapons, coins);
            
            var optionB = InitializeStartingEquipmentOptionB(weapons, coins);
            
            return new [] {optionA, optionB};
        }

        protected abstract StartingEquipment InitializeStartingEquipmentOptionA(Weapon[] weapons, CoinValue[] coins);

        protected abstract StartingEquipment InitializeStartingEquipmentOptionB(Weapon[] weapons, CoinValue[] coins);

        
        protected abstract Level InitializeLevel01();
        protected abstract Level InitializeLevel02();
        protected abstract Level InitializeLevel03();
        protected abstract Level InitializeLevel04();
        protected abstract Level InitializeLevel05();
        protected abstract Level InitializeLevel06();
        protected abstract Level InitializeLevel07();
        protected abstract Level InitializeLevel08();
        protected abstract Level InitializeLevel09();
        protected abstract Level InitializeLevel10();
        protected abstract Level InitializeLevel11();
        protected abstract Level InitializeLevel12();
        protected abstract Level InitializeLevel13();
        protected abstract Level InitializeLevel14();
        protected abstract Level InitializeLevel15();
        protected abstract Level InitializeLevel16();
        protected abstract Level InitializeLevel17();
        protected abstract Level InitializeLevel18();
        protected abstract Level InitializeLevel19();
        protected abstract Level InitializeLevel20();
        
        protected abstract class SubClassInitializer : BaseClassInitializer
        {
            public abstract string ClassName { get; }
            public abstract string SubClassName { get; }
            public abstract string SubClassPath { get; }
            public abstract string SubClassLevelsPath { get; }
            
            protected abstract Level InitializeLevel03();
            protected abstract Level InitializeLevel06();
            protected abstract Level InitializeLevel10();
            protected abstract Level InitializeLevel14();

            
            public SubClass InitializeSubClass() 
            {
                FileSystemHelper.EnsureFolderExists(SubClassPath);
                FileSystemHelper.EnsureFolderExists(SubClassLevelsPath);

                var subClass = ScriptableObjectHelper.CreateScriptableObject<SubClass>(SubClassName, SubClassPath);
                subClass.DisplayName = $"{ClassName}.{SubClassName}";
                subClass.DisplayDescription = $"{ClassName}.{SubClassName}.{NameHelper.Naming.Description}";
                
                subClass.Level03 = InitializeLevel03();
                subClass.Level06 = InitializeLevel06();
                subClass.Level10 = InitializeLevel10();
                subClass.Level14 = InitializeLevel14();
            
                EditorUtility.SetDirty(subClass);
                return subClass;
            }
        }
    }
}