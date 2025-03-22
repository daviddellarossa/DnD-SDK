using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts;
using DnD.Code.Scripts.Characters.Storage;
using DnD.Code.Scripts.Combat;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Weapons.MasteryProperties;
using DnD.Code.Scripts.Weapons.Properties;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;
using Range = DnD.Code.Scripts.Weapons.Properties.Range;

namespace DnD.Editor.Initializer
{
    public static class WeaponsInitializer
    {
        // public static readonly string PathHelper.Weapons.WeaponsPath = $"{Common.FolderPath}/{NameHelper.Naming.Weapons}";
        // public static readonly string PathHelper.Weapons.WeaponTypesPath = $"{PathHelper.Weapons.WeaponsPath}/{NameHelper.Naming.WeaponTypes}";
        // public static readonly string PathHelper.Weapons.AmmunitionTypesPath = $"{PathHelper.Weapons.WeaponsPath}/{NameHelper.Naming.AmmunitionTypes}";
        // public static readonly string PathHelper.Weapons.MartialMeleeWeaponsPath = $"{PathHelper.Weapons.WeaponsPath}/{NameHelper.WeaponTypes.MartialMeleeWeapon}";
        // public static readonly string PathHelper.Weapons.SimpleMeleeWeaponsPath = $"{PathHelper.Weapons.WeaponsPath}/{NameHelper.WeaponTypes.SimpleMeleeWeapon}";
        // public static readonly string PathHelper.Weapons.MartialRangedWeaponsPath = $"{PathHelper.Weapons.WeaponsPath}/{NameHelper.WeaponTypes.MartialRangedWeapon}";
        // public static readonly string PathHelper.Weapons.SimpleRangedWeaponsPath = $"{PathHelper.Weapons.WeaponsPath}/{NameHelper.WeaponTypes.SimpleRangedWeapon}";
        
        public static Weapon[] GetAllWeapons()
        {
            return Common.GetAllScriptableObjects<Weapon>(PathHelper.Weapons.WeaponsPath);
        }
        
        public static WeaponType[] GetAllWeaponTypes()
        {
            return Common.GetAllScriptableObjects<WeaponType>(PathHelper.Weapons.WeaponTypesPath);
        }

        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Weapons")]
        public static void InitializeWeapons()
        {
            var storage = StorageInitializer.GetAllStorage();
            var dice = DiceInitializer.GetAllDice();
            var damageTypes = DamageTypeInitializer.GetAllDamageTypes();
            
            Common.EnsureFolderExists(PathHelper.Weapons.WeaponsPath);

            var weaponTypes = InitializeWeaponTypes();

            var ammunitionTypes = InitializeAmmunitionTypes(storage);

            InitializeMartialMeleeWeapons(weaponTypes, dice, damageTypes, ammunitionTypes);

            InitializeMartialRangedWeapons(weaponTypes, dice, damageTypes, ammunitionTypes);

            InitializeSimpleMeleeWeapons(weaponTypes, dice, damageTypes, ammunitionTypes);

            InitializeSimpleRangedWeapons(weaponTypes, dice, damageTypes, ammunitionTypes);
        }

        private static void InitializeSimpleRangedWeapons(WeaponType[] weaponTypes, Die[] dice, DamageType[] damageType, AmmunitionType[] ammunitionTypes)
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(PathHelper.Weapons.SimpleRangedWeaponsPath);

                var simpleRangedWeaponType =
                    weaponTypes.Single(x => x.name == NameHelper.WeaponTypes.SimpleRangedWeapon);

                {
                    var dart = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleRanged.Dart,
                        PathHelper.Weapons.SimpleRangedWeaponsPath);
                    dart.Name = $"{nameof(NameHelper.Weapons_SimpleRanged)}.{NameHelper.Weapons_SimpleRanged.Dart}";
                    dart.Type = simpleRangedWeaponType;
                    dart.NumOfDamageDice = 1;
                    dart.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    dart.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    dart.Weight = 0.125f;
                    dart.Cost = 500;

                    // Weapon properties
                    var finesse =
                        Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse, dart);
                    finesse.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";


                    var thrown =
                        Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, dart);
                    thrown.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                    thrown.Range = new Range() { Max = 18, Min = 6 };

                    dart.Properties.AddRange(new Property[] { finesse, thrown });

                    // Mastery property
                    var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, dart);
                    vex.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";

                    dart.MasteryProperty = vex;

                }

                {
                    var lightCrossbow =
                        Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleRanged.LightCrossbow,
                            PathHelper.Weapons.SimpleRangedWeaponsPath);
                    lightCrossbow.Name = $"{nameof(NameHelper.Weapons_SimpleRanged)}.{NameHelper.Weapons_SimpleRanged.LightCrossbow}";
                    lightCrossbow.Type = simpleRangedWeaponType;
                    lightCrossbow.NumOfDamageDice = 1;
                    lightCrossbow.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    lightCrossbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    lightCrossbow.Weight = 2.5f;
                    lightCrossbow.Cost = 2500;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            lightCrossbow);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Bolts);
                    ammunition.Range = new Range() { Max = 96, Min = 24 };

                    var loading =
                        Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading,
                            lightCrossbow);
                    loading.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            lightCrossbow);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    lightCrossbow.Properties.AddRange(new Property[] { ammunition, loading, twoHanded });

                    // Mastery property
                    var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                        lightCrossbow);
                    slow.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";

                    lightCrossbow.MasteryProperty = slow;
                }

                {
                    var shortbow = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleRanged.Shortbow,
                        PathHelper.Weapons.SimpleRangedWeaponsPath);
                    shortbow.Name = $"{nameof(NameHelper.Weapons_SimpleRanged)}.{NameHelper.Weapons_SimpleRanged.Shortbow}";
                    shortbow.Type = simpleRangedWeaponType;
                    shortbow.NumOfDamageDice = 1;
                    shortbow.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    shortbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    shortbow.Weight = 0.5f;
                    shortbow.Cost = 2500;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            shortbow);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Arrows);
                    ammunition.Range = new Range() { Max = 96, Min = 24 };


                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            shortbow);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    shortbow.Properties.AddRange(new Property[] { ammunition, twoHanded });

                    // Mastery property
                    var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex,
                        shortbow);
                    vex.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";

                    shortbow.MasteryProperty = vex;
                }

                {
                    var sling = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleRanged.Sling,
                        PathHelper.Weapons.SimpleRangedWeaponsPath);
                    sling.Name = $"{nameof(NameHelper.Weapons_SimpleRanged)}.{NameHelper.Weapons_SimpleRanged.Sling}";
                    sling.Type = simpleRangedWeaponType;
                    sling.NumOfDamageDice = 1;
                    sling.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    sling.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    sling.Weight = 0.0f;
                    sling.Cost = 100;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            sling);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.BulletsSling);
                    ammunition.Range = new Range() { Max = 36, Min = 9 };

                    sling.Properties.AddRange(new Property[] { ammunition });

                    // Mastery property
                    var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                        sling);
                    slow.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                    slow.SlowBy = 3;

                    sling.MasteryProperty = slow;
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void InitializeSimpleMeleeWeapons(WeaponType[] weaponTypes, Die[] dice, DamageType[] damageType,
            AmmunitionType[] ammunitionTypes)
        {
            try
            {
                AssetDatabase.StartAssetEditing();
                Common.EnsureFolderExists(PathHelper.Weapons.SimpleMeleeWeaponsPath);

                var simpleMeleeWeaponType = weaponTypes.Single(x => x.name == NameHelper.WeaponTypes.SimpleMeleeWeapon);

                {
                    var club = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Club,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    club.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Club}";
                    club.Type = simpleMeleeWeaponType;
                    club.NumOfDamageDice = 1;
                    club.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    club.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    club.Weight = 1.0f;
                    club.Cost = 100;

                    // Weapon properties
                    var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                        club);
                    light.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";

                    club.Properties.AddRange(new Property[] { light });

                    // Mastery property
                    var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow, club);
                    slow.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                    slow.SlowBy = 3;

                    club.MasteryProperty = slow;
                }
                {
                    var dagger = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Dagger,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    dagger.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Dagger}";
                    dagger.Type = simpleMeleeWeaponType;
                    dagger.NumOfDamageDice = 1;
                    dagger.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    dagger.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    dagger.Weight = 0.5f;
                    dagger.Cost = 200;

                    // Weapon properties
                    var thrown =
                        Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, dagger);
                    thrown.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                    thrown.Range = new Range() { Max = 18, Min = 6 };

                    var finesse =
                        Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse, dagger);
                    finesse.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";

                    var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                        dagger);
                    light.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";

                    dagger.Properties.AddRange(new Property[] { thrown, finesse, light });

                    // Mastery property
                    var nick = Common.CreateScriptableObjectAndAddToObject<Nick>(NameHelper.MasteryProperty.Nick,
                        dagger);
                    nick.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}";

                    dagger.MasteryProperty = nick;
                }
                {
                    var greatclub = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Greatclub,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    greatclub.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Greatclub}";
                    greatclub.Type = simpleMeleeWeaponType;
                    greatclub.NumOfDamageDice = 1;
                    greatclub.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    greatclub.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    greatclub.Weight = 5.0f;
                    greatclub.Cost = 200;

                    // Weapon properties
                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            greatclub);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    greatclub.Properties.AddRange(new Property[] { twoHanded });

                    // Mastery property
                    var push = Common.CreateScriptableObjectAndAddToObject<Push>(NameHelper.MasteryProperty.Push,
                        greatclub);
                    push.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}";
                    push.Distance = 3;

                    greatclub.MasteryProperty = push;

                }
                {
                    var handaxe = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Handaxe,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    handaxe.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Handaxe}";
                    handaxe.Type = simpleMeleeWeaponType;
                    handaxe.NumOfDamageDice = 1;
                    handaxe.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    handaxe.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    handaxe.Weight = 1.0f;
                    handaxe.Cost = 500;

                    // Weapon properties
                    var thrown =
                        Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, handaxe);
                    thrown.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                    thrown.Range = new Range() { Max = 18, Min = 6 };

                    var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                        handaxe);
                    light.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";

                    handaxe.Properties.AddRange(new Property[] { thrown, light });

                    // Mastery property
                    var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, handaxe);
                    vex.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";

                    handaxe.MasteryProperty = vex;
                }
                {
                    var javelin = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Javelin,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    javelin.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Javelin}";
                    javelin.Type = simpleMeleeWeaponType;
                    javelin.NumOfDamageDice = 1;
                    javelin.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    javelin.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    javelin.Weight = 1.0f;
                    javelin.Cost = 500;

                    // Weapon properties
                    var thrown =
                        Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, javelin);
                    thrown.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                    thrown.Range = new Range() { Max = 36, Min = 9 };

                    javelin.Properties.AddRange(new Property[] { thrown });

                    // Mastery property
                    var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                        javelin);
                    slow.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                    slow.SlowBy = 3;

                    javelin.MasteryProperty = slow;
                }
                {
                    var lightHammer = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.LightHammer,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    lightHammer.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.LightHammer}";
                    lightHammer.Type = simpleMeleeWeaponType;
                    lightHammer.NumOfDamageDice = 1;
                    lightHammer.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    lightHammer.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    lightHammer.Weight = 1.0f;
                    lightHammer.Cost = 200;

                    // Weapon properties
                    var thrown =
                        Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown,
                            lightHammer);
                    thrown.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                    thrown.Range = new Range() { Max = 18, Min = 6 };

                    var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                        lightHammer);
                    light.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";

                    lightHammer.Properties.AddRange(new Property[] { thrown, light });

                    // Mastery property
                    var nick = Common.CreateScriptableObjectAndAddToObject<Nick>(NameHelper.MasteryProperty.Nick,
                        lightHammer);
                    nick.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}";

                    lightHammer.MasteryProperty = nick;
                }
                {
                    var mace = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Mace,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    mace.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Mace}";
                    mace.Type = simpleMeleeWeaponType;
                    mace.NumOfDamageDice = 1;
                    mace.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    mace.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    mace.Weight = 2.0f;
                    mace.Cost = 500;


                    // Mastery property
                    var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap, mace);
                    sap.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";

                    mace.MasteryProperty = sap;
                }
                {
                    var quarterstaff =
                        Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Quarterstaff,
                            PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    quarterstaff.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Quarterstaff}";
                    quarterstaff.Type = simpleMeleeWeaponType;
                    quarterstaff.NumOfDamageDice = 1;
                    quarterstaff.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    quarterstaff.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    quarterstaff.Weight = 2.0f;
                    quarterstaff.Cost = 200;

                    // Weapon properties
                    var versatile =
                        Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                            quarterstaff);
                    versatile.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                    versatile.NumberOfDice = 1;
                    versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D8));

                    quarterstaff.Properties.AddRange(new Property[] { versatile });

                    // Mastery property
                    var topple =
                        Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple,
                            quarterstaff);
                    topple.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";

                    quarterstaff.MasteryProperty = topple;
                }
                {
                    var sickle = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Sickle,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    sickle.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Sickle}";
                    sickle.Type = simpleMeleeWeaponType;
                    sickle.NumOfDamageDice = 1;
                    sickle.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    sickle.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    sickle.Weight = 1.0f;
                    sickle.Cost = 100;

                    // Weapon properties
                    var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                        sickle);
                    light.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";

                    sickle.Properties.AddRange(new Property[] { light });

                    // Mastery property
                    var nick = Common.CreateScriptableObjectAndAddToObject<Nick>(NameHelper.MasteryProperty.Nick,
                        sickle);
                    nick.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}";

                    sickle.MasteryProperty = nick;
                }
                {
                    var spear = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Spear,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    spear.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Spear}";
                    spear.Type = simpleMeleeWeaponType;
                    spear.NumOfDamageDice = 1;
                    spear.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    spear.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    spear.Weight = 1.5f;
                    spear.Cost = 100;

                    // Weapon properties
                    var thrown =
                        Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, spear);
                    thrown.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                    thrown.Range = new Range() { Max = 18, Min = 6 };

                    var versatile =
                        Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                            spear);
                    versatile.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                    versatile.NumberOfDice = 1;
                    versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D8));

                    spear.Properties.AddRange(new Property[] { thrown, versatile });

                    // Mastery property
                    var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap, spear);
                    sap.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";

                    spear.MasteryProperty = sap;
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void InitializeMartialRangedWeapons(WeaponType[] weaponTypes, Die[] dice,
            DamageType[] damageType, AmmunitionType[] ammunitionTypes)
        {
            try
            {
                AssetDatabase.StartAssetEditing();
                Common.EnsureFolderExists(PathHelper.Weapons.MartialRangedWeaponsPath);

                var martialRangedWeaponType =
                    weaponTypes.Single(x => x.name == NameHelper.WeaponTypes.MartialRangedWeapon);

                {
                    var blowgun = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.Blowgun,
                        PathHelper.Weapons.MartialRangedWeaponsPath);

                    blowgun.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.Blowgun}";
                    blowgun.Type = martialRangedWeaponType;
                    blowgun.NumOfDamageDice = 1;
                    blowgun.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D1));
                    blowgun.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    blowgun.Weight = 0.5f;
                    blowgun.Cost = 1000;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            blowgun);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Needles);
                    ammunition.Range = new Range() { Max = 32, Min = 8 };

                    var loading =
                        Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading,
                            blowgun);
                    loading.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";

                    blowgun.Properties.AddRange(new Property[] { ammunition, loading });

                    // Mastery property
                    var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, blowgun);
                    vex.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";

                    blowgun.MasteryProperty = vex;
                }
                {
                    var handCrossbow =
                        Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.HandCrossbow,
                            PathHelper.Weapons.MartialRangedWeaponsPath);

                    handCrossbow.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.HandCrossbow}";
                    handCrossbow.Type = martialRangedWeaponType;
                    handCrossbow.NumOfDamageDice = 1;
                    handCrossbow.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    handCrossbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    handCrossbow.Weight = 0.5f;
                    handCrossbow.Cost = 7500;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            handCrossbow);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Bolts);
                    ammunition.Range = new Range() { Max = 36, Min = 9 };

                    var loading =
                        Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading, handCrossbow);
                    loading.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";

                    var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light, handCrossbow);
                    light.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";

                    handCrossbow.Properties.AddRange(new Property[] { ammunition, loading, light });

                    // Mastery property
                    var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex,
                        handCrossbow);
                    vex.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";

                    handCrossbow.MasteryProperty = vex;
                }
                {
                    var heavyCrossbow =
                        Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.HeavyCrossbow,
                            PathHelper.Weapons.MartialRangedWeaponsPath);

                    heavyCrossbow.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.HeavyCrossbow}";
                    heavyCrossbow.Type = martialRangedWeaponType;
                    heavyCrossbow.NumOfDamageDice = 1;
                    heavyCrossbow.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    heavyCrossbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    heavyCrossbow.Weight = 9.0f;
                    heavyCrossbow.Cost = 5000;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            heavyCrossbow);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Bolts);
                    ammunition.Range = new Range() { Max = 120, Min = 30 };

                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        heavyCrossbow);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var loading =
                        Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading,
                            heavyCrossbow);
                    loading.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            heavyCrossbow);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    heavyCrossbow.Properties.AddRange(new Property[] { ammunition, heavy, loading, twoHanded });

                    // Mastery property
                    var push = Common.CreateScriptableObjectAndAddToObject<Push>(NameHelper.MasteryProperty.Push,
                        heavyCrossbow);
                    push.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}";
                    push.Distance = 3.0f;

                    heavyCrossbow.MasteryProperty = push;
                }
                {
                    var longbow = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.Longbow,
                        PathHelper.Weapons.MartialRangedWeaponsPath);

                    longbow.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.Longbow}";
                    longbow.Type = martialRangedWeaponType;
                    longbow.NumOfDamageDice = 1;
                    longbow.DamageDie = dice.Single(x => x.name == NameHelper.Dice.D8);
                    longbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    longbow.Weight = 1.0f;
                    longbow.Cost = 5000;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            longbow);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Arrows);
                    ammunition.Range = new Range() { Max = 180, Min = 45 };

                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        longbow);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            longbow);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    longbow.Properties.AddRange(new Property[] { ammunition, heavy, twoHanded });

                    // Mastery property
                    var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                        longbow);
                    slow.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                    slow.SlowBy = 3.0f;

                    longbow.MasteryProperty = slow;
                }
                {
                    var musket = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.Musket,
                        PathHelper.Weapons.MartialRangedWeaponsPath);

                    musket.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.Musket}";
                    musket.Type = martialRangedWeaponType;
                    musket.NumOfDamageDice = 1;
                    musket.DamageDie = dice.Single(x => x.name == NameHelper.Dice.D12);
                    musket.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    musket.Weight = 5.0f;
                    musket.Cost = 50000;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            musket);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.BulletsFirearm);
                    ammunition.Range = new Range() { Max = 36, Min = 12 };

                    var loading =
                        Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading, musket);
                    loading.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";

                    musket.Properties.AddRange(new Property[] { ammunition, loading });

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            musket);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                    
                    // Mastery property
                    var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                        musket);
                    slow.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                    slow.SlowBy = 3.0f;

                    musket.MasteryProperty = slow;
                }
                {
                    var pistol = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.Pistol,
                        PathHelper.Weapons.MartialRangedWeaponsPath);
                    pistol.Name = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.Pistol}";
                    pistol.Type = martialRangedWeaponType;
                    pistol.NumOfDamageDice = 1;
                    pistol.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    pistol.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    pistol.Weight = 1.5f;
                    pistol.Cost = 25000;

                    // Weapon properties
                    var ammunition =
                        Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                            pistol);
                    ammunition.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                    ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.BulletsFirearm);
                    ammunition.Range = new Range() { Max = 27, Min = 9 };

                    var loading =
                        Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading, pistol);
                    loading.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";

                    pistol.Properties.AddRange(new Property[] { ammunition, loading });

                    // Mastery property
                    var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, pistol);
                    vex.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";

                    pistol.MasteryProperty = vex;
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void InitializeMartialMeleeWeapons(WeaponType[] weaponTypes, Die[] dice, DamageType[] damageType,
            AmmunitionType[] ammunitionTypes)
        {
            try
            {
                AssetDatabase.StartAssetEditing();
                Common.EnsureFolderExists(PathHelper.Weapons.MartialMeleeWeaponsPath);

                var martialMeleeWeaponType =
                    weaponTypes.Single(x => x.name == NameHelper.WeaponTypes.MartialMeleeWeapon);

                {
                    var battleaxe = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Battleaxe,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    battleaxe.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Battleaxe}";
                    battleaxe.Type = martialMeleeWeaponType;
                    battleaxe.NumOfDamageDice = 1;
                    battleaxe.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    battleaxe.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    battleaxe.Weight = 2.0f;
                    battleaxe.Cost = 1000;

                    // Weapon properties
                    var versatile =
                        Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                            battleaxe);
                    versatile.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                    versatile.NumberOfDice = 1;
                    versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));

                    battleaxe.Properties.AddRange(new Property[] { versatile });

                    // Mastery property
                    var topple =
                        Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple,
                            battleaxe);
                    topple.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";

                    battleaxe.MasteryProperty = topple;
                }
                {
                    var flail = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Flail,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    flail.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Flail}";
                    flail.Type = martialMeleeWeaponType;
                    flail.NumOfDamageDice = 1;
                    flail.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    flail.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    flail.Weight = 1.0f;
                    flail.Cost = 1000;

                    // Mastery property
                    var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap, flail);
                    sap.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";

                    flail.MasteryProperty = sap;
                }
                {
                    var glaive = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Glaive,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    glaive.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Glaive}";
                    glaive.Type = martialMeleeWeaponType;
                    glaive.NumOfDamageDice = 1;
                    glaive.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    glaive.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    glaive.Weight = 3.0f;
                    glaive.Cost = 2000;

                    // Weapon properties
                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        glaive);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                        glaive);
                    reach.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";
                    reach.ExtraReach = 2.5f;

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            glaive);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    glaive.Properties.AddRange(new Property[] { heavy, reach, twoHanded });

                    // Mastery property
                    var graze = Common.CreateScriptableObjectAndAddToObject<Graze>(NameHelper.MasteryProperty.Graze,
                        glaive);
                    graze.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Graze}";;

                    glaive.MasteryProperty = graze;
                }
                {
                    var greataxe = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Greataxe,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    greataxe.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Greataxe}";
                    greataxe.Type = martialMeleeWeaponType;
                    greataxe.NumOfDamageDice = 1;
                    greataxe.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D12));
                    greataxe.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    greataxe.Weight = 3.5f;
                    greataxe.Cost = 3000;

                    // Weapon properties
                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        greataxe);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            greataxe);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    greataxe.Properties.AddRange(new Property[] { heavy, twoHanded });

                    // Mastery property
                    var cleave =
                        Common.CreateScriptableObjectAndAddToObject<Cleave>(NameHelper.MasteryProperty.Cleave,
                            greataxe);
                    cleave.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Cleave}";

                    greataxe.MasteryProperty = cleave;
                }
                {
                    var greatsword = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Greatsword,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    greatsword.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Greatsword}";
                    greatsword.Type = martialMeleeWeaponType;
                    greatsword.NumOfDamageDice = 2;
                    greatsword.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    greatsword.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    greatsword.Weight = 3.0f;
                    greatsword.Cost = 5000;

                    // Weapon properties
                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        greatsword);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            greatsword);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    greatsword.Properties.AddRange(new Property[] { heavy, twoHanded });

                    // Mastery property
                    var graze = Common.CreateScriptableObjectAndAddToObject<Graze>(NameHelper.MasteryProperty.Graze,
                        greatsword);
                    graze.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Graze}";

                    greatsword.MasteryProperty = graze;
                }
                {
                    var halberd = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Halberd,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    halberd.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Halberd}";
                    halberd.Type = martialMeleeWeaponType;
                    halberd.NumOfDamageDice = 1;
                    halberd.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    halberd.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    halberd.Weight = 3.0f;
                    halberd.Cost = 2000;

                    // Weapon properties
                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        halberd);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            halberd);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                        halberd);
                    reach.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";

                    halberd.Properties.AddRange(new Property[] { heavy, twoHanded, reach });

                    // Mastery property
                    var cleave =
                        Common.CreateScriptableObjectAndAddToObject<Cleave>(NameHelper.MasteryProperty.Cleave, halberd);
                    cleave.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Cleave}";

                    halberd.MasteryProperty = cleave;
                }
                {
                    var lance = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Lance,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    lance.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Lance}";
                    lance.Type = martialMeleeWeaponType;
                    lance.NumOfDamageDice = 1;
                    lance.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    lance.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    lance.Weight = 3.0f;
                    lance.Cost = 1000;

                    // Weapon properties
                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        lance);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            lance);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                        lance);
                    reach.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";
                    reach.ExtraReach = 2.5f;

                    lance.Properties.AddRange(new Property[] { heavy, twoHanded, reach });

                    // Mastery property
                    var topple =
                        Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple, lance);
                    topple.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";

                    lance.MasteryProperty = topple;
                }
                {
                    var longsword = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Longsword,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    longsword.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Longsword}";
                    longsword.Type = martialMeleeWeaponType;
                    longsword.NumOfDamageDice = 1;
                    longsword.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    longsword.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    longsword.Weight = 1.5f;
                    longsword.Cost = 1500;

                    // Weapon properties
                    var versatile =
                        Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                            longsword);
                    versatile.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                    versatile.NumberOfDice = 1;
                    versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));

                    longsword.Properties.AddRange(new Property[] { versatile });

                    // Mastery property
                    var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap,
                        longsword);
                    sap.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";

                    longsword.MasteryProperty = sap;
                }
                {
                    var maul = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Maul,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    maul.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Maul}";
                    maul.Type = martialMeleeWeaponType;
                    maul.NumOfDamageDice = 2;
                    maul.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    maul.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    maul.Weight = 5.0f;
                    maul.Cost = 1000;

                    // Weapon properties
                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        maul);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            maul);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    maul.Properties.AddRange(new Property[] { heavy, twoHanded });

                    // Mastery property
                    var topple =
                        Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple, maul);
                    topple.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";

                    maul.MasteryProperty = topple;
                }
                {
                    var morningstar = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Morningstar,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    morningstar.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Morningstar}";
                    morningstar.Type = martialMeleeWeaponType;
                    morningstar.NumOfDamageDice = 1;
                    morningstar.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    morningstar.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    morningstar.Weight = 2.0f;
                    morningstar.Cost = 1500;

                    // Mastery property
                    var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap,
                        morningstar);
                    sap.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";

                    morningstar.MasteryProperty = sap;
                }
                {
                    var pike = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Pike,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    pike.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Pike}";
                    pike.Type = martialMeleeWeaponType;
                    pike.NumOfDamageDice = 1;
                    pike.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    pike.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    pike.Weight = 9.0f;
                    pike.Cost = 500;

                    // Weapon properties
                    var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                        pike);
                    heavy.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";

                    var twoHanded =
                        Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                            pike);
                    twoHanded.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";

                    var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                        pike);
                    reach.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";
                    reach.ExtraReach = 2.5f;

                    pike.Properties.AddRange(new Property[] { heavy, twoHanded, reach });

                    // Mastery property
                    var push = Common.CreateScriptableObjectAndAddToObject<Push>(NameHelper.MasteryProperty.Push, pike);
                    push.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}";
                    push.Distance = 3.0f;

                    pike.MasteryProperty = push;
                }
                {
                    var rapier = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Rapier,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    rapier.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Rapier}";
                    rapier.Type = martialMeleeWeaponType;
                    rapier.NumOfDamageDice = 1;
                    rapier.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    rapier.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    rapier.Weight = 1.5f;
                    rapier.Cost = 25000;

                    // Weapon properties
                    var finesse =
                        Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse, rapier);
                    finesse.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";

                    rapier.Properties.AddRange(new Property[] { finesse });

                    // Mastery property
                    var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, rapier);
                    vex.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";

                    rapier.MasteryProperty = vex;
                }
                {
                    var scimitar = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Scimitar,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    scimitar.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Scimitar}";
                    scimitar.Type = martialMeleeWeaponType;
                    scimitar.NumOfDamageDice = 1;
                    scimitar.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    scimitar.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    scimitar.Weight = 1.05f;
                    scimitar.Cost = 2500;

                    // Weapon properties
                    var finesse =
                        Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse,
                            scimitar);
                    finesse.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";

                    var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                        scimitar);
                    light.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";

                    scimitar.Properties.AddRange(new Property[] { finesse, light });

                    // Mastery property
                    var nick = Common.CreateScriptableObjectAndAddToObject<Nick>(NameHelper.MasteryProperty.Nick,
                        scimitar);
                    nick.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}";

                    scimitar.MasteryProperty = nick;
                }
                {
                    var shortsword = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Shortsword,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    shortsword.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Shortsword}";
                    shortsword.Type = martialMeleeWeaponType;
                    shortsword.NumOfDamageDice = 1;
                    shortsword.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    shortsword.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    shortsword.Weight = 1.0f;
                    shortsword.Cost = 1000;

                    // Weapon properties
                    var finesse =
                        Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse,
                            shortsword);
                    finesse.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";

                    var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                        shortsword);
                    light.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";

                    shortsword.Properties.AddRange(new Property[] { finesse, light });

                    // Mastery property
                    var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex,
                        shortsword);
                    vex.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";

                    shortsword.MasteryProperty = vex;
                }
                {
                    var trident = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Trident,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    trident.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Trident}";
                    trident.Type = martialMeleeWeaponType;
                    trident.NumOfDamageDice = 1;
                    trident.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    trident.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    trident.Weight = 2.0f;
                    trident.Cost = 500;

                    // Weapon properties
                    var thrown =
                        Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, trident);
                    thrown.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                    thrown.Range = new Range() { Max = 18, Min = 6 };

                    var versatile =
                        Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                            trident);
                    versatile.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                    versatile.NumberOfDice = 1;
                    versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));

                    trident.Properties.AddRange(new Property[] { thrown, versatile });

                    // Mastery property
                    var topple =
                        Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple, trident);
                    topple.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";

                    trident.MasteryProperty = topple;
                }
                {
                    var warhammer = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Warhammer,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    warhammer.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Warhammer}";
                    warhammer.Type = martialMeleeWeaponType;
                    warhammer.NumOfDamageDice = 1;
                    warhammer.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    warhammer.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    warhammer.Weight = 2.5f;
                    warhammer.Cost = 1500;

                    // Weapon properties
                    var versatile =
                        Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                            warhammer);
                    versatile.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                    versatile.NumberOfDice = 1;
                    versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));

                    warhammer.Properties.AddRange(new Property[] { versatile });

                    // Mastery property
                    var push = Common.CreateScriptableObjectAndAddToObject<Push>(NameHelper.MasteryProperty.Push,
                        warhammer);
                    push.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}";
                    push.Distance = 3.0f;

                    warhammer.MasteryProperty = push;
                }
                {
                    var warpick = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.WarPick,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    warpick.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.WarPick}";
                    warpick.Type = martialMeleeWeaponType;
                    warpick.NumOfDamageDice = 1;
                    warpick.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    warpick.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    warpick.Weight = 1.0f;
                    warpick.Cost = 500;

                    // Weapon properties
                    var versatile =
                        Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                            warpick);
                    versatile.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                    versatile.NumberOfDice = 1;
                    versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));

                    warpick.Properties.AddRange(new Property[] { versatile });

                    // Mastery property
                    var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap, warpick);
                    sap.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";

                    warpick.MasteryProperty = sap;
                }
                {
                    var whip = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Whip,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    whip.Name = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Whip}";
                    whip.Type = martialMeleeWeaponType;
                    whip.NumOfDamageDice = 1;
                    whip.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    whip.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    whip.Weight = 1.5f;
                    whip.Cost = 200;

                    // Weapon properties
                    var finesse =
                        Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse, whip);
                    finesse.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";

                    var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                        whip);
                    reach.Name = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";

                    whip.Properties.AddRange(new Property[] { finesse, reach });

                    // Mastery property
                    var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow, whip);
                    slow.Name = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                    slow.SlowBy = 3.0f;

                    whip.MasteryProperty = slow;
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static WeaponType[] InitializeWeaponTypes()
        {
            var weaponTypes = new List<WeaponType>();

            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(PathHelper.Weapons.WeaponTypesPath);
                
                var martialMeleeWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.MartialMeleeWeapon, PathHelper.Weapons.WeaponTypesPath);
                martialMeleeWeapon.DisplayName = $"{nameof(NameHelper.WeaponTypes)}.{NameHelper.WeaponTypes.MartialMeleeWeapon}";
                weaponTypes.Add(martialMeleeWeapon);
            
                var simpleMeleeWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.SimpleMeleeWeapon, PathHelper.Weapons.WeaponTypesPath);
                simpleMeleeWeapon.DisplayName = $"{nameof(NameHelper.WeaponTypes)}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}";
                weaponTypes.Add(simpleMeleeWeapon);

                var martialRangedWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.MartialRangedWeapon, PathHelper.Weapons.WeaponTypesPath);
                martialRangedWeapon.DisplayName = $"{nameof(NameHelper.WeaponTypes)}.{NameHelper.WeaponTypes.MartialRangedWeapon}";
                weaponTypes.Add(martialRangedWeapon);
            
                var simpleRangedWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.SimpleRangedWeapon, PathHelper.Weapons.WeaponTypesPath);
                simpleRangedWeapon.DisplayName = $"{nameof(NameHelper.WeaponTypes)}.{NameHelper.WeaponTypes.SimpleRangedWeapon}";
                weaponTypes.Add(simpleRangedWeapon);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
            
            return weaponTypes.ToArray();
        }

        private static AmmunitionType[] InitializeAmmunitionTypes(Storage[] storage)
        {
            var ammunitionTypes = new List<AmmunitionType>();

            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(PathHelper.Weapons.AmmunitionTypesPath);
                
                var arrows = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Arrows, PathHelper.Weapons.AmmunitionTypesPath);
                arrows.DisplayName = $"{nameof(NameHelper.AmmunitionTypes)}.{NameHelper.AmmunitionTypes.Arrows}";
                arrows.Amount = 20;
                arrows.Weight = 0.5f;
                arrows.Cost = 1;
                arrows.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Quiver);
                ammunitionTypes.Add(arrows);
            
                var bolts = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Bolts, PathHelper.Weapons.AmmunitionTypesPath);
                bolts.DisplayName = $"{nameof(NameHelper.AmmunitionTypes)}.{NameHelper.AmmunitionTypes.Bolts}";
                bolts.Amount = 20;
                bolts.Weight = 0.75f;
                bolts.Cost = 1;
                bolts.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Case);
                ammunitionTypes.Add(bolts);

                var bulletsFirearm = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.BulletsFirearm, PathHelper.Weapons.AmmunitionTypesPath);
                bulletsFirearm.DisplayName = $"{nameof(NameHelper.AmmunitionTypes)}.{NameHelper.AmmunitionTypes.BulletsFirearm}";
                bulletsFirearm.Amount = 10;
                bulletsFirearm.Weight = 1f;
                bulletsFirearm.Cost = 3;
                bulletsFirearm.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);
                ammunitionTypes.Add(bulletsFirearm);

                var bulletsSling = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.BulletsSling, PathHelper.Weapons.AmmunitionTypesPath);
                bulletsSling.DisplayName = $"{nameof(NameHelper.AmmunitionTypes)}.{NameHelper.AmmunitionTypes.BulletsSling}";
                bulletsSling.Amount = 20;
                bulletsSling.Weight = 0.75f;
                bulletsSling.Cost = 4;
                bulletsSling.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);
                ammunitionTypes.Add(bulletsSling);

                var needles = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Needles, PathHelper.Weapons.AmmunitionTypesPath);
                needles.DisplayName = $"{nameof(NameHelper.AmmunitionTypes)}.{NameHelper.AmmunitionTypes.Needles}";
                needles.Amount = 50;
                needles.Weight = 0.5f;
                needles.Cost = 1;
                needles.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);
                ammunitionTypes.Add(needles);
            
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
            
            return ammunitionTypes.ToArray();
        }
    }
}