using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Combat;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

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
                            NameHelper.DamageTypes.Acid
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Bludgeoning
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Cold
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Fire
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Force
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Lightning
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Necrotic
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Piercing
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Poison
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Psychic
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Radiant
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Slashing
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            NameHelper.DamageTypes.Thunder
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