using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Helpers.NameHelper;
using NUnit.Framework;
using UnityEditor;

namespace Tests.Classes.Barbarian
{
    [TestFixture]
    public class Barbarian
    {
        private Class _class;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Class)}");
            _class =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Class>)
                .Single(asset => asset.name == NameHelper.Species.Human);
        }

        private class ClassData
        {
            public static IEnumerable ClassTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ClassTestModel()
                        {
                            Name = NameHelper.Classes.Barbarian,
                            Ability = NameHelper.Abilities.Strength,
                            Die = NameHelper.Dice.D12,
                            SavingThrowProficiencies = new []
                            {
                                NameHelper.Abilities.Strength,
                                NameHelper.Abilities.Constitution
                            },
                            SkillProficienciesAvailable = new []
                            {
                                NameHelper.Skills.AnimalHandling,
                                NameHelper.Skills.Athletics,
                                NameHelper.Skills.Intimidation,
                                NameHelper.Skills.Nature,
                                NameHelper.Skills.Perception,
                                NameHelper.Skills.Survival
                            },
                            WeaponProficiencies = new []
                            {
                                NameHelper.WeaponTypes.SimpleMeleeWeapon,
                                NameHelper.WeaponTypes.SimpleRangedWeapon,
                                NameHelper.WeaponTypes.MartialMeleeWeapon,
                                NameHelper.WeaponTypes.MartialRangedWeapon,
                            },
                            ArmorTraining = new []
                            {
                                NameHelper.ArmourType.LightArmour,
                                NameHelper.ArmourType.MediumArmour,
                                NameHelper.ArmourType.Shield,
                            },
                            Levels = new []
                            {
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_01",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_02",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_03",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_04",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_05",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_06",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_07",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_08",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_09",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_10",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_11",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_12",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_13",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_14",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_15",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_16",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_17",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_18",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_19",
                                },
                                new LevelTestModel()
                                {
                                    Name = $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_20",
                                },
                            },
                            SubClasses = new []
                            {
                                new SubClassTestModel(){},
                                new SubClassTestModel(){},
                                new SubClassTestModel(){},
                                new SubClassTestModel(){},
                            }
                        });
                }
            }
        }
    }
}