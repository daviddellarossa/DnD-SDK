using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game.Characters.Species;
using DnD.Code.Scripts.Armour;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    [TestFixture]
    public class CreatureTypes
    {
        private CreatureType[] _creatureTypes;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(CreatureType)}");
            _creatureTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<CreatureType>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.CreatureTypeTestCases))]
        public void TestAllCreatureTypes(CreatureTypeModel expected)
        {
            var creatureType = _creatureTypes.SingleOrDefault(creatureType => creatureType.name == expected.Name);
            
            Assert.That(creatureType, Is.Not.Null);
        }
        
        private class AbilitiesData
        {
            public static IEnumerable CreatureTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Aberration"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Beast"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Celestial"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Construct"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Dragon"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Elemental"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Fey"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Fiend"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Giant"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Humanoid"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Monstrosity"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Ooze"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Plant"
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            "Undead"
                        ));
                }
            }
        }
        public class CreatureTypeModel
        {
            public string Name { get; set; }

            public CreatureTypeModel(string name)
            {
                this.Name = name;
            }
        }
    }
}