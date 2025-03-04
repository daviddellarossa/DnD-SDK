using System.Collections;
using System.Linq;
using Assets.Scripts.Game.Characters.Species;
using DnD.Code.Scripts.Combat;
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
                            "Acid"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Bludgeoning"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Cold"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Fire"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Force"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Lightning"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Necrotic"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Piercing"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Poison"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Psychic"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Radiant"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Slashing"
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            "Thunder"
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