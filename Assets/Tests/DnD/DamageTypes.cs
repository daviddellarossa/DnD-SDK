using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Combat;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests.DnD
{
    [TestFixture]
    public class DamageTypesUnitTests
    {
        private DamageType[] _damageTypes;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(DamageType)}");
            _damageTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<DamageType>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.DamageTypeTestCases))]
        public void TestAllDamageTypes(DamageTypeTestModel expected)
        {
            var damageType = _damageTypes.SingleOrDefault(damageType => damageType.name == expected.Name);
            
            Assert.That(damageType, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.DamageTypes, expected.Name));
            Assert.That(damageType.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(damageType.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable DamageTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Acid
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Bludgeoning
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Cold
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Fire
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Force
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Lightning
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Necrotic
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Piercing
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Poison
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Psychic
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Radiant
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Slashing
                        });
                    yield return new TestCaseData(
                        new DamageTypeTestModel()
                        {
                            Name = NameHelper.DamageTypes.Thunder
                        });
                }
            }
        }
        public class DamageTypeTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.DamageTypes}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        }
    }
}