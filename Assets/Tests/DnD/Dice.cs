using System.Collections;
using System.Linq;
using DnD.Code.Scripts;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests.DnD
{
    [TestFixture]
    public class DiceUnitTests
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
        public void TestAllDice(DiceTestModel expected)
        {
            var die = _dice.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(die, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Dice, expected.Name));
            Assert.That(die.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(die.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(die.NumOfFaces, Is.EqualTo(expected.NumOfFaces), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.NumOfFaces), expected.NumOfFaces));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable DamageTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            
                            Name = NameHelper.Dice.D1,
                            NumOfFaces = 1
                        });
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            Name = NameHelper.Dice.D3,
                            NumOfFaces = 3
                        });
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            Name = NameHelper.Dice.D4,
                            NumOfFaces = 4
                        });
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            Name = NameHelper.Dice.D6,
                            NumOfFaces = 6
                        });
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            Name = NameHelper.Dice.D8,
                            NumOfFaces = 8
                        });
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            Name = NameHelper.Dice.D10,
                            NumOfFaces = 10
                        });
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            Name = NameHelper.Dice.D12,
                            NumOfFaces = 12
                        });
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            Name = NameHelper.Dice.D20,
                            NumOfFaces = 20
                        });
                    yield return new TestCaseData(
                        new DiceTestModel()
                        {
                            Name = NameHelper.Dice.D100,
                            NumOfFaces = 100
                        });
                }
            }
        }
        
        public class DiceTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Dice}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            public int NumOfFaces { get; set; }
        }
    }
}