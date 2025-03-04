using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Characters.Abilities;
using NUnit.Framework;
using UnityEditor;

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
            var armour = _armours.SingleOrDefault(armour => armour.name == expected.Name);
            
            Assert.That(armour, Is.Not.Null);
            Assert.That(armour.Type.name, Is.EqualTo(expected.Type));
            Assert.That(armour.ArmourClass, Is.EqualTo(expected.ArmourClass));
            Assert.That(armour.AddDexModifier, Is.EqualTo(expected.AddDexModifier));
            Assert.That(armour.CapDexModifier, Is.EqualTo(expected.CapDexModifier));
            Assert.That(armour.MaxDexModifier, Is.EqualTo(expected.MaxDexModifier));
            Assert.That(armour.HasDisadvantageOnDexterityChecks, Is.EqualTo(expected.HasDisadvantageOnDexterityChecks));
            Assert.That(armour.Strength, Is.EqualTo(expected.Strength));
            Assert.That(armour.Weight, Is.EqualTo(expected.Weight));
            Assert.That(armour.Cost, Is.EqualTo(expected.Cost));

        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ShieldsTestCases))]
        public void TestAllShields(ShieldModel expected)
        {
            var shield = _shields.SingleOrDefault(shield => shield.name == expected.Name);
            
            Assert.That(shield, Is.Not.Null);
            Assert.That(shield.Type.name, Is.EqualTo(expected.Type));
            Assert.That(shield.IncrementArmourClassBy, Is.EqualTo(expected.IncrementArmourClassBy));
            Assert.That(shield.Weight, Is.EqualTo(expected.Weight));
            Assert.That(shield.Cost, Is.EqualTo(expected.Cost));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable ArmoursTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.ChainMail,
                        Helper.ArmourType.HeavyArmour,
                        16,
                        false,
                        false,
                        0,
                        true,
                        13,
                        27.0f,
                        1
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.PlateArmour,
                        Helper.ArmourType.HeavyArmour,
                        18,
                        false,
                        false,
                        0,
                        true,
                        15,
                        32.5f,
                        0
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.RingMail,
                        Helper.ArmourType.HeavyArmour,
                        14,
                        false,
                        false,
                        0,
                        true,
                        0,
                        20.0f,
                        0
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.SplintArmour,
                        Helper.ArmourType.HeavyArmour,
                        17,
                        false,
                        false,
                        0,
                        true,
                        15,
                        30.0f,
                        0
                        ));

                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.LeatherArmour,
                        Helper.ArmourType.LightArmour,
                        11,
                        true,
                        false,
                        0,
                        true,
                        0,
                        5.0f,
                        0
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.PaddedArmour,
                        Helper.ArmourType.LightArmour,
                        11,
                        true,
                        false,
                        0,
                        true,
                        0,
                        4.0f,
                        0
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.StuddedLeatherArmour,
                        Helper.ArmourType.LightArmour,
                        12,
                        true,
                        false,
                        0,
                        false,
                        0,
                        6.5f,
                        0
                        ));

                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.Breastplate,
                        Helper.ArmourType.MediumArmour,
                        14,
                        true,
                        true,
                        2,
                        false,
                        0,
                        10.0f,
                        0
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.ChainShirt,
                        Helper.ArmourType.MediumArmour,
                        13,
                        true,
                        true,
                        2,
                        false,
                        0,
                        5.0f,
                        0
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.HalfPlateArmour,
                        Helper.ArmourType.MediumArmour,
                        15,
                        true,
                        true,
                        2,
                        true,
                        0,
                        20.0f,
                        0
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.HideArmour,
                        Helper.ArmourType.MediumArmour,
                        12,
                        true,
                        true,
                        2,
                        false,
                        0,
                        6.0f,
                        0
                        ));
                    yield return new TestCaseData(
                        new ArmourModel(
                            Helper.Armour.ScaleMail,
                            Helper.ArmourType.MediumArmour,
                        14,
                        true,
                        true,
                        2,
                        true,
                        0,
                        22.5f,
                        0
                        ));
                }
            }
            
            public static IEnumerable ShieldsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ShieldModel(
                        Helper.Armour.Shield, 
                        Helper.ArmourType.Shield,
                        2,
                        3.0f,
                        0
                        ));
                }
            }
        }

        public class ArmourModel
        {
            public string Name { get; set; }
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
                string name,
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
                Name = name;
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
            public string Name { get; set; }
            public string Type { get; set; }
            public int IncrementArmourClassBy { get; set; }
            public float Weight { get; set; }
            public int Cost { get; set; }

            public ShieldModel(
                string name,
                string type,
                int incrementArmourClassBy,
                float weight,
                int cost
                )
            {
                this.Name = name;
                this.Type = type;
                this.IncrementArmourClassBy = incrementArmourClassBy;
                this.Weight = weight;
                this.Cost = cost;
            }
        }
    }
}