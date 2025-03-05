using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Combat;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    [TestFixture]
    public class DamageTypes
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
        public void TestAllDamageTypes(DamageTypeModel expected)
        {
            var damageType = _damageTypes.SingleOrDefault(damageType => damageType.name == expected.Name);
            
            Assert.That(damageType, Is.Not.Null);
        }
        
        private class AbilitiesData
        {
            public static IEnumerable DamageTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Acid
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Bludgeoning
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Cold
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Fire
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Force
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Lightning
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Necrotic
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Piercing
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Poison
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Psychic
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Radiant
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Slashing
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageType.Thunder
                        ));
                }
            }
        }
        public class DamageTypeModel
        {
            public string Name { get; set; }

            public DamageTypeModel(string name)
            {
                this.Name = name;
            }
        }
    }
}