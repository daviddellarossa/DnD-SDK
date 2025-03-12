using DnD.Code.Scripts.Combat;
using DnD.Code.Scripts.Common;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class DamageTypeInitializer
    {
        public static readonly string DamageTypePath = $"{Common.FolderPath}/DamageTypes";

        public static DamageType[] GetAllDamageTypes()
        {
            return Common.GetAllScriptableObjects<DamageType>(DamageTypePath);
        }

        [MenuItem("D&D Game/Game Data Initializer/Initialize Damage Types")]
        public static void InitializeDamageTypes()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(DamageTypePath);

                {
                    var acid = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Acid, DamageTypePath);
                    acid.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Acid}";
                    acid.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Acid}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var bludgeoning = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Bludgeoning, DamageTypePath);
                    bludgeoning.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Bludgeoning}";
                    bludgeoning.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Bludgeoning}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var cold = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Cold, DamageTypePath);
                    cold.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Cold}";
                    cold.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Cold}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var fire = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Fire, DamageTypePath);
                    fire.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Fire}";
                    fire.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Fire}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var force = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Force, DamageTypePath);
                    force.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Force}";
                    force.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Force}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var lightning = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Lightning, DamageTypePath);
                    lightning.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Lightning}";
                    lightning.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Lightning}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var necrotic = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Necrotic, DamageTypePath);
                    necrotic.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Necrotic}";
                    necrotic.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Necrotic}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var piercing = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Piercing, DamageTypePath);
                    piercing.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Piercing}";
                    piercing.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Piercing}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var poison = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Poison, DamageTypePath);
                    poison.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Poison}";
                    poison.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Poison}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var psychic = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Psychic, DamageTypePath);
                    psychic.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Psychic}";
                    psychic.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Psychic}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var radiant = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Radiant, DamageTypePath);
                    radiant.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Radiant}";
                    radiant.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Radiant}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var slashing = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Slashing, DamageTypePath);
                    slashing.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Slashing}";
                    slashing.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Slashing}.{NameHelper.Naming.Description}";
                    
                }
                {
                    var thunder = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Thunder, DamageTypePath);
                    thunder.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Thunder}";
                    thunder.Description = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Thunder}.{NameHelper.Naming.Description}";
                    
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