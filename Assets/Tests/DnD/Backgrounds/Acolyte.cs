using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers.PathHelper;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests.DnD.Backgrounds
{
    public class AcolyteUnitTests
    {
        private Background _acolyte;
        private IEnumerable<ScriptableObject> _tools ;
        private static readonly string _className = NameHelper.Backgrounds.Acolyte;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Background)}");
            _acolyte = guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Background>)
                .SingleOrDefault(asset => asset.name == NameHelper.Backgrounds.Acolyte);
            _tools = Common.GetAllScriptableObjects<ScriptableObject>(PathHelper.Backgrounds.AcolyteToolsPath);

        }

        [Test]
        public void AcolyteExists()
        {
            Assert.That(_acolyte, Is.Not.Null, "Acolyte doesn't exist");
        }

        [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.OptionsTestCases))]
        public void TestStartingEquipment(string optionName, string displayName, string displayDescription, (string, int)[] equipment)
        {
            var option = _acolyte.StartingEquipmentOptions.SingleOrDefault(asset => asset.name == optionName);
            Assert.That(option, Is.Not.Null, $"Option {optionName} doesn't exist");
            Assert.That(option.DisplayName, Is.EqualTo(displayName));
            Assert.That(option.DisplayDescription, Is.EqualTo(displayDescription));

            Assert.That(option.EquipmentsWithAmountList.Count, Is.EqualTo(equipment.Length),
                $"Items in option {optionName} don't equal expected length.");
            foreach (var equipmentItem in equipment)
            {
                var item = option.EquipmentsWithAmountList.SingleOrDefault(x => x.Equipment.name == equipmentItem.Item1);
                Assert.That(item, Is.Not.Null, $"Item {equipmentItem.Item1} doesn't exist");
                Assert.That(item.Amount, Is.EqualTo(equipmentItem.Item2),
                    $"Item {equipmentItem.Item1} doesn't equal expected amount: {equipmentItem.Item2}. Found: {item.Amount}");
            }
        }

        [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.AbilitiesTestCases))]
        public bool TestAbilities(string abilityName)
        {
            return Array.Exists(_acolyte.Abilities, ability => ability.name == abilityName);
        }

        [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.SkillProficienciesTestCases))]
        public bool TestSkillProficiencies(string skillName)
        {
            return _acolyte.SkillProficiencies.SingleOrDefault(skill => skill.name == skillName) != null;
        }


        [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.BackgroundDataTest))]
        public void TestProperties(BackgroundData data)
        {
            Assert.That(_acolyte.DisplayName, Is.EqualTo(data.DisplayName));
            Assert.That(_acolyte.DisplayDescription, Is.EqualTo(data.DisplayDescription));
            Assert.That(_acolyte.Feat.name, Is.EqualTo(data.FeatName));
            Assert.That(_acolyte.ToolProficiency.ProficiencyName, Is.EqualTo(data.ToolProficiency));
        }

        [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.ToolsTestCases))]
        public void TestTools(ToolData data)
        {
            var toolSo = _tools.SingleOrDefault(tool => tool.name == data.Name);
            Assert.That(toolSo, Is.Not.Null, $"Tool {data.Name} doesn't exist for {_className}");
            
            // Test ILocalizable
            var tool = toolSo as ILocalizable;
            Assert.That(tool, Is.Not.Null, $"Tool {data.Name} is not of type {nameof(ILocalizable)}");
            Assert.That(tool.DisplayName, Is.EqualTo(data.DisplayName),  $"Tool {data.Name}.{nameof(tool.DisplayName)} doesn't equal expected value: {data.DisplayName}");
            Assert.That(tool.DisplayDescription, Is.EqualTo(data.DisplayDescription),  $"Tool {data.Name}.{nameof(tool.DisplayDescription)} doesn't equal expected value: {data.DisplayDescription}");
        }

        private class AcolyteData
        {
            public static IEnumerable OptionsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        NameHelper.StartingEquipmentOptions.OptionA,
                        $"{nameof(NameHelper.StartingEquipmentOptions)}.{NameHelper.StartingEquipmentOptions.OptionA}",
                        $"{nameof(NameHelper.StartingEquipmentOptions)}.{NameHelper.StartingEquipmentOptions.OptionA}.{NameHelper.Naming.Description}",
                        new (string, int)[]
                        {
                            (NameHelper.Equipment.Tools.CalligrapherTool, 1),
                            (NameHelper.Equipment.Gear.Acolyte.Book, 1),
                            (NameHelper.Equipment.Gear.Acolyte.HolySymbol, 1),
                            (NameHelper.Equipment.Gear.Acolyte.Parchment, 10),
                            (NameHelper.Equipment.Gear.Acolyte.Robe, 1),
                            (NameHelper.CoinValues.GoldPiece, 8)
                        });
                    yield return new TestCaseData(
                        NameHelper.StartingEquipmentOptions.OptionB,
                        $"{nameof(NameHelper.StartingEquipmentOptions)}.{NameHelper.StartingEquipmentOptions.OptionB}",
                        $"{nameof(NameHelper.StartingEquipmentOptions)}.{NameHelper.StartingEquipmentOptions.OptionB}.{NameHelper.Naming.Description}",
                        new (string, int)[]
                        {
                            (NameHelper.CoinValues.GoldPiece, 50)
                        });
                }
            }

            public static IEnumerable AbilitiesTestCases
            {
                get
                {
                    yield return new TestCaseData(NameHelper.Abilities.Charisma).Returns(true);
                    yield return new TestCaseData(NameHelper.Abilities.Constitution).Returns(false);
                    yield return new TestCaseData(NameHelper.Abilities.Dexterity).Returns(false);
                    yield return new TestCaseData(NameHelper.Abilities.Intelligence).Returns(true);
                    yield return new TestCaseData(NameHelper.Abilities.Strength).Returns(false);
                    yield return new TestCaseData(NameHelper.Abilities.Wisdom).Returns(true);
                }
            }

            public static IEnumerable SkillProficienciesTestCases
            {
                get
                {
                    yield return new TestCaseData(NameHelper.Skills.Acrobatics).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.AnimalHandling).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Arcana).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Athletics).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Deception).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.History).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Insight).Returns(true);
                    yield return new TestCaseData(NameHelper.Skills.Intimidation).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Investigation).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Medicine).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Nature).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Perception).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Performance).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Persuasion).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Religion).Returns(true);
                    yield return new TestCaseData(NameHelper.Skills.SleightOfHand).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Stealth).Returns(false);
                    yield return new TestCaseData(NameHelper.Skills.Survival).Returns(false);
                }
            }

            public static IEnumerable BackgroundDataTest
            {
                get
                {
                    yield return new TestCaseData(
                        new BackgroundData()
                        {
                            FeatName = NameHelper.Feats.MagicInitiate,
                            DisplayName = $"{NameHelper.Naming.Backgrounds}.{NameHelper.Backgrounds.Acolyte}",
                            DisplayDescription = $"{NameHelper.Naming.Backgrounds}.{NameHelper.Backgrounds.Acolyte}.{NameHelper.Naming.Description}",
                            ToolProficiency =  NameHelper.Equipment.Tools.CalligrapherTool,
                        });
                }
            }

            public static IEnumerable ToolsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ToolData()
                        {
                            Name = NameHelper.Equipment.Gear.Acolyte.HolySymbol,
                            DisplayName = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.HolySymbol}",
                            DisplayDescription = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.HolySymbol}.{NameHelper.Naming.Description}"
                        }
                    );
                    yield return new TestCaseData(
                        new ToolData()
                        {
                            Name = NameHelper.Equipment.Gear.Acolyte.Parchment,
                            DisplayName = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Parchment}",
                            DisplayDescription = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Parchment}.{NameHelper.Naming.Description}"
                        }
                    );
                    yield return new TestCaseData(
                        new ToolData()
                        {
                            Name = NameHelper.Equipment.Gear.Acolyte.Robe,
                            DisplayName = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Robe}",
                            DisplayDescription = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Robe}.{NameHelper.Naming.Description}"
                        }
                    );
                    yield return new TestCaseData(
                        new ToolData()
                        {
                            Name = NameHelper.Equipment.Gear.Acolyte.Book,
                            DisplayName = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Book}",
                            DisplayDescription = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Book}.{NameHelper.Naming.Description}"
                        }
                    );
                    yield return new TestCaseData(
                        new ToolData()
                        {
                            Name = NameHelper.Equipment.Tools.CalligrapherTool,
                            DisplayName = $"{nameof(NameHelper.Equipment.Tools)}.{NameHelper.Equipment.Tools.CalligrapherTool}",
                            DisplayDescription = $"{nameof(NameHelper.Equipment.Tools)}.{NameHelper.Equipment.Tools.CalligrapherTool}.{NameHelper.Naming.Description}"
                        }
                    );
                }
            }
        }

        public struct BackgroundData
        {
            public string DisplayName;
            public string DisplayDescription;
            public string FeatName;
            public string ToolProficiency;
        }

        public struct ToolData
        {
            public string Name;
            public string DisplayName;
            public string DisplayDescription;
        }
    }
}
