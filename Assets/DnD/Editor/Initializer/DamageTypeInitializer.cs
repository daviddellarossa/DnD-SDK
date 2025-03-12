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
            
                var acid = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Acid, DamageTypePath);
                acid.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Acid}";
            
                var bludgeoning = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Bludgeoning, DamageTypePath);
                bludgeoning.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Bludgeoning}";
            
                var cold = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Cold, DamageTypePath);
                cold.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Cold}";
            
                var fire = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Fire, DamageTypePath);
                fire.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Fire}";
            
                var force = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Force, DamageTypePath);
                force.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Force}";
            
                var lightning = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Lightning, DamageTypePath);
                lightning.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Lightning}";
            
                var necrotic = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Necrotic, DamageTypePath);
                necrotic.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Necrotic}";
            
                var piercing = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Piercing, DamageTypePath);
                piercing.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Piercing}";
            
                var poison = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Poison, DamageTypePath);
                poison.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Poison}";
            
                var psychic = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Psychic, DamageTypePath);
                psychic.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Psychic}";
            
                var radiant = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Radiant, DamageTypePath);
                radiant.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Radiant}";
            
                var slashing = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Slashing, DamageTypePath);
                slashing.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Slashing}";
            
                var thunder = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Thunder, DamageTypePath);
                thunder.Name = $"{nameof(NameHelper.DamageTypes)}.{NameHelper.DamageTypes.Thunder}";

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