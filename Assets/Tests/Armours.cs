using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests
{
    public class Armours
    {
        private Armour[] _armours;
        private Shield[] _shields;

        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Armour)}");
            _armours =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Armour>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(Shield)}");
            _shields =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Shield>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ArmoursTestCases))]
        public void TestAllArmours(ArmourModel expected)
        {
            var armour = _armours.SingleOrDefault(armour => armour.name == expected.DisplayName);
            
            Assert.That(armour, Is.Not.Null, GetTestName(expected.DisplayName, string.Empty));
            Assert.That(armour.Type.name, Is.EqualTo(expected.Type), GetTestName(expected.DisplayName, nameof(armour.Type)));
            Assert.That(armour.ArmourClass, Is.EqualTo(expected.ArmourClass), GetTestName(expected.DisplayName, nameof(armour.ArmourClass)));
            Assert.That(armour.AddDexModifier, Is.EqualTo(expected.AddDexModifier), GetTestName(expected.DisplayName, nameof(armour.AddDexModifier)));
            Assert.That(armour.CapDexModifier, Is.EqualTo(expected.CapDexModifier), GetTestName(expected.DisplayName, nameof(armour.CapDexModifier)));
            Assert.That(armour.MaxDexModifier, Is.EqualTo(expected.MaxDexModifier), GetTestName(expected.DisplayName, nameof(armour.MaxDexModifier)));
            Assert.That(armour.HasDisadvantageOnDexterityChecks, Is.EqualTo(expected.HasDisadvantageOnDexterityChecks), GetTestName(expected.DisplayName, nameof(armour.HasDisadvantageOnDexterityChecks)));
            Assert.That(armour.Strength, Is.EqualTo(expected.Strength), GetTestName(expected.DisplayName, nameof(armour.Strength)));
            Assert.That(armour.Weight, Is.EqualTo(expected.Weight), GetTestName(expected.DisplayName, nameof(armour.Weight)));
            Assert.That(armour.Cost, Is.EqualTo(expected.Cost), GetTestName(expected.DisplayName, nameof(armour.Cost)));

        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ShieldsTestCases))]
        public void TestAllShields(ShieldModel expected)
        {
            var shield = _shields.SingleOrDefault(shield => shield.name == expected.DisplayName);
            
            Assert.That(shield, Is.Not.Null);
            Assert.That(shield.Type.name, Is.EqualTo(expected.Type), GetTestName(expected.DisplayName, nameof(shield.Type)));
            Assert.That(shield.IncrementArmourClassBy, Is.EqualTo(expected.IncrementArmourClassBy), GetTestName(expected.DisplayName, nameof(shield.IncrementArmourClassBy)));
            Assert.That(shield.Weight, Is.EqualTo(expected.Weight), GetTestName(expected.DisplayName, nameof(shield.Weight)));
            Assert.That(shield.Cost, Is.EqualTo(expected.Cost), GetTestName(expected.DisplayName, nameof(shield.Cost)));
        }
        
        private string GetTestName(string caseName, string propertyName)
            => $"{caseName}: {propertyName}";
        
        private class AbilitiesData
        {
            public static IEnumerable ArmoursTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Heavy.ChainMail,
                        NameHelper.ArmourType.HeavyArmour,
                        16,
                        false,
                        false,
                        0,
                        true,
                        13,
                        27.5f,
                        7500
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Heavy.PlateArmour,
                        NameHelper.ArmourType.HeavyArmour,
                        18,
                        false,
                        false,
                        0,
                        true,
                        15,
                        32.5f,
                        150000
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Heavy.RingMail,
                        NameHelper.ArmourType.HeavyArmour,
                        14,
                        false,
                        false,
                        0,
                        true,
                        0,
                        20.0f,
                        3000
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Heavy.SplintArmour,
                        NameHelper.ArmourType.HeavyArmour,
                        17,
                        false,
                        false,
                        0,
                        true,
                        15,
                        30.0f,
                        20000
                        ));

                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Light.LeatherArmour,
                        NameHelper.ArmourType.LightArmour,
                        11,
                        true,
                        false,
                        0,
                        true,
                        0,
                        5.0f,
                        1000
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Light.PaddedArmour,
                        NameHelper.ArmourType.LightArmour,
                        11,
                        true,
                        false,
                        0,
                        true,
                        0,
                        4.0f,
                        500
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Light.StuddedLeatherArmour,
                        NameHelper.ArmourType.LightArmour,
                        12,
                        true,
                        false,
                        0,
                        false,
                        0,
                        6.5f,
                        4500
                        ));

                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Medium.Breastplate,
                        NameHelper.ArmourType.MediumArmour,
                        14,
                        true,
                        true,
                        2,
                        false,
                        0,
                        10.0f,
                        40000
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Medium.ChainShirt,
                        NameHelper.ArmourType.MediumArmour,
                        13,
                        true,
                        true,
                        2,
                        false,
                        0,
                        10.0f,
                        5000
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Medium.HalfPlateArmour,
                        NameHelper.ArmourType.MediumArmour,
                        15,
                        true,
                        true,
                        2,
                        true,
                        0,
                        20.0f,
                        75000
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Medium.HideArmour,
                        NameHelper.ArmourType.MediumArmour,
                        12,
                        true,
                        true,
                        2,
                        false,
                        0,
                        6.0f,
                        1000
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            NameHelper.Armours_Medium.ScaleMail,
                            NameHelper.ArmourType.MediumArmour,
                        14,
                        true,
                        true,
                        2,
                        true,
                        0,
                        22.5f,
                        5000
                        ));
                }
            }
            
            public static IEnumerable ShieldsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ShieldModel(
                        NameHelper.Shields.Shield, 
                        NameHelper.ArmourType.Shield,
                        2,
                        3.0f,
                        1000
                        ));
                }
            }
        }

        public class ArmourModel
        {
            public string DisplayName { get; set; }
            public string Type { get; set; }
            public int ArmourClass { get; set; }
            public bool AddDexModifier { get; set; }
            public bool CapDexModifier { get; set; }
            public int MaxDexModifier { get; set; }
            public bool HasDisadvantageOnDexterityChecks { get; set; }
            public int Strength { get; set; }
            public float Weight { get; set; }
            public int Cost { get; set; }

            public  ArmourModel(
                string displayName,
                string type,
                int armourClass,
                bool addDexModifier,
                bool capDexModifier,
                int maxDexModifier,
                bool hasDisadvantageOnDexterityChecks,
                int strength,
                float weight,
                int cost
                )
            {
                DisplayName = displayName;
                Type = type;
                ArmourClass = armourClass;
                AddDexModifier = addDexModifier;
                CapDexModifier = capDexModifier;
                MaxDexModifier = maxDexModifier;
                HasDisadvantageOnDexterityChecks = hasDisadvantageOnDexterityChecks;
                Strength = strength;
                Weight = weight;
                Cost = cost;
            }
        }

        public class ShieldModel
        {
            public string DisplayName { get; set; }
            public string Type { get; set; }
            public int IncrementArmourClassBy { get; set; }
            public float Weight { get; set; }
            public int Cost { get; set; }

            public ShieldModel(
                string displayName,
                string type,
                int incrementArmourClassBy,
                float weight,
                int cost
                )
            {
                this.DisplayName = displayName;
                this.Type = type;
                this.IncrementArmourClassBy = incrementArmourClassBy;
                this.Weight = weight;
                this.Cost = cost;
            }
        }
    }
}