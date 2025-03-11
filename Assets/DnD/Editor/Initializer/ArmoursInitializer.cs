using System;
using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Common;
using UnityEditor;
using UnityEngine;

namespace DnD.Editor.Initializer
{
    public static class ArmoursInitializer
    {
        public static readonly string ArmoursPath = $"{Common.FolderPath}/Armours";
        public static readonly string ArmourTypesPath = $"{ArmoursPath}/ArmourTypes";
        public static readonly string HeavyArmoursPath = $"{ArmoursPath}/{NameHelper.ArmourType.HeavyArmour}";
        public static readonly string MediumArmoursPath = $"{ArmoursPath}/{NameHelper.ArmourType.MediumArmour}";
        public static readonly string LightArmoursPath = $"{ArmoursPath}/{NameHelper.ArmourType.LightArmour}";
        public static readonly string ShieldsPath = $"{ArmoursPath}/{NameHelper.ArmourType.Shield}";

        public static Armour[] GetAllArmours()
        {
            return Common.GetAllScriptableObjects<Armour>(ArmoursPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initialize all Armours")]
        public static void InitializeArmours()
        {
            Common.EnsureFolderExists(ArmoursPath);

            var armourTypes = InitializeArmourTypes();
            
            InitializeHeavyArmours(armourTypes);
            
            InitializeLightArmours(armourTypes);
            
            InitializeMediumArmours(armourTypes);
            
            InitializeShields(armourTypes);

        }

        private static void InitializeShields(IBaseArmourType[] armourTypes)
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(ShieldsPath);
                
                var shieldType =
                    armourTypes.Single(x => ((ShieldType)x).name == NameHelper.ArmourType.Shield) as ShieldType;

                {
                    var shield = Common.CreateScriptableObject<Shield>(NameHelper.Shields.Shield, ShieldsPath);
                    shield.Type = shieldType;
                    shield.IncrementArmourClassBy = 2;
                    shield.Weight = 3;
                    shield.Cost = 0;
                }
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void InitializeMediumArmours(IBaseArmourType[] armourTypes)
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(MediumArmoursPath);
                
                var armourType =
                    armourTypes.Single(x => ((ArmourType)x).name == NameHelper.ArmourType.MediumArmour) as ArmourType;

                {
                    var breastplate = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.Breastplate, MediumArmoursPath);
                    breastplate.Type = armourType;
                    breastplate.ArmourClass = 14;
                    breastplate.AddDexModifier = true;
                    breastplate.CapDexModifier = true;
                    breastplate.MaxDexModifier = 2;
                    breastplate.HasDisadvantageOnDexterityChecks = false;
                    breastplate.Strength = 0;
                    breastplate.Weight = 3;
                    breastplate.Cost = 0;
                }
                
                {
                    var chainShirt = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.ChainShirt, MediumArmoursPath);
                    chainShirt.Type = armourType;
                    chainShirt.ArmourClass = 14;
                    chainShirt.AddDexModifier = true;
                    chainShirt.CapDexModifier = true;
                    chainShirt.MaxDexModifier = 2;
                    chainShirt.HasDisadvantageOnDexterityChecks = false;
                    chainShirt.Strength = 0;
                    chainShirt.Weight = 3;
                    chainShirt.Cost = 0;
                }
                
                {
                    var halfPlateArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.HalfPlateArmour, MediumArmoursPath);
                    halfPlateArmour.Type = armourType;
                    halfPlateArmour.ArmourClass = 15;
                    halfPlateArmour.AddDexModifier = true;
                    halfPlateArmour.CapDexModifier = true;
                    halfPlateArmour.MaxDexModifier = 2;
                    halfPlateArmour.HasDisadvantageOnDexterityChecks = true;
                    halfPlateArmour.Strength = 0;
                    halfPlateArmour.Weight = 20;
                    halfPlateArmour.Cost = 0;
                }
                
                {
                    var hideArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.HideArmour, MediumArmoursPath);
                    hideArmour.Type = armourType;
                    hideArmour.ArmourClass = 12;
                    hideArmour.AddDexModifier = true;
                    hideArmour.CapDexModifier = true;
                    hideArmour.MaxDexModifier = 2;
                    hideArmour.HasDisadvantageOnDexterityChecks = false;
                    hideArmour.Strength = 0;
                    hideArmour.Weight = 6;
                    hideArmour.Cost = 0;
                }
                
                {
                    var scaleMail = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.ScaleMail, MediumArmoursPath);
                    scaleMail.Type = armourType;
                    scaleMail.ArmourClass = 14;
                    scaleMail.AddDexModifier = true;
                    scaleMail.CapDexModifier = true;
                    scaleMail.MaxDexModifier = 2;
                    scaleMail.HasDisadvantageOnDexterityChecks = true;
                    scaleMail.Strength = 0;
                    scaleMail.Weight = 22.5f;
                    scaleMail.Cost = 0;
                }
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void InitializeLightArmours(IBaseArmourType[] armourTypes)
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(LightArmoursPath);
                
                var armourType =
                    armourTypes.Single(x => ((ArmourType)x).name == NameHelper.ArmourType.LightArmour) as ArmourType;

                {
                    var leatherArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Light.LeatherArmour, LightArmoursPath);
                    leatherArmour.Type = armourType;
                    leatherArmour.ArmourClass = 11;
                    leatherArmour.AddDexModifier = true;
                    leatherArmour.CapDexModifier = false;
                    leatherArmour.MaxDexModifier = 0;
                    leatherArmour.HasDisadvantageOnDexterityChecks = true;
                    leatherArmour.Strength = 0;
                    leatherArmour.Weight = 5;
                    leatherArmour.Cost = 0;
                }
                
                {
                    var paddedArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Light.PaddedArmour, LightArmoursPath);
                    paddedArmour.Type = armourType;
                    paddedArmour.ArmourClass = 11;
                    paddedArmour.AddDexModifier = true;
                    paddedArmour.CapDexModifier = false;
                    paddedArmour.MaxDexModifier = 0;
                    paddedArmour.HasDisadvantageOnDexterityChecks = true;
                    paddedArmour.Strength = 0;
                    paddedArmour.Weight = 4;
                    paddedArmour.Cost = 0;
                }
                
                {
                    var studdedLeatherArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Light.StuddedLeatherArmour, LightArmoursPath);
                    studdedLeatherArmour.Type = armourType;
                    studdedLeatherArmour.ArmourClass = 12;
                    studdedLeatherArmour.AddDexModifier = true;
                    studdedLeatherArmour.CapDexModifier = false;
                    studdedLeatherArmour.MaxDexModifier = 0;
                    studdedLeatherArmour.HasDisadvantageOnDexterityChecks = false;
                    studdedLeatherArmour.Strength = 0;
                    studdedLeatherArmour.Weight = 6.5f;
                    studdedLeatherArmour.Cost = 0;
                }
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void InitializeHeavyArmours(IBaseArmourType[] armourTypes)
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(HeavyArmoursPath);
                
                var armourType =
                    armourTypes.Single(x => ((ArmourType)x).name == NameHelper.ArmourType.HeavyArmour) as ArmourType;

                {
                    var chainMail = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Heavy.ChainMail, HeavyArmoursPath);
                    chainMail.Type = armourType;
                    chainMail.ArmourClass = 16;
                    chainMail.AddDexModifier = false;
                    chainMail.CapDexModifier = false;
                    chainMail.MaxDexModifier = 0;
                    chainMail.HasDisadvantageOnDexterityChecks = true;
                    chainMail.Strength = 13;
                    chainMail.Weight = 27;
                    chainMail.Cost = 1;
                }
                
                {
                    var plateArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Heavy.PlateArmour, HeavyArmoursPath);
                    plateArmour.Type = armourType;
                    plateArmour.ArmourClass = 18;
                    plateArmour.AddDexModifier = false;
                    plateArmour.CapDexModifier = false;
                    plateArmour.MaxDexModifier = 0;
                    plateArmour.HasDisadvantageOnDexterityChecks = true;
                    plateArmour.Strength = 15;
                    plateArmour.Weight = 32.5f;
                    plateArmour.Cost = 0;
                }
                
                {
                    var ringMail = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Heavy.RingMail, HeavyArmoursPath);
                    ringMail.Type = armourType;
                    ringMail.ArmourClass = 14;
                    ringMail.AddDexModifier = false;
                    ringMail.CapDexModifier = false;
                    ringMail.MaxDexModifier = 0;
                    ringMail.HasDisadvantageOnDexterityChecks = true;
                    ringMail.Strength = 0;
                    ringMail.Weight = 20.0f;
                    ringMail.Cost = 0;
                }
                
                {
                    var splintArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Heavy.SplintArmour, HeavyArmoursPath);
                    splintArmour.Type = armourType;
                    splintArmour.ArmourClass = 17;
                    splintArmour.AddDexModifier = false;
                    splintArmour.CapDexModifier = false;
                    splintArmour.MaxDexModifier = 0;
                    splintArmour.HasDisadvantageOnDexterityChecks = true;
                    splintArmour.Strength = 15;
                    splintArmour.Weight = 30.0f;
                    splintArmour.Cost = 0;
                }
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static IBaseArmourType[] InitializeArmourTypes()
        {
            var armourTypes = new List<IBaseArmourType>();

            try
            {
                AssetDatabase.StartAssetEditing();

                {
                    var heavyArmourType = Common.CreateScriptableObject<ArmourType>(NameHelper.ArmourType.HeavyArmour, ArmourTypesPath);
                    heavyArmourType.TimeInMinutesToDoff = 5;
                    heavyArmourType.TimeInMinutesToDon = 10;
                    armourTypes.Add(heavyArmourType);
                }

                {
                    var lightArmourType = Common.CreateScriptableObject<ArmourType>(NameHelper.ArmourType.LightArmour, ArmourTypesPath);
                    lightArmourType.TimeInMinutesToDoff = 1;
                    lightArmourType.TimeInMinutesToDon = 1;
                    armourTypes.Add(lightArmourType);
                }
                
                {
                    var mediumArmourType = Common.CreateScriptableObject<ArmourType>(NameHelper.ArmourType.MediumArmour, ArmourTypesPath);
                    mediumArmourType.TimeInMinutesToDoff = 1;
                    mediumArmourType.TimeInMinutesToDon = 5;
                    armourTypes.Add(mediumArmourType);
                }
                
                {
                    var shieldType = Common.CreateScriptableObject<ShieldType>(NameHelper.ArmourType.Shield, ArmourTypesPath);
                    armourTypes.Add(shieldType);
                }
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
            
            return armourTypes.ToArray();
        }
    }
}
