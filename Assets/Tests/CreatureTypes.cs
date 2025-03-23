using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Species;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

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
        public void TestAllCreatureTypes(CreatureTypeTestModel expected)
        {
            var creatureType = _creatureTypes.SingleOrDefault(creatureType => creatureType.name == expected.Name);
            
            Assert.That(creatureType, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.CreatureTypes, expected.Name));
            Assert.That(creatureType.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(creatureType.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable CreatureTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Aberration
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Beast
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Celestial
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Construct
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Dragon
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Elemental
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Fey
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Fiend
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Giant
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Humanoid
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Monstrosity
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Ooze
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Plant
                        });
                    yield return new TestCaseData(
                        new CreatureTypeTestModel()
                        {
                            Name = NameHelper.CreatureTypes.Undead
                        });
                }
            }
        }
        public class CreatureTypeTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.CreatureTypes}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        }
    }
}