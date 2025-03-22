using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Languages;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Weapons;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

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
            var ammunitionType = _ammunitionTypes.SingleOrDefault(d => d.name == expected.DisplayName);
            
            Assert.That(ammunitionType, Is.Not.Null);
            Assert.That(ammunitionType.Amount, Is.EqualTo(expected.Amount));
            Assert.That(ammunitionType.Storage.Name, Is.EqualTo(expected.Storage));
            Assert.That(ammunitionType.Weight, Is.EqualTo(expected.Weight));
            Assert.That(ammunitionType.Cost, Is.EqualTo(expected.Cost));
        }
        
        [TestCaseSource(typeof(WeaponsData), nameof(WeaponsData.WeaponTypesTestCases))]
        public void TestAllWeaponTypes(WeaponTypeModel expected)
        {
            var weaponType = _weaponTypes.SingleOrDefault(d => d.name == expected.DisplayName);
            
            Assert.That(weaponType, Is.Not.Null);
        }
        
        [TestCaseSource(typeof(WeaponsData), nameof(WeaponsData.WeaponsTestCases))]
        public void TestAllWeapons(WeaponModel expected)
        {
            var weapon = _weapons.SingleOrDefault(d => d.name == expected.DisplayName);
            
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon.Type.DisplayName, Is.EqualTo(expected.Type));
            Assert.That(weapon.NumOfDamageDice, Is.EqualTo(expected.NumOfDamageDice));
            Assert.That(weapon.DamageDie.name, Is.EqualTo(expected.DamageDie));
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
                            NameHelper.Storage.Quiver,
                            0.5f,
                            1.0f
                        ));
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Bolts",
                            20,
                            NameHelper.Storage.Case,
                            0.75f,
                            1.0f
                        ));
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Bullets Firearm",
                            10,
                            NameHelper.Storage.Pouch,
                            1.0f,
                            3.0f
                        ));
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Bullets Sling",
                            20,
                            NameHelper.Storage.Pouch,
                            0.75f,
                            4.0f
                        ));
                    yield return new TestCaseData(
                        new AmmunitionTypeModel(
                            "Needles",
                            50,
                            NameHelper.Storage.Pouch,
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
                            NameHelper.WeaponTypes.MartialMeleeWeapon
                        ));
                    yield return new TestCaseData(
                        new WeaponTypeModel(
                            NameHelper.WeaponTypes.MartialRangedWeapon
                        ));
                    yield return new TestCaseData(
                        new WeaponTypeModel(
                            NameHelper.WeaponTypes.SimpleMeleeWeapon
                        ));
                    yield return new TestCaseData(
                        new WeaponTypeModel(
                            NameHelper.WeaponTypes.SimpleRangedWeapon
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
                            NameHelper.Weapons_MartialMelee.Battleaxe,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                             NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                            NameHelper.MasteryProperty.Topple,
                            2.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Flail,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                
                            },
                            NameHelper.MasteryProperty.Sap,
                            1.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Glaive,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D10,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.Reach,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Graze,
                            3.0f,
                            2000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Greataxe,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D12,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Cleave,
                            3.5f,
                            3000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Greatsword,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            2,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Graze,
                            3.0f,
                            5000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Halberd,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D10,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.Reach,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Cleave,
                            3.0f,
                            2000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Lance,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D10,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.Reach,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Topple,
                            3.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Longsword,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                            NameHelper.MasteryProperty.Sap,
                            1.5f,
                            1500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Maul,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            2,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Topple,
                            5.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Morningstar,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                
                            },
                            NameHelper.MasteryProperty.Sap,
                            2.0f,
                            1500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Pike,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D10,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.Reach,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Push,
                            9.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Rapier,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Finesse
                            },
                            NameHelper.MasteryProperty.Vex,
                            1.0f,
                            2500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Scimitar,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Light
                            },
                            NameHelper.MasteryProperty.Nick,
                            1.5f,
                            2500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Shortsword,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Light
                            },
                            NameHelper.MasteryProperty.Vex,
                            1.0f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Trident,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Thrown,
                                NameHelper.WeaponProperty.Versatile
                            },
                            NameHelper.MasteryProperty.Topple,
                            2.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Warhammer,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                            NameHelper.MasteryProperty.Push,
                            2.5f,
                            1500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.WarPick,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                            NameHelper.MasteryProperty.Sap,
                            1.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialMelee.Whip,
                            NameHelper.WeaponTypes.MartialMeleeWeapon,
                            1,
                            NameHelper.Dice.D4,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Reach
                            },
                            NameHelper.MasteryProperty.Slow,
                            1.5f,
                            200.0f
                        ));
                    
                    // MartialRangedWeapon
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialRanged.Blowgun,
                            NameHelper.WeaponTypes.MartialRangedWeapon,
                            1,
                            NameHelper.Dice.D1,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading
                            },
                            NameHelper.MasteryProperty.Vex,
                            0.5f,
                            1000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialRanged.HandCrossbow,
                            NameHelper.WeaponTypes.MartialRangedWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading,
                                NameHelper.WeaponProperty.Light
                            },
                            NameHelper.MasteryProperty.Vex,
                            1.5f,
                            7500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialRanged.HeavyCrossbow,
                            NameHelper.WeaponTypes.MartialRangedWeapon,
                            1,
                            NameHelper.Dice.D10,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading,
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Push,
                            9.0f,
                            5000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialRanged.Longbow,
                            NameHelper.WeaponTypes.MartialRangedWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Heavy,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Slow,
                            1.0f,
                            5000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialRanged.Musket,
                            NameHelper.WeaponTypes.MartialRangedWeapon,
                            1,
                            NameHelper.Dice.D12,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Slow,
                            5.0f,
                            50000.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_MartialRanged.Pistol,
                            NameHelper.WeaponTypes.MartialRangedWeapon,
                            1,
                            NameHelper.Dice.D10,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading
                            },
                            NameHelper.MasteryProperty.Vex,
                            1.5f,
                            25000.0f
                        ));
                    
                    // SimpleMeleeWeapon
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Club,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D4,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Light
                            },
                            NameHelper.MasteryProperty.Slow,
                            1.0f,
                            100.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Dagger,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D4,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Light,
                                NameHelper.WeaponProperty.Thrown
                            },
                            NameHelper.MasteryProperty.Nick,
                            0.5f,
                            200.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Greatclub,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Push,
                            5.0f,
                            200.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Handaxe,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Light,
                                NameHelper.WeaponProperty.Thrown
                            },
                            NameHelper.MasteryProperty.Vex,
                            1.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Javelin,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Thrown
                            },
                            NameHelper.MasteryProperty.Slow,
                            1.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.LightHammer,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D4,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Light,
                                NameHelper.WeaponProperty.Thrown
                            },
                            NameHelper.MasteryProperty.Nick,
                            1.0f,
                            200.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Mace,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                
                            },
                            NameHelper.MasteryProperty.Sap,
                            2.0f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Quarterstaff,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Versatile
                            },
                            NameHelper.MasteryProperty.Topple,
                            2.0f,
                            200.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Sickle,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D4,
                            NameHelper.DamageTypes.Slashing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Light
                            },
                            NameHelper.MasteryProperty.Nick,
                            1.0f,
                            100.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleMelee.Spear,
                            NameHelper.WeaponTypes.SimpleMeleeWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Thrown,
                                NameHelper.WeaponProperty.Versatile
                            },
                            NameHelper.MasteryProperty.Sap,
                            1.5f,
                            100.0f
                        ));
                    
                    // SimpleRangedWeapon
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleRanged.Dart,
                            NameHelper.WeaponTypes.SimpleRangedWeapon,
                            1,
                            NameHelper.Dice.D4,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Finesse,
                                NameHelper.WeaponProperty.Thrown
                            },
                            NameHelper.MasteryProperty.Vex,
                            0.125f,
                            500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleRanged.LightCrossbow,
                            NameHelper.WeaponTypes.SimpleRangedWeapon,
                            1,
                            NameHelper.Dice.D8,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.Loading,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Slow,
                            2.5f,
                            2500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleRanged.Shortbow,
                            NameHelper.WeaponTypes.SimpleRangedWeapon,
                            1,
                            NameHelper.Dice.D6,
                            NameHelper.DamageTypes.Piercing,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                                NameHelper.WeaponProperty.TwoHanded
                            },
                            NameHelper.MasteryProperty.Vex,
                            1.0f,
                            2500.0f
                        ));
                    yield return new TestCaseData(
                        new WeaponModel(
                            NameHelper.Weapons_SimpleRanged.Sling,
                            NameHelper.WeaponTypes.SimpleRangedWeapon,
                            1,
                            NameHelper.Dice.D4,
                            NameHelper.DamageTypes.Bludgeoning,
                            new string[]
                            {
                                NameHelper.WeaponProperty.Ammunition,
                            },
                            NameHelper.MasteryProperty.Slow,
                            0f,
                            100.0f
                        ));
                }
            }

        }
        
        public class AmmunitionTypeModel
        {
            public string DisplayName { get; set; }
            public int Amount { get; set; }
            public string Storage { get; set; }
            public float Weight { get; set; }
            public float Cost { get; set; }

            public AmmunitionTypeModel(
                string displayName,  
                int amount,
                string storage,
                float weight,
                float cost)
            {
                this.DisplayName = displayName;
                this.Amount = amount;
                this.Storage = storage;
                this.Weight = weight;
                this.Cost = cost;
            }
        }

        public class WeaponTypeModel
        {
            public string DisplayName { get; set; }
            
            public WeaponTypeModel(
                string displayName)
            {
                this.DisplayName = displayName;
            }
        }

        public class WeaponModel
        {
            public string DisplayName { get; set; }
            public string Type { get; set; }
            public int NumOfDamageDice { get; set; }
            public string DamageDie  { get; set; }
            public string DamageType { get; set; }
            public string[] Properties { get; set; }
            public string MasteryProperty { get; set; }
            public float Weight { get; set; }
            public float Cost { get; set; }

            public WeaponModel(
                string displayName,
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
                this.DisplayName = displayName;
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