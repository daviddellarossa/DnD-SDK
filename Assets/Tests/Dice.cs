using System.Collections;
using System.Linq;
using DnD.Code.Scripts;
using NUnit.Framework;
using UnityEditor;

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
        }
        
        private class AbilitiesData
        {
            public static IEnumerable DamageTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new DiceModel(
                            "D3",
                            3
                        ));
                    yield return new TestCaseData(
                        new DiceModel(
                            "D4",
                            4
                        ));
                    yield return new TestCaseData(
                        new DiceModel(
                            "D6",
                            6
                        ));
                    yield return new TestCaseData(
                        new DiceModel(
                            "D8",
                            8
                        ));
                    yield return new TestCaseData(
                        new DiceModel(
                            "D10",
                            10
                        ));
                    yield return new TestCaseData(
                        new DiceModel(
                            "D12",
                            12
                        ));
                    yield return new TestCaseData(
                        new DiceModel(
                            "D20",
                            20
                        ));
                    yield return new TestCaseData(
                        new DiceModel(
                            "D100",
                            100
                        ));
                }
            }
        }
        
        public class DiceModel
        {
            public string Name { get; set; }
            public int NumOfFaces { get; set; }

            public DiceModel(string name,  int numOfFaces)
            {
                this.Name = name;
                this.NumOfFaces = numOfFaces;
            }
        }
    }
}