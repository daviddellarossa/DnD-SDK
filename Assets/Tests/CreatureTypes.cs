using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game.Characters.Species;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Characters.Species;
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
                            Helper.CreatureTypes.Aberration
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Beast
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Celestial
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Construct
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Dragon
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Elemental
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Fey
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Fiend
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Giant
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Humanoid
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Monstrosity
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Ooze
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Plant
                        ));
                    yield return new TestCaseData(
                        new CreatureTypeModel(
                            Helper.CreatureTypes.Undead
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