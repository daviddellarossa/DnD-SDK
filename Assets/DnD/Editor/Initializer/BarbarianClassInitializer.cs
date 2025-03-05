using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Characters.Classes;
using DnD.Code.Scripts.Characters.Classes.Barbarian.ClassFeatures;
using DnD.Code.Scripts.Characters.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Characters.Classes.ClassFeatures;
using DnD.Code.Scripts.Characters.Classes.FeatureProperties;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Weapons;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DnD.Editor.Initializer
{
    public static class BarbarianClassInitializer
    {
        public static readonly string BarbarianPath = $"{Common.ClassesPath}/Barbarian_test";
        public static readonly string BarbarianStartingEquipmentPath = $"{BarbarianPath}/Starting Equipment";
        public static readonly string BarbarianLevelsPath = $"{BarbarianPath}/Levels";

        [MenuItem("D&D Game/Game Data Initializer/Generate Barbarian Data")]
        public static void InitializeBarbarianClass()
        {
            Common.EnsureFolderExists(BarbarianPath);
            
            // Create instance
            var barbarian = Common.CreateScriptableObject<Class>(NameHelper.Classes.Barbarian, BarbarianPath);
            
            // Create Starting Equipment
            var startingEquipmentArray = InitializeStartingEquipment(barbarian);
            
            barbarian.StartingEquipmentOptions.AddRange(startingEquipmentArray);
            
            // Create Levels
            var levelsArray = InitializeLevels(barbarian);
            for (int i = 0; i < Classes.ClassLevels; i++)
            {
                barbarian.Levels[i] = levelsArray[i];
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
        }

        public static StartingEquipment[] InitializeStartingEquipment(Object parent)
        {
            var weapons = WeaponsInitializer.GetAllWeapons();
            var coins = EquipmentInitializer.GetAllCoinValues();
         
            Common.EnsureFolderExists(BarbarianStartingEquipmentPath);
            
            var optionA = Classes.CreateStartingEquipmentOption(
                NameHelper.StartingEquipmentOptions.OptionA,
                BarbarianStartingEquipmentPath,
                new StartingEquipment.ItemWithAmount[]
                {
                    new StartingEquipment.ItemWithAmount()
                    {
                        Item = weapons.Single(w => w.name == NameHelper.Weapons.Greataxe),
                        Amount = 1
                    },
                    new StartingEquipment.ItemWithAmount()
                    {
                        Item = weapons.Single(w => w.name == NameHelper.Weapons.Handaxe),
                        Amount = 4
                    },
                    
                    // TODO: Items missing here
                    
                    new StartingEquipment.ItemWithAmount()
                    {
                        Item = coins.Single(w => w.name == NameHelper.CoinValues.GoldPiece),
                        Amount = 15
                    }
                });
            
            var optionB = Classes.CreateStartingEquipmentOption(
                NameHelper.StartingEquipmentOptions.OptionB,
                BarbarianStartingEquipmentPath,
                new StartingEquipment.ItemWithAmount[]
                {
                    new StartingEquipment.ItemWithAmount()
                    {
                        Item = coins.Single(w => w.name == NameHelper.CoinValues.GoldPiece),
                        Amount = 75
                    }
                });
            
            return new [] {optionA, optionB};
        }
        

        public static Level[] InitializeLevels(Object parent)
        {
            Common.EnsureFolderExists(BarbarianLevelsPath);

            List<Level> levels = new List<Level>();

            string levelName = "Level";
            int counter = 0;
            
            // Level 01
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                1,
                2,
                new BarbarianFP()
                {
                    rages = 2,
                    rageDamage = 2,
                    weaponMastery = 2
                },
                BarbarianLevelsPath);

            
            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<Rage>($"{nameof(Rage)}", level),
                Common.CreateScriptableObjectAndAddToObject<UnarmouredDefense>($"{nameof(UnarmouredDefense)}", level),
                Common.CreateScriptableObjectAndAddToObject<WeaponMastery>($"{nameof(WeaponMastery)}", level),
            });
            
            levels.Add(level);

            // Level 02
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                2,
                2,
                new BarbarianFP()
                {
                    rages = 2,
                    rageDamage = 2,
                    weaponMastery = 2
                },
                BarbarianLevelsPath);
            
            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<DangerSense>($"{nameof(DangerSense)}", level),
                Common.CreateScriptableObjectAndAddToObject<RecklessAttack>($"{nameof(RecklessAttack)}", level),

            });
            
            levels.Add(level);
            
            // Level 03
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                3,
                2,
                new BarbarianFP()
                {
                    rages = 3,
                    rageDamage = 2,
                    weaponMastery = 2
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<BarbarianSubclass>($"{nameof(BarbarianSubclass)}", level),
                Common.CreateScriptableObjectAndAddToObject<PrimalKnowledge>($"{nameof(PrimalKnowledge)}", level),

            });
            
            levels.Add(level);
            
            // Level 04
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                4,
                2,
                new BarbarianFP()
                {
                    rages = 3,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>($"{nameof(AbilityScoreImprovement)}", level),

            });
            
            levels.Add(level);
            
            // Level 05
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                5,
                3,
                new BarbarianFP()
                {
                    rages = 3,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<ExtraAttack>($"{nameof(ExtraAttack)}", level),
                Common.CreateScriptableObjectAndAddToObject<FastMovement>($"{nameof(FastMovement)}", level),

            });
            
            levels.Add(level);
            
            // Level 06
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                6,
                3,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<SubclassFeature>($"{nameof(SubclassFeature)}", level),

            });
            
            levels.Add(level);
            
            // Level 07
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                7,
                3,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<FeralInstinct>($"{nameof(FeralInstinct)}", level),
                Common.CreateScriptableObjectAndAddToObject<InstinctivePounce>($"{nameof(InstinctivePounce)}", level),

            });
            
            levels.Add(level);
            
            // Level 08
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                8,
                3,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>($"{nameof(AbilityScoreImprovement)}", level),

            });
            
            levels.Add(level);
            
            // Level 09
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                9,
                4,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 3,
                    weaponMastery = 3
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<BrutalStrike>($"{nameof(BrutalStrike)}", level),

            });
            
            levels.Add(level);
            
            // Level 10
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                10,
                4,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);
            
            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<SubclassFeature>($"{nameof(SubclassFeature)}", level),

            });
            
            levels.Add(level);
            
            // Level 11
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                11,
                4,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<RelentlessRage>($"{nameof(RelentlessRage)}", level),

            });
            
            levels.Add(level);
            
            // Level 12
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                12,
                4,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>($"{nameof(AbilityScoreImprovement)}", level),

            });
            
            levels.Add(level);
            
            // Level 13
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                13,
                5,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<ImprovedBrutalStrike>($"{nameof(ImprovedBrutalStrike)}", level),

            });
            
            levels.Add(level);
            
            // Level 14
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                14,
                5,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<SubclassFeature>($"{nameof(SubclassFeature)}", level),

            });
            
            levels.Add(level);
            
            // Level 15
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                15,
                5,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<PersistentRage>($"{nameof(PersistentRage)}", level),

            });
            
            levels.Add(level);
            
            // Level 16
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                16,
                5,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>($"{nameof(AbilityScoreImprovement)}", level),

            });
            
            levels.Add(level);
            
            // Level 17
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                17,
                6,
                new BarbarianFP()
                {
                    rages = 6,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<ImprovedBrutalStrike>($"{nameof(ImprovedBrutalStrike)}", level),

            });
            
            levels.Add(level);
            
            // Level 18
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                18,
                6,
                new BarbarianFP()
                {
                    rages = 6,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<SubclassFeature>($"{nameof(SubclassFeature)}", level),

            });
            
            levels.Add(level);
            
            // Level 19
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                19,
                6,
                new BarbarianFP()
                {
                    rages = 6,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<EpicBoon>($"{nameof(EpicBoon)}", level),

            });
            
            levels.Add(level);
            
            // Level 20
            level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{levelName}_{++counter:00}",
                20,
                6,
                new BarbarianFP()
                {
                    rages = 6,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                BarbarianLevelsPath);
            
            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<PrimalChampion>($"{nameof(PrimalChampion)}", level),

            });
            
            levels.Add(level);
            
            return levels.ToArray();
        }
        
        private static Level InitializeLevel(string levelName, int levelNum, int proficiencyBonus, IClassFeatureTraits classFeatureTraits, Object parent)
        {
            var level = Common.CreateScriptableObjectAndAddToObject<Level>(levelName, parent);
            level.LevelNum = levelNum;
            level.ProficiencyBonus = proficiencyBonus;
            level.ClassFeatureTraits = classFeatureTraits;

            //AssetDatabase.SaveAssetIfDirty(parent);
            return level;
        }
        
        private static Level InitializeLevel(string levelName, int levelNum, int proficiencyBonus, IClassFeatureTraits classFeatureTraits, string assetPath)
        {
            var level = Common.CreateScriptableObject<Level>(levelName, assetPath);
            level.LevelNum = levelNum;
            level.ProficiencyBonus = proficiencyBonus;
            level.ClassFeatureTraits = classFeatureTraits;

            //AssetDatabase.SaveAssetIfDirty(parent);
            return level;
        }
    }
}