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
                acid.Name = NameHelper.DamageTypes.Acid;
            
                var bludgeoning = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Bludgeoning, DamageTypePath);
                bludgeoning.Name = NameHelper.DamageTypes.Bludgeoning;
            
                var cold = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Cold, DamageTypePath);
                cold.Name = NameHelper.DamageTypes.Cold;
            
                var fire = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Fire, DamageTypePath);
                fire.Name = NameHelper.DamageTypes.Fire;
            
                var force = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Force, DamageTypePath);
                force.Name = NameHelper.DamageTypes.Force;
            
                var lightning = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Lightning, DamageTypePath);
                lightning.Name = NameHelper.DamageTypes.Lightning;
            
                var necrotic = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Necrotic, DamageTypePath);
                necrotic.Name = NameHelper.DamageTypes.Necrotic;
            
                var piercing = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Piercing, DamageTypePath);
                piercing.Name = NameHelper.DamageTypes.Piercing;
            
                var poison = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Poison, DamageTypePath);
                poison.Name = NameHelper.DamageTypes.Poison;
            
                var psychic = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Psychic, DamageTypePath);
                psychic.Name = NameHelper.DamageTypes.Psychic;
            
                var radiant = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Radiant, DamageTypePath);
                radiant.Name = NameHelper.DamageTypes.Radiant;
            
                var slashing = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Slashing, DamageTypePath);
                slashing.Name = NameHelper.DamageTypes.Slashing;
            
                var thunder = Common.CreateScriptableObject<DamageType>(NameHelper.DamageTypes.Thunder, DamageTypePath);
                thunder.Name = NameHelper.DamageTypes.Thunder;

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