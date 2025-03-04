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
                            Helper.DamageType.Acid
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Bludgeoning
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Cold
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Fire
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Force
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Lightning
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Necrotic
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Piercing
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Poison
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Psychic
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Radiant
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Slashing
                        ));
                    yield return new TestCaseData(
                        new DamageTypeModel(
                            Helper.DamageType.Thunder
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