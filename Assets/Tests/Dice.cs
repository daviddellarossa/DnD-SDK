using System.Collections;
using System.Linq;
using DnD.Code.Scripts;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests
{
    [TestFixture]
    public class Dice
    {
        private Die[] _dice;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Die)}");
            _dice =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Die>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.DamageTypeTestCases))]
        public void TestAllDice(DiceModel expected)
        {
            var die = _dice.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(die, Is.Not.Null);
            Assert.That(die.NumOfFaces, Is.EqualTo(expected.NumOfFaces));
            
            // Testing ILocalizable
            Assert.That(die.DisplayName, Is.EqualTo(expected.DisplayName));
            Assert.That(die.DisplayDescription, Is.EqualTo(expected.DisplayDescription));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable DamageTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            
                            Name = NameHelper.Dice.D1,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D1}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D1}.{NameHelper.Naming.Description}",
                            NumOfFaces = 1
                        });
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            Name = NameHelper.Dice.D3,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D3}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D3}.{NameHelper.Naming.Description}",
                            NumOfFaces = 3
                        });
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            Name = NameHelper.Dice.D4,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D4}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D4}.{NameHelper.Naming.Description}",
                            NumOfFaces = 4
                        });
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            Name = NameHelper.Dice.D6,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D6}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D6}.{NameHelper.Naming.Description}",
                            NumOfFaces = 6
                        });
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            Name = NameHelper.Dice.D8,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D8}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D8}.{NameHelper.Naming.Description}",
                            NumOfFaces = 8
                        });
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            Name = NameHelper.Dice.D10,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D10}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D10}.{NameHelper.Naming.Description}",
                            NumOfFaces = 10
                        });
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            Name = NameHelper.Dice.D12,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D12}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D12}.{NameHelper.Naming.Description}",
                            NumOfFaces = 12
                        });
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            Name = NameHelper.Dice.D20,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D20}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D20}.{NameHelper.Naming.Description}",
                            NumOfFaces = 20
                        });
                    yield return new TestCaseData(
                        new DiceModel()
                        {
                            Name = NameHelper.Dice.D100,
                            DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D100}",
                            DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D100}.{NameHelper.Naming.Description}",
                            NumOfFaces = 100
                        });
                }
            }
        }
        
        public class DiceModel
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string DisplayDescription { get; set; }
            public int NumOfFaces { get; set; }
        }
    }
}