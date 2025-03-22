﻿using System;
using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers.PathHelper;
using UnityEditor;
using UnityEngine;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class ArmoursInitializer
    {
        // public static readonly string PathHelper.Armours.ArmoursPath = $"{Common.FolderPath}/{NameHelper.Naming.Armours}";
        // public static readonly string PathHelper.Armours.ArmourTypesPath = $"{PathHelper.Armours.ArmoursPath}/{NameHelper.Naming.ArmourTypes}";
        // public static readonly string PathHelper.Armours.HeavyArmoursPath = $"{PathHelper.Armours.ArmoursPath}/{NameHelper.ArmourType.HeavyArmour}";
        // public static readonly string PathHelper.Armours.MediumArmoursPath = $"{PathHelper.Armours.ArmoursPath}/{NameHelper.ArmourType.MediumArmour}";
        // public static readonly string PathHelper.Armours.LightArmoursPath = $"{PathHelper.Armours.ArmoursPath}/{NameHelper.ArmourType.LightArmour}";
        // public static readonly string PathHelper.Armours.ShieldsPath = $"{PathHelper.Armours.ArmoursPath}/{NameHelper.ArmourType.Shield}";

        public static Armour[] GetAllArmours()
        {
            return Common.GetAllScriptableObjects<Armour>(PathHelper.Armours.ArmoursPath);
        }

        public static IBaseArmourType[] GetAllArmourTypes()
        {
            var armourTypes = new List<IBaseArmourType>();
            armourTypes.AddRange(Common.GetAllScriptableObjects<ArmourType>(PathHelper.Armours.ArmourTypesPath));
            armourTypes.AddRange(Common.GetAllScriptableObjects<ShieldType>(PathHelper.Armours.ArmourTypesPath));
            return armourTypes.ToArray();
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Armours")]
        public static void InitializeArmours()
        {
            Common.EnsureFolderExists(PathHelper.Armours.ArmoursPath);

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

                Common.EnsureFolderExists(PathHelper.Armours.ShieldsPath);
                
                var shieldType =
                    armourTypes.Single(x => ((ScriptableObject)x).name == NameHelper.ArmourType.Shield) as ShieldType;

                {
                    var shield = Common.CreateScriptableObject<Shield>(NameHelper.Shields.Shield, PathHelper.Armours.ShieldsPath);
                    shield.DisplayName = $"{nameof(NameHelper.Shields)}.{NameHelper.Shields.Shield}";
                    shield.Type = shieldType;
                    shield.IncrementArmourClassBy = 2;
                    shield.Weight = 3;
                    shield.Cost = 1000;
                    
                    EditorUtility.SetDirty(shield);
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

                Common.EnsureFolderExists(PathHelper.Armours.MediumArmoursPath);
                
                var armourType =
                    armourTypes.Single(x => ((ScriptableObject)x).name == NameHelper.ArmourType.MediumArmour) as ArmourType;

                {
                    var breastplate = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.Breastplate, PathHelper.Armours.MediumArmoursPath);
                    breastplate.DisplayName = $"{nameof(NameHelper.Armours_Medium)}.{NameHelper.Armours_Medium.Breastplate}";
                    breastplate.Type = armourType;
                    breastplate.ArmourClass = 14;
                    breastplate.AddDexModifier = true;
                    breastplate.CapDexModifier = true;
                    breastplate.MaxDexModifier = 2;
                    breastplate.HasDisadvantageOnDexterityChecks = false;
                    breastplate.Strength = 0;
                    breastplate.Weight = 10;
                    breastplate.Cost = 40000;
                    
                    EditorUtility.SetDirty(breastplate);
                }
                
                {
                    var chainShirt = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.ChainShirt, PathHelper.Armours.MediumArmoursPath);
                    chainShirt.DisplayName = $"{nameof(NameHelper.Armours_Medium)}.{NameHelper.Armours_Medium.ChainShirt}";
                    chainShirt.Type = armourType;
                    chainShirt.ArmourClass = 13;
                    chainShirt.AddDexModifier = true;
                    chainShirt.CapDexModifier = true;
                    chainShirt.MaxDexModifier = 2;
                    chainShirt.HasDisadvantageOnDexterityChecks = false;
                    chainShirt.Strength = 0;
                    chainShirt.Weight = 10;
                    chainShirt.Cost = 5000;
                    
                    EditorUtility.SetDirty(chainShirt);
                }
                
                {
                    var halfPlateArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.HalfPlateArmour, PathHelper.Armours.MediumArmoursPath);
                    halfPlateArmour.DisplayName = $"{nameof(NameHelper.Armours_Medium)}.{NameHelper.Armours_Medium.HalfPlateArmour}";
                    halfPlateArmour.Type = armourType;
                    halfPlateArmour.ArmourClass = 15;
                    halfPlateArmour.AddDexModifier = true;
                    halfPlateArmour.CapDexModifier = true;
                    halfPlateArmour.MaxDexModifier = 2;
                    halfPlateArmour.HasDisadvantageOnDexterityChecks = true;
                    halfPlateArmour.Strength = 0;
                    halfPlateArmour.Weight = 20;
                    halfPlateArmour.Cost = 75000;
                    
                    EditorUtility.SetDirty(halfPlateArmour);
                }
                
                {
                    var hideArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.HideArmour, PathHelper.Armours.MediumArmoursPath);
                    hideArmour.DisplayName = $"{nameof(NameHelper.Armours_Medium)}.{NameHelper.Armours_Medium.HideArmour}";
                    hideArmour.Type = armourType;
                    hideArmour.ArmourClass = 12;
                    hideArmour.AddDexModifier = true;
                    hideArmour.CapDexModifier = true;
                    hideArmour.MaxDexModifier = 2;
                    hideArmour.HasDisadvantageOnDexterityChecks = false;
                    hideArmour.Strength = 0;
                    hideArmour.Weight = 6;
                    hideArmour.Cost = 1000;
                    
                    EditorUtility.SetDirty(hideArmour);
                }
                
                {
                    var scaleMail = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Medium.ScaleMail, PathHelper.Armours.MediumArmoursPath);
                    scaleMail.DisplayName = $"{nameof(NameHelper.Armours_Medium)}.{NameHelper.Armours_Medium.ScaleMail}";
                    scaleMail.Type = armourType;
                    scaleMail.ArmourClass = 14;
                    scaleMail.AddDexModifier = true;
                    scaleMail.CapDexModifier = true;
                    scaleMail.MaxDexModifier = 2;
                    scaleMail.HasDisadvantageOnDexterityChecks = true;
                    scaleMail.Strength = 0;
                    scaleMail.Weight = 22.5f;
                    scaleMail.Cost = 5000;
                    
                    EditorUtility.SetDirty(scaleMail);
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

                Common.EnsureFolderExists(PathHelper.Armours.LightArmoursPath);
                
                var armourType =
                    armourTypes.Single(x => ((ScriptableObject)x).name == NameHelper.ArmourType.LightArmour) as ArmourType;

                {
                    var leatherArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Light.LeatherArmour, PathHelper.Armours.LightArmoursPath);
                    leatherArmour.DisplayName = $"{nameof(NameHelper.Armours_Light)}.{NameHelper.Armours_Light.LeatherArmour}";
                    leatherArmour.Type = armourType;
                    leatherArmour.ArmourClass = 11;
                    leatherArmour.AddDexModifier = true;
                    leatherArmour.CapDexModifier = false;
                    leatherArmour.MaxDexModifier = 0;
                    leatherArmour.HasDisadvantageOnDexterityChecks = true;
                    leatherArmour.Strength = 0;
                    leatherArmour.Weight = 5;
                    leatherArmour.Cost = 1000;
                    
                    EditorUtility.SetDirty(leatherArmour);
                }
                
                {
                    var paddedArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Light.PaddedArmour, PathHelper.Armours.LightArmoursPath);
                    paddedArmour.DisplayName = $"{nameof(NameHelper.Armours_Light)}.{NameHelper.Armours_Light.PaddedArmour}";
                    paddedArmour.Type = armourType;
                    paddedArmour.ArmourClass = 11;
                    paddedArmour.AddDexModifier = true;
                    paddedArmour.CapDexModifier = false;
                    paddedArmour.MaxDexModifier = 0;
                    paddedArmour.HasDisadvantageOnDexterityChecks = true;
                    paddedArmour.Strength = 0;
                    paddedArmour.Weight = 4;
                    paddedArmour.Cost = 500;
                    
                    EditorUtility.SetDirty(paddedArmour);
                }
                
                {
                    var studdedLeatherArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Light.StuddedLeatherArmour, PathHelper.Armours.LightArmoursPath);
                    studdedLeatherArmour.DisplayName = $"{nameof(NameHelper.Armours_Light)}.{NameHelper.Armours_Light.StuddedLeatherArmour}";
                    studdedLeatherArmour.Type = armourType;
                    studdedLeatherArmour.ArmourClass = 12;
                    studdedLeatherArmour.AddDexModifier = true;
                    studdedLeatherArmour.CapDexModifier = false;
                    studdedLeatherArmour.MaxDexModifier = 0;
                    studdedLeatherArmour.HasDisadvantageOnDexterityChecks = false;
                    studdedLeatherArmour.Strength = 0;
                    studdedLeatherArmour.Weight = 6.5f;
                    studdedLeatherArmour.Cost = 4500;
                    
                    EditorUtility.SetDirty(studdedLeatherArmour);
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

                Common.EnsureFolderExists(PathHelper.Armours.HeavyArmoursPath);
                
                var armourType =
                    armourTypes.Single(x => ((ScriptableObject)x).name == NameHelper.ArmourType.HeavyArmour) as ArmourType;

                if (armourType == null)
                {
                    Debug.LogError($"Couldn't find armour type {NameHelper.ArmourType.HeavyArmour}");
                }
                
                {
                    var chainMail = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Heavy.ChainMail, PathHelper.Armours.HeavyArmoursPath);
                    chainMail.DisplayName = $"{nameof(NameHelper.Armours_Heavy)}.{NameHelper.Armours_Heavy.ChainMail}";
                    chainMail.Type = armourType;
                    chainMail.ArmourClass = 16;
                    chainMail.AddDexModifier = false;
                    chainMail.CapDexModifier = false;
                    chainMail.MaxDexModifier = 0;
                    chainMail.HasDisadvantageOnDexterityChecks = true;
                    chainMail.Strength = 13;
                    chainMail.Weight = 27.5f;
                    chainMail.Cost = 7500;
                    
                    EditorUtility.SetDirty(chainMail);
                }
                
                {
                    var plateArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Heavy.PlateArmour, PathHelper.Armours.HeavyArmoursPath);
                    plateArmour.DisplayName = $"{nameof(NameHelper.Armours_Heavy)}.{NameHelper.Armours_Heavy.PlateArmour}";
                    plateArmour.Type = armourType;
                    plateArmour.ArmourClass = 18;
                    plateArmour.AddDexModifier = false;
                    plateArmour.CapDexModifier = false;
                    plateArmour.MaxDexModifier = 0;
                    plateArmour.HasDisadvantageOnDexterityChecks = true;
                    plateArmour.Strength = 15;
                    plateArmour.Weight = 32.5f;
                    plateArmour.Cost = 150000;
                    
                    EditorUtility.SetDirty(plateArmour);
                }
                
                {
                    var ringMail = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Heavy.RingMail, PathHelper.Armours.HeavyArmoursPath);
                    ringMail.DisplayName = $"{nameof(NameHelper.Armours_Heavy)}.{NameHelper.Armours_Heavy.RingMail}";
                    ringMail.Type = armourType;
                    ringMail.ArmourClass = 14;
                    ringMail.AddDexModifier = false;
                    ringMail.CapDexModifier = false;
                    ringMail.MaxDexModifier = 0;
                    ringMail.HasDisadvantageOnDexterityChecks = true;
                    ringMail.Strength = 0;
                    ringMail.Weight = 20.0f;
                    ringMail.Cost = 3000;
                    
                    EditorUtility.SetDirty(ringMail);
                }
                
                {
                    var splintArmour = Common.CreateScriptableObject<Armour>(NameHelper.Armours_Heavy.SplintArmour, PathHelper.Armours.HeavyArmoursPath);
                    splintArmour.DisplayName = $"{nameof(NameHelper.Armours_Heavy)}.{NameHelper.Armours_Heavy.SplintArmour}";
                    splintArmour.Type = armourType;
                    splintArmour.ArmourClass = 17;
                    splintArmour.AddDexModifier = false;
                    splintArmour.CapDexModifier = false;
                    splintArmour.MaxDexModifier = 0;
                    splintArmour.HasDisadvantageOnDexterityChecks = true;
                    splintArmour.Strength = 15;
                    splintArmour.Weight = 30.0f;
                    splintArmour.Cost = 20000;
                    
                    EditorUtility.SetDirty(splintArmour);
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

                Common.EnsureFolderExists(PathHelper.Armours.ArmourTypesPath);

                {
                    var heavyArmourType = Common.CreateScriptableObject<ArmourType>(NameHelper.ArmourType.HeavyArmour, PathHelper.Armours.ArmourTypesPath);
                    heavyArmourType.Name = $"{nameof(NameHelper.ArmourType)}.{NameHelper.ArmourType.HeavyArmour}";
                    heavyArmourType.TimeInMinutesToDoff = 5;
                    heavyArmourType.TimeInMinutesToDon = 10;
                    armourTypes.Add(heavyArmourType);
                    
                    EditorUtility.SetDirty(heavyArmourType);
                }

                {
                    var lightArmourType = Common.CreateScriptableObject<ArmourType>(NameHelper.ArmourType.LightArmour, PathHelper.Armours.ArmourTypesPath);
                    lightArmourType.Name = $"{nameof(NameHelper.ArmourType)}.{NameHelper.ArmourType.LightArmour}";
                    lightArmourType.TimeInMinutesToDoff = 1;
                    lightArmourType.TimeInMinutesToDon = 1;
                    armourTypes.Add(lightArmourType);
                    
                    EditorUtility.SetDirty(lightArmourType);
                }
                
                {
                    var mediumArmourType = Common.CreateScriptableObject<ArmourType>(NameHelper.ArmourType.MediumArmour, PathHelper.Armours.ArmourTypesPath);
                    mediumArmourType.Name = $"{nameof(NameHelper.ArmourType)}.{NameHelper.ArmourType.MediumArmour}";
                    mediumArmourType.TimeInMinutesToDoff = 1;
                    mediumArmourType.TimeInMinutesToDon = 5;
                    armourTypes.Add(mediumArmourType);
                    
                    EditorUtility.SetDirty(mediumArmourType);
                }
                
                {
                    var shieldType = Common.CreateScriptableObject<ShieldType>(NameHelper.ArmourType.Shield, PathHelper.Armours.ArmourTypesPath);
                    shieldType.DisplayName = $"{nameof(NameHelper.ArmourType)}.{NameHelper.ArmourType.Shield}";
                    armourTypes.Add(shieldType);
                    
                    EditorUtility.SetDirty(shieldType);
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
