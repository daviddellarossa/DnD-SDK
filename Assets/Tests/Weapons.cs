using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Languages;
using DnD.Code.Scripts.Weapons;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    [TestFixture]
    public class Weapons
    {
        private AmmunitionType[] _ammunitionTypes;
        private WeaponType[] _weaponTypes;
        private Weapon[] _weapons;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(AmmunitionType)}");
            _ammunitionTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<AmmunitionType>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(WeaponType)}");
            _weaponTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<WeaponType>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(Weapon)}");
            _weapons =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Weapon>)
                .Where(asset => asset != null)
                .ToArray();
            
        }
        
        [TestCaseSource(typeof(WeaponsData), nameof(WeaponsData.AmmunitionTypesTestCases))]
        public void TestAllAmmunitionTypes(AmmunitionTypeModel expected)
        {
            var ammunitionType = _ammunitionTypes.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(ammunitionType, Is.Not.Null);
            Assert.That(ammunitionType.Amount, Is.EqualTo(expected.Amount));
            Assert.That(ammunitionType.Storage.Name, Is.EqualTo(expected.Storage));
            Assert.That(ammunitionType.Weight, Is.EqualTo(expected.Weight));
            Assert.That(ammunitionType.Cost, Is.EqualTo(expected.Cost));
        }
        
        [TestCaseSource(typeof(WeaponsData), nameof(WeaponsData.WeaponTypesTestCases))]
        public void TestAllWeaponTypes(WeaponTypeModel expected)
        {
            var weaponType = _weaponTypes.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(weaponType, Is.Not.Null);
        }
        
        [TestCaseSource(typeof(WeaponsData), nameof(WeaponsData.WeaponsTestCases))]
        public void TestAllWeapons(WeaponModel expected)
        {
            var weapon = _weapons.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon.Type.Name, Is.EqualTo(expected.Type));
            Assert.That(weapon.NumOfDamageDice, Is.EqualTo(expected.NumOfDamageDice));
            Assert.That(weapon.DamageDie.Name, Is.EqualTo(expected.DamageDie));
            Assert.That(weapon.DamageType.Name, Is.EqualTo(expected.DamageType));
            Assert.That(weapon.Properties.Select(x => x.Name), Is.EquivalentTo(expected.Properties));
            Assert.That(weapon.MasteryProperty.Name, Is.EqualTo(expected.MasteryProperty));
            Assert.That(weapon.Weight, Is.EqualTo(expected.Weight));
            Assert.That(weapon.Cost, Is.EqualTo(expected.Cost));
        }
        
        private class WeaponsData
        {
            public static IEnumerable AmmunitionTypesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Arrows",
                            20,
                            Helper.Storage.Quiver,
                            0.5f,
                            1.0f
                        ));
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Bolts",
                            20,
                            Helper.Storage.Case,
                            0.75f,
                            1.0f
                        ));
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Bullets Firearm",
                            10,
                            Helper.Storage.Pouch,
                            1.0f,
                            3.0f
                        ));
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Bullets Sling",
                            20,
                            Helper.Storage.Pouch,
                            0.75f,
                            4.0f
                        ));
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Needles",
                            50,
                            Helper.Storage.Pouch,
                            0.5f,
                            1.0f
                        ));
                }
            }
            
            public static IEnumerable WeaponTypesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new WeaponTypeModel(
                            Helper.WeaponType.MartialMeleeWeapon
                        ));
                    yield return new TestCaseData(
                        new WeaponTypeModel(
                            Helper.WeaponType.MartialRangedWeapon
                        ));
                    yield return new TestCaseData(
                        new WeaponTypeModel(
                            Helper.WeaponType.SimpleMeleeWeapon
                        ));
                    yield return new TestCaseData(
                        new WeaponTypeModel(
                            Helper.WeaponType.SimpleRangedWeapon
                        ));
                }
            }
            
            public static IEnumerable WeaponsTestCases
            {
                get
                {
                    // MartialMeleeWeapon
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Battleaxe,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                             Helper.Dice.D8,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Versatile
                            },
                            Helper.MasteryProperty.Topple,
                            2.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Flail,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                
                            },
                            Helper.MasteryProperty.Sap,
                            1.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Glaive,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D10,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.Reach,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Graze,
                            3.0f,
                            2000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Greataxe,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D12,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Cleave,
                            3.5f,
                            3000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Greatsword,
                            Helper.WeaponType.MartialMeleeWeapon,
                            2,
                            Helper.Dice.D6,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Graze,
                            3.0f,
                            5000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Halberd,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D10,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.Reach,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Cleave,
                            3.0f,
                            2000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Lance,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D10,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.Reach,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Topple,
                            3.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Longsword,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Versatile
                            },
                            Helper.MasteryProperty.Sap,
                            1.5f,
                            1500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Maul,
                            Helper.WeaponType.MartialMeleeWeapon,
                            2,
                            Helper.Dice.D6,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Topple,
                            5.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Morningstar,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                
                            },
                            Helper.MasteryProperty.Sap,
                            2.0f,
                            1500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Pike,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D10,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.Reach,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Push,
                            9.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Rapier,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Finesse
                            },
                            Helper.MasteryProperty.Vex,
                            1.0f,
                            2500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Scimitar,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Finesse,
                                Helper.WeaponProperty.Light
                            },
                            Helper.MasteryProperty.Nick,
                            1.5f,
                            2500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Shortsword,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Finesse,
                                Helper.WeaponProperty.Light
                            },
                            Helper.MasteryProperty.Vex,
                            1.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Trident,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Thrown,
                                Helper.WeaponProperty.Versatile
                            },
                            Helper.MasteryProperty.Topple,
                            2.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Warhammer,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                Helper.WeaponProperty.Versatile
                            },
                            Helper.MasteryProperty.Push,
                            2.5f,
                            1500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.WarPick,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Versatile
                            },
                            Helper.MasteryProperty.Sap,
                            1.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Whip,
                            Helper.WeaponType.MartialMeleeWeapon,
                            1,
                            Helper.Dice.D4,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Finesse,
                                Helper.WeaponProperty.Reach
                            },
                            Helper.MasteryProperty.Slow,
                            1.5f,
                            200.0f
                        ));
                    
                    // MartialRangedWeapon
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Blowgun,
                            Helper.WeaponType.MartialRangedWeapon,
                            1,
                            Helper.Dice.D1,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                                Helper.WeaponProperty.Loading
                            },
                            Helper.MasteryProperty.Vex,
                            0.5f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.HandCrossbow,
                            Helper.WeaponType.MartialRangedWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                                Helper.WeaponProperty.Loading,
                                Helper.WeaponProperty.Light
                            },
                            Helper.MasteryProperty.Vex,
                            1.5f,
                            7500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.HeavyCrossbow,
                            Helper.WeaponType.MartialRangedWeapon,
                            1,
                            Helper.Dice.D10,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                                Helper.WeaponProperty.Loading,
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Push,
                            9.0f,
                            5000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Longbow,
                            Helper.WeaponType.MartialRangedWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                                Helper.WeaponProperty.Heavy,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Slow,
                            1.0f,
                            5000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Musket,
                            Helper.WeaponType.MartialRangedWeapon,
                            1,
                            Helper.Dice.D12,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                                Helper.WeaponProperty.Loading,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Slow,
                            5.0f,
                            50000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Pistol,
                            Helper.WeaponType.MartialRangedWeapon,
                            1,
                            Helper.Dice.D10,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                                Helper.WeaponProperty.Loading
                            },
                            Helper.MasteryProperty.Vex,
                            1.5f,
                            25000.0f
                        ));
                    
                    // SimpleMeleeWeapon
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Club,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D4,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                Helper.WeaponProperty.Light
                            },
                            Helper.MasteryProperty.Slow,
                            1.0f,
                            100.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Dagger,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D4,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Finesse,
                                Helper.WeaponProperty.Light,
                                Helper.WeaponProperty.Thrown
                            },
                            Helper.MasteryProperty.Nick,
                            0.5f,
                            200.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Greatclub,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Push,
                            5.0f,
                            200.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Handaxe,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Light,
                                Helper.WeaponProperty.Thrown
                            },
                            Helper.MasteryProperty.Vex,
                            1.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Javelin,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Thrown
                            },
                            Helper.MasteryProperty.Slow,
                            1.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.LightHammer,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D4,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                Helper.WeaponProperty.Light,
                                Helper.WeaponProperty.Thrown
                            },
                            Helper.MasteryProperty.Nick,
                            1.0f,
                            200.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Mace,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                
                            },
                            Helper.MasteryProperty.Sap,
                            2.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Quarterstaff,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                Helper.WeaponProperty.Versatile
                            },
                            Helper.MasteryProperty.Topple,
                            2.0f,
                            200.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Sickle,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D4,
                            Helper.DamageType.Slashing,
                            new string[]
                            {
                                Helper.WeaponProperty.Light
                            },
                            Helper.MasteryProperty.Nick,
                            1.0f,
                            100.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Spear,
                            Helper.WeaponType.SimpleMeleeWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Thrown,
                                Helper.WeaponProperty.Versatile
                            },
                            Helper.MasteryProperty.Sap,
                            1.5f,
                            100.0f
                        ));
                    
                    // SimpleRangedWeapon
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Dart,
                            Helper.WeaponType.SimpleRangedWeapon,
                            1,
                            Helper.Dice.D4,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Finesse,
                                Helper.WeaponProperty.Thrown
                            },
                            Helper.MasteryProperty.Vex,
                            0.125f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.LightCrossbow,
                            Helper.WeaponType.SimpleRangedWeapon,
                            1,
                            Helper.Dice.D8,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                                Helper.WeaponProperty.Loading,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Slow,
                            2.5f,
                            2500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Shortbow,
                            Helper.WeaponType.SimpleRangedWeapon,
                            1,
                            Helper.Dice.D6,
                            Helper.DamageType.Piercing,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                                Helper.WeaponProperty.TwoHanded
                            },
                            Helper.MasteryProperty.Vex,
                            1.0f,
                            2500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            Helper.Weapons.Sling,
                            Helper.WeaponType.SimpleRangedWeapon,
                            1,
                            Helper.Dice.D4,
                            Helper.DamageType.Bludgeoning,
                            new string[]
                            {
                                Helper.WeaponProperty.Ammunition,
                            },
                            Helper.MasteryProperty.Slow,
                            0f,
                            100.0f
                        ));
                }
            }

        }
        
        public class AmmunitionTypeModel
        {
            public string Name { get; set; }
            public int Amount { get; set; }
            public string Storage { get; set; }
            public float Weight { get; set; }
            public float Cost { get; set; }

            public AmmunitionTypeModel(
                string name,  
                int amount,
                string storage,
                float weight,
                float cost)
            {
                this.Name = name;
                this.Amount = amount;
                this.Storage = storage;
                this.Weight = weight;
                this.Cost = cost;
            }
        }

        public class WeaponTypeModel
        {
            public string Name { get; set; }
            
            public WeaponTypeModel(
                string name)
            {
                this.Name = name;
            }
        }

        public class WeaponModel
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int NumOfDamageDice { get; set; }
            public string DamageDie  { get; set; }
            public string DamageType { get; set; }
            public string[] Properties { get; set; }
            public string MasteryProperty { get; set; }
            public float Weight { get; set; }
            public float Cost { get; set; }

            public WeaponModel(
                string name,
                string type,
                int numOfDamageDice,
                string damageDie,
                string damageType,
                string[] properties,
                string masteryProperty,
                float weight,
                float cost
                )
            {
                this.Name = name;
                this.Type = type;
                this.NumOfDamageDice = numOfDamageDice;
                this.DamageDie = damageDie;
                this.DamageType = damageType;
                this.Properties = properties;
                this.MasteryProperty = masteryProperty;
                this.Weight = weight;
                this.Cost = cost;
            }
        }
    }
}