using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Characters.Feats;
using DnD.Code.Scripts.Common;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class FeatsInitializer
    {
        public static readonly string FeatsDataPath = $"{Common.FolderPath}/FeatsData";
        public static readonly string FeatCategoriesPath = $"{FeatsDataPath}/Categories";
        public static readonly string FeatsPath = $"{FeatsDataPath}/Feats";

        public static Feat[] GetAllFeats()
        {
            return Common.GetAllScriptableObjects<Feat>(FeatsPath);
        }
        
        public static FeatCategory[] GetAllFeatCategories()
        {
            return Common.GetAllScriptableObjects<FeatCategory>(FeatCategoriesPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Feats Data")]
        public static void InitializeFeatsData()
        {
            Common.EnsureFolderExists(FeatsDataPath);
            
            var featCategories = InitializeFeatCategories();
            InitializeFeats(featCategories);
        }
        private static FeatCategory[] InitializeFeatCategories()
        {
            var featCategories = new List<FeatCategory>();
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(FeatCategoriesPath);

                {
                    var barbarianFeat = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.BarbarianFeat, FeatCategoriesPath);
                    barbarianFeat.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.BarbarianFeat}";
                    featCategories.Add(barbarianFeat);
                }
                
                {
                    var epicBoon = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.EpicBoon, FeatCategoriesPath);
                    epicBoon.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.EpicBoon}";
                    featCategories.Add(epicBoon);
                }
                
                {
                    var fightingStyle = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.FightingStyle, FeatCategoriesPath);
                    fightingStyle.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.FightingStyle}";
                    featCategories.Add(fightingStyle);
                }
                
                {
                    var general = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.General, FeatCategoriesPath);
                    general.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.General}";
                    featCategories.Add(general);
                }
                
                {
                    var origin = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.Origin, FeatCategoriesPath);
                    origin.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.Origin}";
                    featCategories.Add(origin);
                }
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
            return featCategories.ToArray();
        }

        private static void InitializeFeats(FeatCategory[] featCategories)
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(FeatsPath);

                {
                    var magicInitiate = Common.CreateScriptableObject<Feat>(NameHelper.Feats.MagicInitiate, FeatsPath);
                    magicInitiate.DisplayName = $"{nameof(NameHelper.Feats)}.{NameHelper.Feats.MagicInitiate}";
                    magicInitiate.Category = featCategories.Single(category => category.name == nameof(NameHelper.FeatCategories.Origin));
                    magicInitiate.Repeatable = Repeatable.Once;
                    magicInitiate.Benefit = null; // TODO
                    magicInitiate.Prerequisite = null; // TODO
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