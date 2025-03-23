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
        public void TestAllArmours(ArmourTestModel expected)
        {
            var armour = _armours.SingleOrDefault(armour => armour.name == expected.Name);
            
            Assert.That(armour, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Armours, expected.Name));
            Assert.That(armour.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(armour.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(armour.Type.name, Is.EqualTo(expected.Type), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Type), expected.Type));
            Assert.That(armour.ArmourClass, Is.EqualTo(expected.ArmourClass), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.ArmourClass), expected.ArmourClass));
            Assert.That(armour.AddDexModifier, Is.EqualTo(expected.AddDexModifier), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.AddDexModifier), expected.AddDexModifier));
            Assert.That(armour.CapDexModifier, Is.EqualTo(expected.CapDexModifier), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.CapDexModifier), expected.CapDexModifier));
            Assert.That(armour.MaxDexModifier, Is.EqualTo(expected.MaxDexModifier), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.MaxDexModifier), expected.MaxDexModifier));
            Assert.That(armour.HasDisadvantageOnDexterityChecks, Is.EqualTo(expected.HasDisadvantageOnDexterityChecks), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.HasDisadvantageOnDexterityChecks), expected.HasDisadvantageOnDexterityChecks));
            Assert.That(armour.Strength, Is.EqualTo(expected.Strength), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Strength), expected.Strength));
            Assert.That(armour.Weight, Is.EqualTo(expected.Weight), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Weight), expected.Weight));
            Assert.That(armour.Cost, Is.EqualTo(expected.Cost), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Cost), expected.Cost));

        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ShieldsTestCases))]
        public void TestAllShields(ShieldTestModel expected)
        {
            var shield = _shields.SingleOrDefault(shield => shield.name == expected.Name);
            
            Assert.That(shield, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Shields, expected.Name));
            Assert.That(shield.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(shield.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(shield.Type.name, Is.EqualTo(expected.Type), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Type), expected.Type));
            Assert.That(shield.IncrementArmourClassBy, Is.EqualTo(expected.IncrementArmourClassBy), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.IncrementArmourClassBy), expected.IncrementArmourClassBy));
            Assert.That(shield.Weight, Is.EqualTo(expected.Weight), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Weight), expected.Weight));
            Assert.That(shield.Cost, Is.EqualTo(expected.Cost), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Cost), expected.Cost));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable ArmoursTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Heavy.ChainMail,
                            Type = NameHelper.ArmourType.HeavyArmour,
                            ArmourClass = 16,
                            AddDexModifier = false,
                            CapDexModifier = false,
                            MaxDexModifier = 0,
                            HasDisadvantageOnDexterityChecks = true,
                            Strength = 13,
                            Weight = 27.5f,
                            Cost = 7500
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Heavy.PlateArmour,
                            Type = NameHelper.ArmourType.HeavyArmour,
                            ArmourClass = 18,
                            AddDexModifier = false,
                            CapDexModifier = false,
                            MaxDexModifier = 0,
                            HasDisadvantageOnDexterityChecks = true,
                            Strength = 15,
                            Weight = 32.5f,
                            Cost = 150000
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Heavy.RingMail,
                            Type = NameHelper.ArmourType.HeavyArmour,
                            ArmourClass = 14,
                            AddDexModifier = false,
                            CapDexModifier = false,
                            MaxDexModifier = 0,
                            HasDisadvantageOnDexterityChecks = true,
                            Strength = 0,
                            Weight = 20.0f,
                            Cost = 3000
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Heavy.SplintArmour,
                            Type = NameHelper.ArmourType.HeavyArmour,
                            ArmourClass = 17,
                            AddDexModifier = false,
                            CapDexModifier = false,
                            MaxDexModifier = 0,
                            HasDisadvantageOnDexterityChecks = true,
                            Strength = 15,
                            Weight = 30.0f,
                            Cost = 20000
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Light.LeatherArmour,
                            Type = NameHelper.ArmourType.LightArmour,
                            ArmourClass = 11,
                            AddDexModifier = true,
                            CapDexModifier = false,
                            MaxDexModifier = 0,
                            HasDisadvantageOnDexterityChecks = true,
                            Strength = 0,
                            Weight = 5.0f,
                            Cost = 1000
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Light.PaddedArmour,
                            Type = NameHelper.ArmourType.LightArmour,
                            ArmourClass = 11,
                            AddDexModifier = true,
                            CapDexModifier = false,
                            MaxDexModifier = 0,
                            HasDisadvantageOnDexterityChecks = true,
                            Strength = 0,
                            Weight = 4.0f,
                            Cost = 500
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Light.StuddedLeatherArmour,
                            Type = NameHelper.ArmourType.LightArmour,
                            ArmourClass = 12,
                            AddDexModifier = true,
                            CapDexModifier = false,
                            MaxDexModifier = 0,
                            HasDisadvantageOnDexterityChecks = false,
                            Strength = 0,
                            Weight = 6.5f,
                            Cost = 4500
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Medium.Breastplate,
                            Type = NameHelper.ArmourType.MediumArmour,
                            ArmourClass = 14,
                            AddDexModifier = true,
                            CapDexModifier = true,
                            MaxDexModifier = 2,
                            HasDisadvantageOnDexterityChecks = false,
                            Strength = 0,
                            Weight = 10.0f,
                            Cost = 40000
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Medium.ChainShirt,
                            Type = NameHelper.ArmourType.MediumArmour,
                            ArmourClass = 13,
                            AddDexModifier = true,
                            CapDexModifier = true,
                            MaxDexModifier = 2,
                            HasDisadvantageOnDexterityChecks = false,
                            Strength = 0,
                            Weight = 10.0f,
                            Cost = 5000
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Medium.HalfPlateArmour,
                            Type = NameHelper.ArmourType.MediumArmour,
                            ArmourClass = 15,
                            AddDexModifier = true,
                            CapDexModifier = true,
                            MaxDexModifier = 2,
                            HasDisadvantageOnDexterityChecks = true,
                            Strength = 0,
                            Weight = 20.0f,
                            Cost = 75000
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Medium.HideArmour,
                            Type = NameHelper.ArmourType.MediumArmour,
                            ArmourClass = 12,
                            AddDexModifier = true,
                            CapDexModifier = true,
                            MaxDexModifier = 2,
                            HasDisadvantageOnDexterityChecks = false,
                            Strength = 0,
                            Weight = 6.0f,
                            Cost = 1000
                        });
                    yield return new TestCaseData(
                        new ArmourTestModel()
                        {
                            Name = NameHelper.Armours_Medium.ScaleMail,
                            Type = NameHelper.ArmourType.MediumArmour,
                            ArmourClass = 14,
                            AddDexModifier = true,
                            CapDexModifier = true,
                            MaxDexModifier = 2,
                            HasDisadvantageOnDexterityChecks = true,
                            Strength = 0,
                            Weight = 22.5f,
                            Cost = 5000
                        });
                }
            }
            
            public static IEnumerable ShieldsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ShieldTestModel()
                        {
                            Name = NameHelper.Shields.Shield,
                            Type = NameHelper.ArmourType.Shield,
                            IncrementArmourClassBy = 2,
                            Weight = 3.0f,
                            Cost = 1000
                        });
                }
            }
        }

        public class ArmourTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Armours}.{Type}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            public string Type { get; set; }
            public int ArmourClass { get; set; }
            public bool AddDexModifier { get; set; }
            public bool CapDexModifier { get; set; }
            public int MaxDexModifier { get; set; }
            public bool HasDisadvantageOnDexterityChecks { get; set; }
            public int Strength { get; set; }
            public float Weight { get; set; }
            public int Cost { get; set; }
            
        }

        public class ShieldTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Shields}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            public string Type { get; set; }
            public int IncrementArmourClassBy { get; set; }
            public float Weight { get; set; }
            public int Cost { get; set; }
        }
    }
}