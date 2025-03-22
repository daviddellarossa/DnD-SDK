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
                    dart.DisplayName = $"{nameof(NameHelper.Weapons_SimpleRanged)}.{NameHelper.Weapons_SimpleRanged.Dart}";
                    dart.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleRangedWeapon}.{NameHelper.Weapons_SimpleRanged.Dart}.{NameHelper.Naming.Description}";
                    dart.Type = simpleRangedWeaponType;
                    dart.NumOfDamageDice = 1;
                    dart.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    dart.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    dart.Weight = 0.125f;
                    dart.Cost = 500;

                    // Weapon properties
                    {
                        var finesse =
                            Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse, dart);
                        finesse.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";
                        finesse.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(finesse);
                        
                        var thrown =
                            Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, dart);
                        thrown.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                        thrown.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}.{NameHelper.Naming.Description}";
                        thrown.Range = new Range() { Max = 18, Min = 6 };
                        
                        EditorUtility.SetDirty(finesse);

                        dart.Properties.AddRange(new Property[] { finesse, thrown });
                    }

                    // Mastery property
                    {
                        var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, dart);
                        vex.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";
                        vex.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(vex);

                        dart.MasteryProperty = vex;
                    }
                    
                    EditorUtility.SetDirty(dart);
                }

                {
                    var lightCrossbow =
                        Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleRanged.LightCrossbow,
                            PathHelper.Weapons.SimpleRangedWeaponsPath);
                    lightCrossbow.DisplayName = $"{nameof(NameHelper.Weapons_SimpleRanged)}.{NameHelper.Weapons_SimpleRanged.LightCrossbow}";
                    lightCrossbow.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleRangedWeapon}.{NameHelper.Weapons_SimpleRanged.LightCrossbow}.{NameHelper.Naming.Description}";
                    lightCrossbow.Type = simpleRangedWeaponType;
                    lightCrossbow.NumOfDamageDice = 1;
                    lightCrossbow.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    lightCrossbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    lightCrossbow.Weight = 2.5f;
                    lightCrossbow.Cost = 2500;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                lightCrossbow);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}.{NameHelper.Naming.Description}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Bolts);
                        ammunition.Range = new Range() { Max = 96, Min = 24 };
                        
                        EditorUtility.SetDirty(ammunition);

                        var loading =
                            Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading,
                                lightCrossbow);
                        loading.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";
                        loading.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(loading);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                lightCrossbow);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        lightCrossbow.Properties.AddRange(new Property[] { ammunition, loading, twoHanded });
                    }

                    // Mastery property
                    {
                        var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                            lightCrossbow);
                        slow.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                        slow.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(slow);

                        lightCrossbow.MasteryProperty = slow;
                    }
                    
                    EditorUtility.SetDirty(lightCrossbow);
                }

                {
                    var shortbow = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleRanged.Shortbow,
                        PathHelper.Weapons.SimpleRangedWeaponsPath);
                    shortbow.DisplayName = $"{nameof(NameHelper.Weapons_SimpleRanged)}.{NameHelper.Weapons_SimpleRanged.Shortbow}";
                    shortbow.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleRangedWeapon}.{NameHelper.Weapons_SimpleRanged.Shortbow}.{NameHelper.Naming.Description}";
                    shortbow.Type = simpleRangedWeaponType;
                    shortbow.NumOfDamageDice = 1;
                    shortbow.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    shortbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    shortbow.Weight = 0.5f;
                    shortbow.Cost = 2500;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                shortbow);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}.{NameHelper.Naming.Description}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Arrows);
                        ammunition.Range = new Range() { Max = 96, Min = 24 };
                        
                        EditorUtility.SetDirty(ammunition);


                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                shortbow);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        shortbow.Properties.AddRange(new Property[] { ammunition, twoHanded });
                    }

                    // Mastery property
                    {
                        var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex,
                            shortbow);
                        vex.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";
                        vex.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(vex);

                        shortbow.MasteryProperty = vex;
                    }
                    
                    EditorUtility.SetDirty(shortbow);
                }

                {
                    var sling = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleRanged.Sling,
                        PathHelper.Weapons.SimpleRangedWeaponsPath);
                    sling.DisplayName = $"{nameof(NameHelper.Weapons_SimpleRanged)}.{NameHelper.Weapons_SimpleRanged.Sling}";
                    sling.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleRangedWeapon}.{NameHelper.Weapons_SimpleRanged.Sling}.{NameHelper.Naming.Description}";
                    sling.Type = simpleRangedWeaponType;
                    sling.NumOfDamageDice = 1;
                    sling.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    sling.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    sling.Weight = 0.0f;
                    sling.Cost = 100;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                sling);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}.{NameHelper.Naming.Description}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.BulletsSling);
                        ammunition.Range = new Range() { Max = 36, Min = 9 };
                        
                        EditorUtility.SetDirty(ammunition);

                        sling.Properties.AddRange(new Property[] { ammunition });
                    }

                    // Mastery property
                    {
                        var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                            sling);
                        slow.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                        slow.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}.{NameHelper.Naming.Description}";
                        slow.SlowBy = 3;
                        
                        EditorUtility.SetDirty(slow);

                        sling.MasteryProperty = slow;
                    }
                    
                    EditorUtility.SetDirty(sling);
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
                    club.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Club}";
                    club.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Club}.{NameHelper.Naming.Description}";
                    club.Type = simpleMeleeWeaponType;
                    club.NumOfDamageDice = 1;
                    club.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    club.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    club.Weight = 1.0f;
                    club.Cost = 100;

                    // Weapon properties
                    {
                        var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                            club);
                        light.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";
                        light.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(light);

                        club.Properties.AddRange(new Property[] { light });
                    }

                    // Mastery property
                    {
                        var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow, club);
                        slow.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                        slow.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}.{NameHelper.Naming.Description}";
                        slow.SlowBy = 3;
                        
                        EditorUtility.SetDirty(slow);

                        club.MasteryProperty = slow;
                    }
                    
                    EditorUtility.SetDirty(club);
                }
                {
                    var dagger = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Dagger,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    dagger.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Dagger}";
                    dagger.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Dagger}.{NameHelper.Naming.Description}";
                    dagger.Type = simpleMeleeWeaponType;
                    dagger.NumOfDamageDice = 1;
                    dagger.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    dagger.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    dagger.Weight = 0.5f;
                    dagger.Cost = 200;

                    // Weapon properties
                    {
                        var thrown =
                            Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, dagger);
                        thrown.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                        thrown.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}.{NameHelper.Naming.Description}";
                        thrown.Range = new Range() { Max = 18, Min = 6 };
                        
                        EditorUtility.SetDirty(thrown);

                        var finesse =
                            Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse, dagger);
                        finesse.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";
                        finesse.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(finesse);

                        var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                            dagger);
                        light.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";
                        light.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(light);

                        dagger.Properties.AddRange(new Property[] { thrown, finesse, light });
                    }

                    // Mastery property
                    {
                        var nick = Common.CreateScriptableObjectAndAddToObject<Nick>(NameHelper.MasteryProperty.Nick,
                            dagger);
                        nick.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}";
                        nick.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(nick);

                        dagger.MasteryProperty = nick;
                    }
                    
                    EditorUtility.SetDirty(dagger);
                }
                {
                    var greatclub = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Greatclub,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    greatclub.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Greatclub}";
                    greatclub.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Greatclub}.{NameHelper.Naming.Description}";
                    greatclub.Type = simpleMeleeWeaponType;
                    greatclub.NumOfDamageDice = 1;
                    greatclub.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    greatclub.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    greatclub.Weight = 5.0f;
                    greatclub.Cost = 200;

                    // Weapon properties
                    {
                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                greatclub);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        greatclub.Properties.AddRange(new Property[] { twoHanded });
                    }

                    // Mastery property
                    {
                        var push = Common.CreateScriptableObjectAndAddToObject<Push>(NameHelper.MasteryProperty.Push,
                            greatclub);
                        push.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}";
                        push.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}.{NameHelper.Naming.Description}";
                        push.Distance = 3;
                        
                        EditorUtility.SetDirty(push);

                        greatclub.MasteryProperty = push;
                    }
                    
                    EditorUtility.SetDirty(greatclub);

                }
                {
                    var handaxe = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Handaxe,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    handaxe.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Handaxe}";
                    handaxe.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Handaxe}.{NameHelper.Naming.Description}";
                    handaxe.Type = simpleMeleeWeaponType;
                    handaxe.NumOfDamageDice = 1;
                    handaxe.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    handaxe.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    handaxe.Weight = 1.0f;
                    handaxe.Cost = 500;

                    // Weapon properties
                    {
                        var thrown =
                            Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, handaxe);
                        thrown.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                        thrown.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}.{NameHelper.Naming.Description}";
                        thrown.Range = new Range() { Max = 18, Min = 6 };
                        
                        EditorUtility.SetDirty(thrown);

                        var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                            handaxe);
                        light.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";
                        light.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(light);

                        handaxe.Properties.AddRange(new Property[] { thrown, light });
                    }

                    // Mastery property
                    {
                        var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, handaxe);
                        vex.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";
                        vex.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(vex);

                        handaxe.MasteryProperty = vex;
                    }
                    
                    EditorUtility.SetDirty(handaxe);
                }
                {
                    var javelin = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Javelin,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    javelin.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Javelin}";
                    javelin.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Javelin}.{NameHelper.Naming.Description}";
                    javelin.Type = simpleMeleeWeaponType;
                    javelin.NumOfDamageDice = 1;
                    javelin.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    javelin.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    javelin.Weight = 1.0f;
                    javelin.Cost = 500;

                    // Weapon properties
                    {
                        var thrown =
                            Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, javelin);
                        thrown.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                        thrown.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}.{NameHelper.Naming.Description}";
                        thrown.Range = new Range() { Max = 36, Min = 9 };
                        
                        EditorUtility.SetDirty(thrown);

                        javelin.Properties.AddRange(new Property[] { thrown });
                    }

                    // Mastery property
                    {
                        var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                            javelin);
                        slow.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                        slow.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}.{NameHelper.Naming.Description}";
                        slow.SlowBy = 3;
                        
                        EditorUtility.SetDirty(slow);

                        javelin.MasteryProperty = slow;
                    }
                    
                    EditorUtility.SetDirty(javelin);
                }
                {
                    var lightHammer = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.LightHammer,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    lightHammer.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.LightHammer}";
                    lightHammer.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.LightHammer}.{NameHelper.Naming.Description}";
                    lightHammer.Type = simpleMeleeWeaponType;
                    lightHammer.NumOfDamageDice = 1;
                    lightHammer.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    lightHammer.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    lightHammer.Weight = 1.0f;
                    lightHammer.Cost = 200;

                    // Weapon properties
                    {
                        var thrown =
                            Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown,
                                lightHammer);
                        thrown.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                        thrown.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}.{NameHelper.Naming.Description}";
                        thrown.Range = new Range() { Max = 18, Min = 6 };
                        
                        EditorUtility.SetDirty(thrown);

                        var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                            lightHammer);
                        light.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";
                        light.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(light);

                        lightHammer.Properties.AddRange(new Property[] { thrown, light });
                    }

                    // Mastery property
                    {
                        var nick = Common.CreateScriptableObjectAndAddToObject<Nick>(NameHelper.MasteryProperty.Nick,
                            lightHammer);
                        nick.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}";
                        nick.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}.{NameHelper.Naming.Description}";

                        lightHammer.MasteryProperty = nick;
                        
                        EditorUtility.SetDirty(lightHammer);
                    }
                    
                    EditorUtility.SetDirty(lightHammer);
                }
                {
                    var mace = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Mace,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    mace.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Mace}";
                    mace.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Mace}.{NameHelper.Naming.Description}";
                    mace.Type = simpleMeleeWeaponType;
                    mace.NumOfDamageDice = 1;
                    mace.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    mace.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    mace.Weight = 2.0f;
                    mace.Cost = 500;


                    // Mastery property
                    {
                        var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap, mace);
                        sap.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";
                        sap.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(sap);

                        mace.MasteryProperty = sap;
                    }
                    
                    EditorUtility.SetDirty(mace);
                }
                {
                    var quarterstaff =
                        Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Quarterstaff,
                            PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    quarterstaff.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Quarterstaff}";
                    quarterstaff.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Quarterstaff}.{NameHelper.Naming.Description}";
                    quarterstaff.Type = simpleMeleeWeaponType;
                    quarterstaff.NumOfDamageDice = 1;
                    quarterstaff.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    quarterstaff.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    quarterstaff.Weight = 2.0f;
                    quarterstaff.Cost = 200;

                    // Weapon properties
                    {
                        var versatile =
                            Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                                quarterstaff);
                        versatile.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                        versatile.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}.{NameHelper.Naming.Description}";
                        versatile.NumberOfDice = 1;
                        versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D8));
                        
                        EditorUtility.SetDirty(versatile);

                        quarterstaff.Properties.AddRange(new Property[] { versatile });
                    }

                    // Mastery property
                    {
                        var topple =
                            Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple,
                                quarterstaff);
                        topple.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";
                        topple.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(topple);

                        quarterstaff.MasteryProperty = topple;
                    }
                    
                    EditorUtility.SetDirty(quarterstaff);
                }
                {
                    var sickle = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Sickle,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    sickle.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Sickle}";
                    sickle.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Sickle}.{NameHelper.Naming.Description}";
                    sickle.Type = simpleMeleeWeaponType;
                    sickle.NumOfDamageDice = 1;
                    sickle.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    sickle.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    sickle.Weight = 1.0f;
                    sickle.Cost = 100;

                    // Weapon properties
                    {
                        var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                            sickle);
                        light.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";
                        light.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(light);

                        sickle.Properties.AddRange(new Property[] { light });
                    }

                    // Mastery property
                    {
                        var nick = Common.CreateScriptableObjectAndAddToObject<Nick>(NameHelper.MasteryProperty.Nick,
                            sickle);
                        nick.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}";
                        nick.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(nick);

                        sickle.MasteryProperty = nick;
                    }
                    
                    EditorUtility.SetDirty(sickle);
                }
                {
                    var spear = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_SimpleMelee.Spear,
                        PathHelper.Weapons.SimpleMeleeWeaponsPath);
                    spear.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_SimpleMelee.Spear}";
                    spear.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Weapons_SimpleMelee.Spear}.{NameHelper.Naming.Description}";
                    spear.Type = simpleMeleeWeaponType;
                    spear.NumOfDamageDice = 1;
                    spear.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    spear.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    spear.Weight = 1.5f;
                    spear.Cost = 100;

                    // Weapon properties
                    {
                        var thrown =
                            Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, spear);
                        thrown.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                        thrown.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}.{NameHelper.Naming.Description}";
                        thrown.Range = new Range() { Max = 18, Min = 6 };
                        
                        EditorUtility.SetDirty(thrown);

                        var versatile =
                            Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                                spear);
                        versatile.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                        versatile.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}.{NameHelper.Naming.Description}";
                        versatile.NumberOfDice = 1;
                        versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D8));
                        
                        EditorUtility.SetDirty(versatile);

                        spear.Properties.AddRange(new Property[] { thrown, versatile });
                    }

                    // Mastery property
                    {
                        var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap, spear);
                        sap.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";
                        sap.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(sap);

                        spear.MasteryProperty = sap;
                    }
                    
                    EditorUtility.SetDirty(spear);
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

                    blowgun.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.Blowgun}";
                    blowgun.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialRangedWeapon}.{NameHelper.Weapons_MartialRanged.Blowgun}.{NameHelper.Naming.Description}";
                    blowgun.Type = martialRangedWeaponType;
                    blowgun.NumOfDamageDice = 1;
                    blowgun.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D1));
                    blowgun.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    blowgun.Weight = 0.5f;
                    blowgun.Cost = 1000;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                blowgun);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}.{NameHelper.Naming.Description}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Needles);
                        ammunition.Range = new Range() { Max = 32, Min = 8 };
                        
                        EditorUtility.SetDirty(ammunition);

                        var loading =
                            Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading,
                                blowgun);
                        loading.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";
                        loading.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(loading);

                        blowgun.Properties.AddRange(new Property[] { ammunition, loading });
                    }

                    // Mastery property
                    {
                        var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, blowgun);
                        vex.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";
                        vex.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(vex);

                        blowgun.MasteryProperty = vex;
                    }
                    
                    EditorUtility.SetDirty(blowgun);
                }
                {
                    var handCrossbow =
                        Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.HandCrossbow,
                            PathHelper.Weapons.MartialRangedWeaponsPath);

                    handCrossbow.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.HandCrossbow}";
                    handCrossbow.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialRangedWeapon}.{NameHelper.Weapons_MartialRanged.HandCrossbow}.{NameHelper.Naming.Description}";
                    handCrossbow.Type = martialRangedWeaponType;
                    handCrossbow.NumOfDamageDice = 1;
                    handCrossbow.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    handCrossbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    handCrossbow.Weight = 0.5f;
                    handCrossbow.Cost = 7500;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                handCrossbow);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}.{NameHelper.Naming.Description}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Bolts);
                        ammunition.Range = new Range() { Max = 36, Min = 9 };
                        
                        EditorUtility.SetDirty(ammunition);

                        var loading =
                            Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading, handCrossbow);
                        loading.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";
                        loading.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(loading);

                        var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light, handCrossbow);
                        light.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";
                        light.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(light);

                        handCrossbow.Properties.AddRange(new Property[] { ammunition, loading, light });
                    }

                    // Mastery property
                    {
                        var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex,
                            handCrossbow);
                        vex.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";
                        vex.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(vex);

                        handCrossbow.MasteryProperty = vex;
                    }
                    
                    EditorUtility.SetDirty(handCrossbow);
                }
                {
                    var heavyCrossbow =
                        Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.HeavyCrossbow,
                            PathHelper.Weapons.MartialRangedWeaponsPath);

                    heavyCrossbow.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.HeavyCrossbow}";
                    heavyCrossbow.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialRangedWeapon}.{NameHelper.Weapons_MartialRanged.HeavyCrossbow}.{NameHelper.Naming.Description}";
                    heavyCrossbow.Type = martialRangedWeaponType;
                    heavyCrossbow.NumOfDamageDice = 1;
                    heavyCrossbow.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    heavyCrossbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    heavyCrossbow.Weight = 9.0f;
                    heavyCrossbow.Cost = 5000;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                heavyCrossbow);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Bolts);
                        ammunition.Range = new Range() { Max = 120, Min = 30 };
                        
                        EditorUtility.SetDirty(ammunition);

                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            heavyCrossbow);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var loading =
                            Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading,
                                heavyCrossbow);
                        loading.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";
                        loading.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(loading);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                heavyCrossbow);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        heavyCrossbow.Properties.AddRange(new Property[] { ammunition, heavy, loading, twoHanded });
                    }

                    // Mastery property
                    {
                        var push = Common.CreateScriptableObjectAndAddToObject<Push>(NameHelper.MasteryProperty.Push,
                            heavyCrossbow);
                        push.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}";
                        push.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}.{NameHelper.Naming.Description}";
                        push.Distance = 3.0f;
                        
                        EditorUtility.SetDirty(push);

                        heavyCrossbow.MasteryProperty = push;
                    }
                    
                    EditorUtility.SetDirty(heavyCrossbow);
                }
                {
                    var longbow = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.Longbow,
                        PathHelper.Weapons.MartialRangedWeaponsPath);

                    longbow.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.Longbow}";
                    longbow.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialRangedWeapon}.{NameHelper.Weapons_MartialRanged.Longbow}.{NameHelper.Naming.Description}";
                    longbow.Type = martialRangedWeaponType;
                    longbow.NumOfDamageDice = 1;
                    longbow.DamageDie = dice.Single(x => x.name == NameHelper.Dice.D8);
                    longbow.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    longbow.Weight = 1.0f;
                    longbow.Cost = 5000;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                longbow);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}.{NameHelper.Naming.Description}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.Arrows);
                        ammunition.Range = new Range() { Max = 180, Min = 45 };
                        
                        EditorUtility.SetDirty(ammunition);

                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            longbow);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                longbow);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        longbow.Properties.AddRange(new Property[] { ammunition, heavy, twoHanded });
                    }

                    // Mastery property
                    {
                        var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                            longbow);
                        slow.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                        slow.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}.{NameHelper.Naming.Description}";
                        slow.SlowBy = 3.0f;
                        
                        EditorUtility.SetDirty(slow);

                        longbow.MasteryProperty = slow;
                    }
                    
                    EditorUtility.SetDirty(longbow);
                }
                {
                    var musket = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.Musket,
                        PathHelper.Weapons.MartialRangedWeaponsPath);

                    musket.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.Musket}";
                    musket.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialRangedWeapon}.{NameHelper.Weapons_MartialRanged.Musket}.{NameHelper.Naming.Description}";
                    musket.Type = martialRangedWeaponType;
                    musket.NumOfDamageDice = 1;
                    musket.DamageDie = dice.Single(x => x.name == NameHelper.Dice.D12);
                    musket.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    musket.Weight = 5.0f;
                    musket.Cost = 50000;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                musket);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}.{NameHelper.Naming.Description}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.BulletsFirearm);
                        ammunition.Range = new Range() { Max = 36, Min = 12 };
                        
                        EditorUtility.SetDirty(ammunition);

                        var loading =
                            Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading, musket);
                        loading.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";
                        loading.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(loading);
                        
                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                musket);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);
                        
                        musket.Properties.AddRange(new Property[] { ammunition, loading, twoHanded });

                    }
                    
                    // Mastery property
                    {
                        var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow,
                            musket);
                        slow.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                        slow.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}.{NameHelper.Naming.Description}";
                        slow.SlowBy = 3.0f;
                        
                        EditorUtility.SetDirty(slow);

                        musket.MasteryProperty = slow;
                    }
                    
                    EditorUtility.SetDirty(musket);
                }
                {
                    var pistol = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialRanged.Pistol,
                        PathHelper.Weapons.MartialRangedWeaponsPath);
                    pistol.DisplayName = $"{nameof(NameHelper.Weapons_SimpleMelee)}.{NameHelper.Weapons_MartialRanged.Pistol}";
                    pistol.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialRangedWeapon}.{NameHelper.Weapons_MartialRanged.Pistol}.{NameHelper.Naming.Description}";
                    pistol.Type = martialRangedWeaponType;
                    pistol.NumOfDamageDice = 1;
                    pistol.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    pistol.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    pistol.Weight = 1.5f;
                    pistol.Cost = 25000;

                    // Weapon properties
                    {
                        var ammunition =
                            Common.CreateScriptableObjectAndAddToObject<Ammunition>(NameHelper.WeaponProperty.Ammunition,
                                pistol);
                        ammunition.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}";
                        ammunition.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Ammunition}.{NameHelper.Naming.Description}";
                        ammunition.Type = ammunitionTypes.Single(x => x.name == NameHelper.AmmunitionTypes.BulletsFirearm);
                        ammunition.Range = new Range() { Max = 27, Min = 9 };
                        
                        EditorUtility.SetDirty(ammunition);

                        var loading =
                            Common.CreateScriptableObjectAndAddToObject<Loading>(NameHelper.WeaponProperty.Loading, pistol);
                        loading.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}";
                        loading.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Loading}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(loading);

                        pistol.Properties.AddRange(new Property[] { ammunition, loading });
                    }

                    // Mastery property
                    {
                        var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, pistol);
                        vex.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";
                        vex.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(vex);

                        pistol.MasteryProperty = vex;
                    }
                    
                    EditorUtility.SetDirty(pistol);
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
                    battleaxe.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Battleaxe}";
                    battleaxe.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Battleaxe}.{NameHelper.Naming.Description}";
                    battleaxe.Type = martialMeleeWeaponType;
                    battleaxe.NumOfDamageDice = 1;
                    battleaxe.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    battleaxe.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    battleaxe.Weight = 2.0f;
                    battleaxe.Cost = 1000;

                    // Weapon properties
                    {
                        var versatile =
                            Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                                battleaxe);
                        versatile.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                        versatile.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}.{NameHelper.Naming.Description}";
                        versatile.NumberOfDice = 1;
                        versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));
                        
                        EditorUtility.SetDirty(versatile);

                        battleaxe.Properties.AddRange(new Property[] { versatile });
                    }

                    // Mastery property
                    {
                        var topple =
                            Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple,
                                battleaxe);
                        topple.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";
                        topple.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(topple);

                        battleaxe.MasteryProperty = topple;
                    }
                    
                    EditorUtility.SetDirty(battleaxe);
                }
                {
                    var flail = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Flail,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    flail.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Flail}";
                    flail.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Flail}.{NameHelper.Naming.Description}";
                    flail.Type = martialMeleeWeaponType;
                    flail.NumOfDamageDice = 1;
                    flail.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    flail.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    flail.Weight = 1.0f;
                    flail.Cost = 1000;

                    // Mastery property
                    {
                        var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap, flail);
                        sap.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";
                        sap.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(sap);

                        flail.MasteryProperty = sap;
                    }
                    
                    EditorUtility.SetDirty(flail);
                }
                {
                    var glaive = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Glaive,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    glaive.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Glaive}";
                    glaive.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Glaive}.{NameHelper.Naming.Description}";
                    glaive.Type = martialMeleeWeaponType;
                    glaive.NumOfDamageDice = 1;
                    glaive.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    glaive.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    glaive.Weight = 3.0f;
                    glaive.Cost = 2000;

                    // Weapon properties
                    {
                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            glaive);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                            glaive);
                        reach.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";
                        reach.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}.{NameHelper.Naming.Description}";
                        reach.ExtraReach = 2.5f;
                        
                        EditorUtility.SetDirty(reach);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                glaive);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        glaive.Properties.AddRange(new Property[] { heavy, reach, twoHanded });
                    }

                    // Mastery property
                    {
                        var graze = Common.CreateScriptableObjectAndAddToObject<Graze>(NameHelper.MasteryProperty.Graze,
                            glaive);
                        graze.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Graze}";
                        graze.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Graze}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(graze);

                        glaive.MasteryProperty = graze;
                    }
                    
                    EditorUtility.SetDirty(glaive);
                }
                {
                    var greataxe = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Greataxe,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    greataxe.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Greataxe}";
                    greataxe.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Greataxe}.{NameHelper.Naming.Description}";
                    greataxe.Type = martialMeleeWeaponType;
                    greataxe.NumOfDamageDice = 1;
                    greataxe.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D12));
                    greataxe.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    greataxe.Weight = 3.5f;
                    greataxe.Cost = 3000;

                    // Weapon properties
                    {
                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            greataxe);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                greataxe);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        greataxe.Properties.AddRange(new Property[] { heavy, twoHanded });
                    }

                    // Mastery property
                    {
                        var cleave =
                            Common.CreateScriptableObjectAndAddToObject<Cleave>(NameHelper.MasteryProperty.Cleave,
                                greataxe);
                        cleave.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Cleave}";
                        cleave.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Cleave}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(cleave);

                        greataxe.MasteryProperty = cleave;
                    }
                    
                    EditorUtility.SetDirty(greataxe);
                }
                {
                    var greatsword = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Greatsword,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    greatsword.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Greatsword}";
                    greatsword.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Greatsword}.{NameHelper.Naming.Description}";
                    greatsword.Type = martialMeleeWeaponType;
                    greatsword.NumOfDamageDice = 2;
                    greatsword.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    greatsword.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    greatsword.Weight = 3.0f;
                    greatsword.Cost = 5000;

                    // Weapon properties
                    {
                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            greatsword);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                greatsword);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        greatsword.Properties.AddRange(new Property[] { heavy, twoHanded });
                    }

                    // Mastery property
                    {
                        var graze = Common.CreateScriptableObjectAndAddToObject<Graze>(NameHelper.MasteryProperty.Graze,
                            greatsword);
                        graze.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Graze}";
                        graze.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Graze}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(graze);

                        greatsword.MasteryProperty = graze;
                    }
                    
                    EditorUtility.SetDirty(greatsword);
                }
                {
                    var halberd = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Halberd,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    halberd.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Halberd}";
                    halberd.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Halberd}.{NameHelper.Naming.Description}";
                    halberd.Type = martialMeleeWeaponType;
                    halberd.NumOfDamageDice = 1;
                    halberd.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    halberd.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    halberd.Weight = 3.0f;
                    halberd.Cost = 2000;

                    // Weapon properties
                    {
                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            halberd);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                halberd);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                            halberd);
                        reach.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";
                        reach.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(reach);

                        halberd.Properties.AddRange(new Property[] { heavy, twoHanded, reach });
                    }

                    // Mastery property
                    {
                        var cleave =
                            Common.CreateScriptableObjectAndAddToObject<Cleave>(NameHelper.MasteryProperty.Cleave, halberd);
                        cleave.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Cleave}";
                        cleave.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Cleave}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(cleave);

                        halberd.MasteryProperty = cleave;
                    }
                    
                    EditorUtility.SetDirty(halberd);
                }
                {
                    var lance = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Lance,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    lance.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Lance}";
                    lance.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Lance}.{NameHelper.Naming.Description}";
                    lance.Type = martialMeleeWeaponType;
                    lance.NumOfDamageDice = 1;
                    lance.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    lance.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    lance.Weight = 3.0f;
                    lance.Cost = 1000;

                    // Weapon properties
                    {
                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            lance);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                lance);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                            lance);
                        reach.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";
                        reach.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}.{NameHelper.Naming.Description}";
                        reach.ExtraReach = 2.5f;
                        
                        EditorUtility.SetDirty(reach);

                        lance.Properties.AddRange(new Property[] { heavy, twoHanded, reach });
                    }

                    // Mastery property
                    {
                        var topple =
                            Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple, lance);
                        topple.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";
                        topple.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(topple);

                        lance.MasteryProperty = topple;
                    }
                    
                    EditorUtility.SetDirty(lance);
                }
                {
                    var longsword = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Longsword,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    longsword.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Longsword}";
                    longsword.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Longsword}.{NameHelper.Naming.Description}";
                    longsword.Type = martialMeleeWeaponType;
                    longsword.NumOfDamageDice = 1;
                    longsword.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    longsword.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    longsword.Weight = 1.5f;
                    longsword.Cost = 1500;

                    // Weapon properties
                    {
                        var versatile =
                            Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                                longsword);
                        versatile.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                        versatile.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}.{NameHelper.Naming.Description}";
                        versatile.NumberOfDice = 1;
                        versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));
                        
                        EditorUtility.SetDirty(versatile);

                        longsword.Properties.AddRange(new Property[] { versatile });
                    }

                    // Mastery property
                    {
                        var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap,
                            longsword);
                        sap.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";
                        sap.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(sap);

                        longsword.MasteryProperty = sap;
                    }
                    
                    EditorUtility.SetDirty(longsword);
                }
                {
                    var maul = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Maul,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    maul.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Maul}";
                    maul.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Maul}.{NameHelper.Naming.Description}";
                    maul.Type = martialMeleeWeaponType;
                    maul.NumOfDamageDice = 2;
                    maul.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    maul.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    maul.Weight = 5.0f;
                    maul.Cost = 1000;

                    // Weapon properties
                    {
                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            maul);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                maul);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        maul.Properties.AddRange(new Property[] { heavy, twoHanded });
                    }

                    // Mastery property
                    {
                        var topple =
                            Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple, maul);
                        topple.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";
                        topple.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(topple);

                        maul.MasteryProperty = topple;
                    }
                    
                    EditorUtility.SetDirty(maul);
                }
                {
                    var morningstar = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Morningstar,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    morningstar.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Morningstar}";
                    morningstar.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Morningstar}.{NameHelper.Naming.Description}";
                    morningstar.Type = martialMeleeWeaponType;
                    morningstar.NumOfDamageDice = 1;
                    morningstar.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    morningstar.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    morningstar.Weight = 2.0f;
                    morningstar.Cost = 1500;

                    // Mastery property
                    {
                        var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap,
                            morningstar);
                        sap.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";
                        sap.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(sap);

                        morningstar.MasteryProperty = sap;
                    }
                    
                    EditorUtility.SetDirty(morningstar);
                }
                {
                    var pike = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Pike,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    pike.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Pike}";
                    pike.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Pike}.{NameHelper.Naming.Description}";
                    pike.Type = martialMeleeWeaponType;
                    pike.NumOfDamageDice = 1;
                    pike.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D10));
                    pike.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    pike.Weight = 9.0f;
                    pike.Cost = 500;

                    // Weapon properties
                    {
                        var heavy = Common.CreateScriptableObjectAndAddToObject<Heavy>(NameHelper.WeaponProperty.Heavy,
                            pike);
                        heavy.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}";
                        heavy.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Heavy}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(heavy);

                        var twoHanded =
                            Common.CreateScriptableObjectAndAddToObject<TwoHanded>(NameHelper.WeaponProperty.TwoHanded,
                                pike);
                        twoHanded.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}";
                        twoHanded.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.TwoHanded}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(twoHanded);

                        var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                            pike);
                        reach.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";
                        reach.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}.{NameHelper.Naming.Description}";
                        reach.ExtraReach = 2.5f;
                        
                        EditorUtility.SetDirty(reach);

                        pike.Properties.AddRange(new Property[] { heavy, twoHanded, reach });
                    }

                    // Mastery property
                    {
                        var push = Common.CreateScriptableObjectAndAddToObject<Push>(NameHelper.MasteryProperty.Push, pike);
                        push.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}";
                        push.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}.{NameHelper.Naming.Description}";
                        push.Distance = 3.0f;
                        
                        EditorUtility.SetDirty(push);

                        pike.MasteryProperty = push;
                    }
                    
                    EditorUtility.SetDirty(pike);
                }
                {
                    var rapier = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Rapier,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    rapier.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Rapier}";
                    rapier.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Rapier}.{NameHelper.Naming.Description}";
                    rapier.Type = martialMeleeWeaponType;
                    rapier.NumOfDamageDice = 1;
                    rapier.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    rapier.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    rapier.Weight = 1.5f;
                    rapier.Cost = 25000;

                    // Weapon properties
                    {
                        var finesse =
                            Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse, rapier);
                        finesse.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";
                        finesse.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(finesse);

                        rapier.Properties.AddRange(new Property[] { finesse });
                    }

                    // Mastery property
                    {
                        var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex, rapier);
                        vex.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";
                        vex.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(vex);

                        rapier.MasteryProperty = vex;
                    }
                    
                    EditorUtility.SetDirty(rapier);
                }
                {
                    var scimitar = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Scimitar,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    scimitar.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Scimitar}";
                    scimitar.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Scimitar}.{NameHelper.Naming.Description}";
                    scimitar.Type = martialMeleeWeaponType;
                    scimitar.NumOfDamageDice = 1;
                    scimitar.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    scimitar.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    scimitar.Weight = 1.05f;
                    scimitar.Cost = 2500;

                    // Weapon properties
                    {
                        var finesse =
                            Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse,
                                scimitar);
                        finesse.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";
                        finesse.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(finesse);

                        var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                            scimitar);
                        light.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";
                        light.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(light);

                        scimitar.Properties.AddRange(new Property[] { finesse, light });
                    }

                    // Mastery property
                    {
                        var nick = Common.CreateScriptableObjectAndAddToObject<Nick>(NameHelper.MasteryProperty.Nick,
                            scimitar);
                        nick.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}";
                        nick.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Nick}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(nick);

                        scimitar.MasteryProperty = nick;
                    }
                    
                    EditorUtility.SetDirty(scimitar);
                }
                {
                    var shortsword = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Shortsword,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    shortsword.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Shortsword}";
                    shortsword.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Shortsword}.{NameHelper.Naming.Description}";
                    shortsword.Type = martialMeleeWeaponType;
                    shortsword.NumOfDamageDice = 1;
                    shortsword.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D6));
                    shortsword.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    shortsword.Weight = 1.0f;
                    shortsword.Cost = 1000;

                    // Weapon properties
                    {
                        var finesse =
                            Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse,
                                shortsword);
                        finesse.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";
                        finesse.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(finesse);

                        var light = Common.CreateScriptableObjectAndAddToObject<Light>(NameHelper.WeaponProperty.Light,
                            shortsword);
                        light.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}";
                        light.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Light}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(light);

                        shortsword.Properties.AddRange(new Property[] { finesse, light });
                    }

                    // Mastery property
                    {
                        var vex = Common.CreateScriptableObjectAndAddToObject<Vex>(NameHelper.MasteryProperty.Vex,
                            shortsword);
                        vex.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}";
                        vex.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Vex}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(vex);

                        shortsword.MasteryProperty = vex;
                    }
                    
                    EditorUtility.SetDirty(shortsword);
                }
                {
                    var trident = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Trident,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    trident.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Trident}";
                    trident.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Trident}.{NameHelper.Naming.Description}";
                    trident.Type = martialMeleeWeaponType;
                    trident.NumOfDamageDice = 1;
                    trident.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    trident.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    trident.Weight = 2.0f;
                    trident.Cost = 500;

                    // Weapon properties
                    {
                        var thrown =
                            Common.CreateScriptableObjectAndAddToObject<Thrown>(NameHelper.WeaponProperty.Thrown, trident);
                        thrown.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}";
                        thrown.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Thrown}.{NameHelper.Naming.Description}";
                        thrown.Range = new Range() { Max = 18, Min = 6 };
                        
                        EditorUtility.SetDirty(thrown);

                        var versatile =
                            Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                                trident);
                        versatile.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                        versatile.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}.{NameHelper.Naming.Description}";
                        versatile.NumberOfDice = 1;
                        versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));
                        
                        EditorUtility.SetDirty(versatile);

                        trident.Properties.AddRange(new Property[] { thrown, versatile });
                    }

                    // Mastery property
                    {
                        var topple =
                            Common.CreateScriptableObjectAndAddToObject<Topple>(NameHelper.MasteryProperty.Topple, trident);
                        topple.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}";
                        topple.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Topple}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(topple);

                        trident.MasteryProperty = topple;
                    }
                    
                    EditorUtility.SetDirty(trident);
                }
                {
                    var warhammer = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Warhammer,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    warhammer.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Warhammer}";
                    warhammer.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Warhammer}.{NameHelper.Naming.Description}";
                    warhammer.Type = martialMeleeWeaponType;
                    warhammer.NumOfDamageDice = 1;
                    warhammer.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    warhammer.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Bludgeoning);
                    warhammer.Weight = 2.5f;
                    warhammer.Cost = 1500;

                    // Weapon properties
                    {
                        var versatile =
                            Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                                warhammer);
                        versatile.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                        versatile.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}.{NameHelper.Naming.Description}";
                        versatile.NumberOfDice = 1;
                        versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));
                        
                        EditorUtility.SetDirty(versatile);

                        warhammer.Properties.AddRange(new Property[] { versatile });
                    }

                    // Mastery property
                    {
                        var push = Common.CreateScriptableObjectAndAddToObject<Push>(NameHelper.MasteryProperty.Push,
                            warhammer);
                        push.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}";
                        push.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Push}.{NameHelper.Naming.Description}";
                        push.Distance = 3.0f;
                        
                        EditorUtility.SetDirty(push);

                        warhammer.MasteryProperty = push;
                    }
                    
                    EditorUtility.SetDirty(warhammer);
                }
                {
                    var warpick = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.WarPick,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    warpick.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.WarPick}";
                    warpick.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.WarPick}.{NameHelper.Naming.Description}";
                    warpick.Type = martialMeleeWeaponType;
                    warpick.NumOfDamageDice = 1;
                    warpick.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D8));
                    warpick.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Piercing);
                    warpick.Weight = 1.0f;
                    warpick.Cost = 500;

                    // Weapon properties
                    {
                        var versatile =
                            Common.CreateScriptableObjectAndAddToObject<Versatile>(NameHelper.WeaponProperty.Versatile,
                                warpick);
                        versatile.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}";
                        versatile.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Versatile}.{NameHelper.Naming.Description}";
                        versatile.NumberOfDice = 1;
                        versatile.DieType = dice.Single((x => x.name == NameHelper.Dice.D10));
                        
                        EditorUtility.SetDirty(versatile);

                        warpick.Properties.AddRange(new Property[] { versatile });
                    }

                    // Mastery property
                    {
                        var sap = Common.CreateScriptableObjectAndAddToObject<Sap>(NameHelper.MasteryProperty.Sap, warpick);
                        sap.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}";
                        sap.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Sap}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(sap);

                        warpick.MasteryProperty = sap;
                    }
                    
                    EditorUtility.SetDirty(warpick);
                }
                {
                    var whip = Common.CreateScriptableObject<Weapon>(NameHelper.Weapons_MartialMelee.Whip,
                        PathHelper.Weapons.MartialMeleeWeaponsPath);
                    whip.DisplayName = $"{nameof(NameHelper.Weapons_MartialMelee)}.{NameHelper.Weapons_MartialMelee.Whip}";
                    whip.DisplayDescription = $"{NameHelper.Naming.Weapons}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Weapons_MartialMelee.Whip}.{NameHelper.Naming.Description}";
                    whip.Type = martialMeleeWeaponType;
                    whip.NumOfDamageDice = 1;
                    whip.DamageDie = dice.Single((x => x.name == NameHelper.Dice.D4));
                    whip.DamageType = damageType.Single(x => x.name == NameHelper.DamageTypes.Slashing);
                    whip.Weight = 1.5f;
                    whip.Cost = 200;

                    // Weapon properties
                    {
                        var finesse =
                            Common.CreateScriptableObjectAndAddToObject<Finesse>(NameHelper.WeaponProperty.Finesse, whip);
                        finesse.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}";
                        finesse.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Finesse}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(finesse);

                        var reach = Common.CreateScriptableObjectAndAddToObject<Reach>(NameHelper.WeaponProperty.Reach,
                            whip);
                        reach.DisplayName = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}";
                        reach.DisplayDescription = $"{nameof(NameHelper.WeaponProperty)}.{NameHelper.WeaponProperty.Reach}.{NameHelper.Naming.Description}";
                        
                        EditorUtility.SetDirty(reach);

                        whip.Properties.AddRange(new Property[] { finesse, reach });
                    }

                    // Mastery property
                    {
                        var slow = Common.CreateScriptableObjectAndAddToObject<Slow>(NameHelper.MasteryProperty.Slow, whip);
                        slow.DisplayName = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}";
                        slow.DisplayDescription = $"{nameof(NameHelper.MasteryProperty)}.{NameHelper.MasteryProperty.Slow}.{NameHelper.Naming.Description}";
                        slow.SlowBy = 3.0f;
                        
                        EditorUtility.SetDirty(slow);

                        whip.MasteryProperty = slow;
                    }
                    
                    EditorUtility.SetDirty(whip);
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

                {
                    var martialMeleeWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.MartialMeleeWeapon, PathHelper.Weapons.WeaponTypesPath);
                    martialMeleeWeapon.DisplayName = $"{NameHelper.Naming.WeaponTypes}.{NameHelper.WeaponTypes.MartialMeleeWeapon}";
                    martialMeleeWeapon.DisplayDescription = $"{NameHelper.Naming.WeaponTypes}.{NameHelper.WeaponTypes.MartialMeleeWeapon}.{NameHelper.Naming.Description}";
                    weaponTypes.Add(martialMeleeWeapon);
                    
                    EditorUtility.SetDirty(martialMeleeWeapon);
                }

                {
                    var simpleMeleeWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.SimpleMeleeWeapon, PathHelper.Weapons.WeaponTypesPath);
                    simpleMeleeWeapon.DisplayName = $"{NameHelper.Naming.WeaponTypes}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}";
                    simpleMeleeWeapon.DisplayDescription = $"{NameHelper.Naming.WeaponTypes}.{NameHelper.WeaponTypes.SimpleMeleeWeapon}.{NameHelper.Naming.Description}";
                    weaponTypes.Add(simpleMeleeWeapon);
                    
                    EditorUtility.SetDirty(simpleMeleeWeapon);
                }

                {
                    var martialRangedWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.MartialRangedWeapon, PathHelper.Weapons.WeaponTypesPath);
                    martialRangedWeapon.DisplayName = $"{NameHelper.Naming.WeaponTypes}.{NameHelper.WeaponTypes.MartialRangedWeapon}";
                    martialRangedWeapon.DisplayDescription = $"{NameHelper.Naming.WeaponTypes}.{NameHelper.WeaponTypes.MartialRangedWeapon}.{NameHelper.Naming.Description}";
                    weaponTypes.Add(martialRangedWeapon);
                    
                    EditorUtility.SetDirty(martialRangedWeapon);
                }

                {
                    var simpleRangedWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.SimpleRangedWeapon, PathHelper.Weapons.WeaponTypesPath);
                    simpleRangedWeapon.DisplayName = $"{NameHelper.Naming.WeaponTypes}.{NameHelper.WeaponTypes.SimpleRangedWeapon}";
                    simpleRangedWeapon.DisplayDescription = $"{NameHelper.Naming.WeaponTypes}.{NameHelper.WeaponTypes.SimpleRangedWeapon}.{NameHelper.Naming.Description}";
                    weaponTypes.Add(simpleRangedWeapon);
                    
                    EditorUtility.SetDirty(simpleRangedWeapon);
                }
                
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

                {
                    var arrows = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Arrows, PathHelper.Weapons.AmmunitionTypesPath);
                    arrows.DisplayName = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.Arrows}";
                    arrows.DisplayDescription = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.Arrows}.{NameHelper.Naming.Description}";
                    arrows.Amount = 20;
                    arrows.Weight = 0.5f;
                    arrows.Cost = 1;
                    arrows.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Quiver);
                    ammunitionTypes.Add(arrows);
                    
                    EditorUtility.SetDirty(arrows);
                }

                {
                    var bolts = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Bolts, PathHelper.Weapons.AmmunitionTypesPath);
                    bolts.DisplayName = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.Bolts}";
                    bolts.DisplayDescription = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.Bolts}.{NameHelper.Naming.Description}";
                    bolts.Amount = 20;
                    bolts.Weight = 0.75f;
                    bolts.Cost = 1;
                    bolts.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Case);
                    ammunitionTypes.Add(bolts);
                    
                    EditorUtility.SetDirty(bolts);
                }

                {
                    var bulletsFirearm = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.BulletsFirearm, PathHelper.Weapons.AmmunitionTypesPath);
                    bulletsFirearm.DisplayName = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.BulletsFirearm}";
                    bulletsFirearm.DisplayDescription = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.BulletsFirearm}.{NameHelper.Naming.Description}";
                    bulletsFirearm.Amount = 10;
                    bulletsFirearm.Weight = 1f;
                    bulletsFirearm.Cost = 3;
                    bulletsFirearm.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);
                    ammunitionTypes.Add(bulletsFirearm);
                    
                    EditorUtility.SetDirty(bulletsFirearm);
                }

                {
                    var bulletsSling = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.BulletsSling, PathHelper.Weapons.AmmunitionTypesPath);
                    bulletsSling.DisplayName = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.BulletsSling}";
                    bulletsSling.DisplayDescription = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.BulletsSling}.{NameHelper.Naming.Description}";
                    bulletsSling.Amount = 20;
                    bulletsSling.Weight = 0.75f;
                    bulletsSling.Cost = 4;
                    bulletsSling.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);
                    ammunitionTypes.Add(bulletsSling);
                    
                    EditorUtility.SetDirty(bulletsSling);
                }

                {
                    var needles = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Needles, PathHelper.Weapons.AmmunitionTypesPath);
                    needles.DisplayName = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.Needles}";
                    needles.DisplayDescription = $"{NameHelper.Naming.AmmunitionTypes}.{NameHelper.AmmunitionTypes.Needles}.{NameHelper.Naming.Description}";
                    needles.Amount = 50;
                    needles.Weight = 0.5f;
                    needles.Cost = 1;
                    needles.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);
                    ammunitionTypes.Add(needles);
                    
                    EditorUtility.SetDirty(needles);
                }
            
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