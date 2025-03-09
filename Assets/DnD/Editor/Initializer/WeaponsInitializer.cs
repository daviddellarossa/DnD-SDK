using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Weapons;
using System;
using System.Linq;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class WeaponsInitializer
    {
        public static readonly string WeaponsPath = $"{Common.FolderPath}/Weapons";
        public static readonly string WeaponTypesPath = $"{WeaponsPath}/WeaponTypes";
        public static readonly string AmmunitionTypesPath = $"{WeaponsPath}/AmmunitionTypes";
        public static readonly string MartialMeleeWeaponsPath = $"{WeaponsPath}/{NameHelper.WeaponTypes.MartialMeleeWeapon}";
        public static readonly string SimpleMeleeWeaponsPath = $"{WeaponsPath}/{NameHelper.WeaponTypes.SimpleMeleeWeapon}";
        public static readonly string MartialRangedWeaponsPath = $"{WeaponsPath}/{NameHelper.WeaponTypes.MartialRangedWeapon}";
        public static readonly string SimpleRangedWeaponsPath = $"{WeaponsPath}/{NameHelper.WeaponTypes.SimpleRangedWeapon}";

        public static Weapon[] GetAllWeapons()
        {
            return Common.GetAllScriptableObjects<Weapon>(WeaponsPath);
        }

        public static void InitializeWeapons()
        {
            AssetDatabase.StartAssetEditing();

            Common.EnsureFolderExists(WeaponsPath);

            InitializeWeaponTypes();

            InitializeAmmunitionTypes();

            InitializeMartialMeleeWeapons();

            InitializeMartialRangedWeapons();

            InitializeSimpleMeleeWeapons();

            InitializeSimpleRangedWeapons();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetDatabase.StopAssetEditing();
        }

        private static void InitializeSimpleRangedWeapons()
        {
            throw new NotImplementedException();
        }

        private static void InitializeSimpleMeleeWeapons()
        {
            throw new NotImplementedException();
        }

        private static void InitializeMartialRangedWeapons()
        {
            throw new NotImplementedException();
        }

        private static void InitializeMartialMeleeWeapons()
        {
            throw new NotImplementedException();
        }

        public static void InitializeWeaponTypes()
        {
            var martialMeleeWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.MartialMeleeWeapon, WeaponTypesPath);
            martialMeleeWeapon.Name = NameHelper.WeaponTypes.MartialMeleeWeapon;

            var simpleMeleeWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.SimpleMeleeWeapon, WeaponTypesPath);
            simpleMeleeWeapon.Name = NameHelper.WeaponTypes.SimpleMeleeWeapon;

            var martialRangedWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.MartialRangedWeapon, WeaponTypesPath);
            martialRangedWeapon.Name = NameHelper.WeaponTypes.MartialRangedWeapon;

            var simpleRangedWeapon = Common.CreateScriptableObject<WeaponType>(NameHelper.WeaponTypes.SimpleRangedWeapon, WeaponTypesPath);
            simpleRangedWeapon.Name = NameHelper.WeaponTypes.SimpleRangedWeapon;
        }

        public static void InitializeAmmunitionTypes()
        {
            var storage = StorageInitializer.GetAllStorage();

            var arrows = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Arrows, AmmunitionTypesPath);
            arrows.Name = NameHelper.AmmunitionTypes.Arrows;
            arrows.Amount = 20;
            arrows.Weight = 0.5f;
            arrows.Cost = 1;
            arrows.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Quiver);

            var bolts = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Bolts, AmmunitionTypesPath);
            bolts.Name = NameHelper.AmmunitionTypes.Bolts;
            bolts.Amount = 20;
            bolts.Weight = 0.75f;
            bolts.Cost = 1;
            bolts.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Case);

            var bulletsFirearm = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.BulletsFirearm, AmmunitionTypesPath);
            bulletsFirearm.Name = NameHelper.AmmunitionTypes.BulletsFirearm;
            bulletsFirearm.Amount = 10;
            bulletsFirearm.Weight = 1f;
            bulletsFirearm.Cost = 3;
            bulletsFirearm.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);

            var bulletsSling = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.BulletsSling, AmmunitionTypesPath);
            bulletsSling.Name = NameHelper.AmmunitionTypes.BulletsSling;
            bulletsSling.Amount = 20;
            bulletsSling.Weight = 0.75f;
            bulletsSling.Cost = 4;
            bulletsSling.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);

            var needles = Common.CreateScriptableObject<AmmunitionType>(NameHelper.AmmunitionTypes.Needles, AmmunitionTypesPath);
            needles.Name = NameHelper.AmmunitionTypes.Needles;
            needles.Amount = 50;
            needles.Weight = 0.5f;
            needles.Cost = 1;
            needles.Storage = storage.SingleOrDefault(x => x.name == NameHelper.Storage.Pouch);

        }
    }
}