using DnD.Code.Scripts.Combat;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers;
using DnD.Code.Scripts.Helpers.PathHelper;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class DamageTypeInitializer
    {
        public static DamageType[] GetAllDamageTypes()
        {
            return ScriptableObjectHelper.GetAllScriptableObjects<DamageType>(PathHelper.DamageTypePath);
        }

        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Damage Types")]
        public static void InitializeDamageTypes()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                FileSystemHelper.EnsureFolderExists(PathHelper.DamageTypePath);

                {
                    var acid = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Acid, PathHelper.DamageTypePath);
                    acid.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Acid}";
                    acid.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Acid}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(acid);
                }
                {
                    var bludgeoning = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Bludgeoning, PathHelper.DamageTypePath);
                    bludgeoning.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Bludgeoning}";
                    bludgeoning.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Bludgeoning}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(bludgeoning);
                    
                }
                {
                    var cold = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Cold, PathHelper.DamageTypePath);
                    cold.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Cold}";
                    cold.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Cold}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(cold);
                    
                }
                {
                    var fire = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Fire, PathHelper.DamageTypePath);
                    fire.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Fire}";
                    fire.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Fire}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(fire);
                    
                }
                {
                    var force = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Force, PathHelper.DamageTypePath);
                    force.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Force}";
                    force.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Force}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(force);
                    
                }
                {
                    var lightning = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Lightning, PathHelper.DamageTypePath);
                    lightning.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Lightning}";
                    lightning.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Lightning}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(lightning);
                    
                }
                {
                    var necrotic = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Necrotic, PathHelper.DamageTypePath);
                    necrotic.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Necrotic}";
                    necrotic.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Necrotic}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(necrotic);
                    
                }
                {
                    var piercing = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Piercing, PathHelper.DamageTypePath);
                    piercing.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Piercing}";
                    piercing.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Piercing}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(piercing);
                    
                }
                {
                    var poison = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Poison, PathHelper.DamageTypePath);
                    poison.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Poison}";
                    poison.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Poison}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(poison);
                    
                }
                {
                    var psychic = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Psychic, PathHelper.DamageTypePath);
                    psychic.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Psychic}";
                    psychic.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Psychic}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(psychic);
                    
                }
                {
                    var radiant = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Radiant, PathHelper.DamageTypePath);
                    radiant.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Radiant}";
                    radiant.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Radiant}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(radiant);
                    
                }
                {
                    var slashing = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Slashing, PathHelper.DamageTypePath);
                    slashing.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Slashing}";
                    slashing.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Slashing}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(slashing);
                    
                }
                {
                    var thunder = ScriptableObjectHelper.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Thunder, PathHelper.DamageTypePath);
                    thunder.DisplayName = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Thunder}";
                    thunder.DisplayDescription = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Thunder}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(thunder);
                    
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }
    }
}