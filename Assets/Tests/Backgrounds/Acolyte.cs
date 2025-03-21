using System;
using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Characters.Backgrounds;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using Tests;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Backgrounds
{
    public class Acolyte
    {
        private Background _acolyte;

        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Background)}");
            _acolyte = guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Background>)
                .SingleOrDefault(asset => asset.name == NameHelper.Backgrounds.Acolyte);
        }

        [Test]
        public void AcolyteExists()
        {
            Assert.That(_acolyte, Is.Not.Null, "Acolyte doesn't exist");
        }

        [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.OptionsTestCases))]
        public void TestStartingEquipment(string optionName, (string, int)[] equipment)
        {
            var option = _acolyte.StartingEquipment.SingleOrDefault(asset => asset.name == optionName);
            Assert.That(option, Is.Not.Null, $"Option {optionName} doesn't exist");

            Assert.That(option.Items.Count, Is.EqualTo(equipment.Length),
                $"Items in option {optionName} don't equal expected length.");
            foreach (var equipmentItem in equipment)
            {
                var item = option.Items.SingleOrDefault(x => x.Item.name == equipmentItem.Item1);
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
            Assert.That(_acolyte.Name, Is.EqualTo(data.LocalizationName));
            Assert.That(_acolyte.Feat.name, Is.EqualTo(data.FeatName));
        }

        [Test, Ignore("Not implemented yet")]
        public void TestTools()
        {
            
        }

        private class AcolyteData
        {
            public static IEnumerable OptionsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        NameHelper.StartingEquipmentOptions.OptionA,
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
                            LocalizationName = $"{NameHelper.Naming.Backgrounds}.{NameHelper.Backgrounds.Acolyte}"
                        });
                }
            }
        }

        public struct BackgroundData
        {
            public string LocalizationName;
            public string FeatName;
        }
    }
}
