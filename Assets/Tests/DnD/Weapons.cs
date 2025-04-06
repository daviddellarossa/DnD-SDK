using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Weapons;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests
{
    [TestFixture]
    public class Weapons
    {
        private Weapon[] _weapons;
        
        [SetUp]
        public void Setup()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(Weapon)}");
            _weapons =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Weapon>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(WeaponsData), nameof(WeaponsData.WeaponsTestCases))]
        public void TestAllWeapons(WeaponTestModel expected)
        {
            var weapon = _weapons.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(weapon, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Weapons, expected.Name));
            Assert.That(weapon.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(weapon.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(weapon.NumOfDamageDice, Is.EqualTo(expected.NumOfDamageDice), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.NumOfDamageDice), expected.NumOfDamageDice));
            Assert.That(weapon.DamageDie.name, Is.EqualTo(expected.DamageDie), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DamageDie), expected.DamageDie));
            Assert.That(weapon.DamageType.name, Is.EqualTo(expected.DamageType), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DamageType), expected.DamageType));
            Assert.That(weapon.Properties.Select(x => x.name), Is.EquivalentTo(expected.Properties), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Properties), expected.Properties));
            Assert.That(weapon.MasteryProperty.name, Is.EqualTo(expected.MasteryProperty), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.MasteryProperty), expected.MasteryProperty));
            Assert.That(weapon.Weight, Is.EqualTo(expected.Weight), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Weight), expected.Weight));
            Assert.That(weapon.Cost, Is.EqualTo(expected.Cost), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Cost), expected.Cost));
        }
        
        private class WeaponsData
        {
            public static IEnumerable WeaponsTestCases
            {
                get
                {
                    // MartialMeleeWeapon
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Battleaxe,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 2.0f,
                            Cost = 1000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Topple,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Flail,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 1.0f,
                            Cost = 1000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Sap,
                            Properties = new string[] { },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Glaive,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D10,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 3.0f,
                            Cost = 2000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Graze,
                            Properties = new []
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.Reach,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Greataxe,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D12,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 3.5f,
                            Cost = 3000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Cleave,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Greatsword,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 2,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 3.0f,
                            Cost = 5000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Graze,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });

                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Halberd,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D10,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 3.0f,
                            Cost = 2000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Cleave,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.Reach,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });

                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Lance,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D10,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 3.0f,
                            Cost = 1000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Topple,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.Reach,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });

                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Longsword,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 1.5f,
                            Cost = 1500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Sap,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Maul,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 2,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 5.0f,
                            Cost = 1000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Topple,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Morningstar,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 2.0f,
                            Cost = 1500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Sap,
                            Properties = new string[] { },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Pike,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D10,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 9.0f,
                            Cost = 500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Push,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.Reach,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Rapier,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.0f,
                            Cost = 2500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Vex,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Finesse
                            },
                        });

                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Scimitar,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 1.5f,
                            Cost = 2500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Nick,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Light
                            },
                        });

                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Shortsword,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.0f,
                            Cost = 1000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Vex,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Light
                            },
                        });

                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Trident,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 2.0f,
                            Cost = 500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Topple,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Thrown,
                                NameHelper.WeaponProperty.Versatile
                            },
                        });

                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Warhammer,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 2.5f,
                            Cost = 1500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Push,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.WarPick,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.0f,
                            Cost = 500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Sap,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                        });

                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialMelee.Whip,
                            Type = NameHelper.WeaponTypes.MartialMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D4,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 1.5f,
                            Cost = 200.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Slow,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Reach
                            },
                        });
                    
                    // MartialRangedWeapon
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialRanged.Blowgun,
                            Type = NameHelper.WeaponTypes.MartialRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D1,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 0.5f,
                            Cost = 1000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Vex,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialRanged.HandCrossbow,
                            Type = NameHelper.WeaponTypes.MartialRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.5f,
                            Cost = 7500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Vex,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading,
                                NameHelper.WeaponProperty.Light
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialRanged.HeavyCrossbow,
                            Type = NameHelper.WeaponTypes.MartialRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D10,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 9.0f,
                            Cost = 5000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Push,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading,
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialRanged.Longbow,
                            Type = NameHelper.WeaponTypes.MartialRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.0f,
                            Cost = 5000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Slow,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialRanged.Musket,
                            Type = NameHelper.WeaponTypes.MartialRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D12,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 5.0f,
                            Cost = 50000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Slow,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_MartialRanged.Pistol,
                            Type = NameHelper.WeaponTypes.MartialRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D10,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.5f,
                            Cost = 25000.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Vex,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading
                            },
                        });
                    
                    // SimpleMeleeWeapon
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Club,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D4,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 1.0f,
                            Cost = 100.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Slow,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Light
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Dagger,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D4,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 0.5f,
                            Cost = 200.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Nick,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Light,
                                NameHelper.WeaponProperty.Thrown
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Greatclub,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 5.0f,
                            Cost = 200.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Push,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Handaxe,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 1.0f,
                            Cost = 500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Vex,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Light,
                                NameHelper.WeaponProperty.Thrown
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Javelin,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.0f,
                            Cost = 500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Slow,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Thrown
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.LightHammer,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D4,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 1.0f,
                            Cost = 200.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Nick,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Light,
                                NameHelper.WeaponProperty.Thrown
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Mace,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 2.0f,
                            Cost = 500.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Sap,
                            Properties = new string[] { },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Quarterstaff,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 2.0f,
                            Cost = 200.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Topple,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Sickle,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D4,
                            DamageType = NameHelper.DamageTypes.Slashing,
                            Weight = 1.0f,
                            Cost = 100.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Nick,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Light
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleMelee.Spear,
                            Type = NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.5f,
                            Cost = 100.0f,
                            MasteryProperty = NameHelper.MasteryProperty.Sap,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Thrown,
                                NameHelper.WeaponProperty.Versatile
                            },
                        });
                    
                    // SimpleRangedWeapon
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleRanged.Dart,
                            Type = NameHelper.WeaponTypes.SimpleRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D4,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 0.125f,
                            Cost = 500f,
                            MasteryProperty = NameHelper.MasteryProperty.Vex,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Thrown
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleRanged.LightCrossbow,
                            Type = NameHelper.WeaponTypes.SimpleRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D8,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 2.5f,
                            Cost = 2500f,
                            MasteryProperty = NameHelper.MasteryProperty.Slow,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleRanged.Shortbow,
                            Type = NameHelper.WeaponTypes.SimpleRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D6,
                            DamageType = NameHelper.DamageTypes.Piercing,
                            Weight = 1.0f,
                            Cost = 2500f,
                            MasteryProperty = NameHelper.MasteryProperty.Vex,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                        });
                    
                    yield return new TestCaseData(
                        new WeaponTestModel()
                        {
                            Name = NameHelper.Weapons_SimpleRanged.Sling,
                            Type = NameHelper.WeaponTypes.SimpleRangedWeapon,
                            NumOfDamageDice = 1,
                            DamageDie = NameHelper.Dice.D4,
                            DamageType = NameHelper.DamageTypes.Bludgeoning,
                            Weight = 0.0f,
                            Cost = 100f,
                            MasteryProperty = NameHelper.MasteryProperty.Slow,
                            Properties = new[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                            },
                        });
                }
            }

        }

        public class WeaponTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Weapons}.{Type}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            public string Type { get; set; }
            public int NumOfDamageDice { get; set; }
            public string DamageDie  { get; set; }
            public string DamageType { get; set; }
            public string[] Properties { get; set; }
            public string MasteryProperty { get; set; }
            public float Weight { get; set; }
            public float Cost { get; set; }
        }
    }
}