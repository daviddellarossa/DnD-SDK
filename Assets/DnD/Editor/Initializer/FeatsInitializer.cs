using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Characters.Feats;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers.PathHelper;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class FeatsInitializer
    {
        // public static readonly string PathHelper.Feats.FeatsDataPath = $"{Common.FolderPath}/{NameHelper.Naming.FeatsData}";
        // public static readonly string PathHelper.Feats.FeatCategoriesPath = $"{PathHelper.Feats.FeatsDataPath}/{NameHelper.Naming.Categories}";
        // public static readonly string PathHelper.Feats.FeatsPath = $"{PathHelper.Feats.FeatsDataPath}/{NameHelper.Naming.Feats}";

        public static Feat[] GetAllFeats()
        {
            return Common.GetAllScriptableObjects<Feat>(PathHelper.Feats.FeatsPath);
        }
        
        public static FeatCategory[] GetAllFeatCategories()
        {
            return Common.GetAllScriptableObjects<FeatCategory>(PathHelper.Feats.FeatCategoriesPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Feats Data")]
        public static void InitializeFeatsData()
        {
            Common.EnsureFolderExists(PathHelper.Feats.FeatsDataPath);
            
            var featCategories = InitializeFeatCategories();
            InitializeFeats(featCategories);
        }
        private static FeatCategory[] InitializeFeatCategories()
        {
            var featCategories = new List<FeatCategory>();
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(PathHelper.Feats.FeatCategoriesPath);

                {
                    var barbarianFeat = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.BarbarianFeat, PathHelper.Feats.FeatCategoriesPath);
                    barbarianFeat.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.BarbarianFeat}";
                    featCategories.Add(barbarianFeat);
                }
                
                {
                    var epicBoon = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.EpicBoon, PathHelper.Feats.FeatCategoriesPath);
                    epicBoon.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.EpicBoon}";
                    featCategories.Add(epicBoon);
                }
                
                {
                    var fightingStyle = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.FightingStyle, PathHelper.Feats.FeatCategoriesPath);
                    fightingStyle.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.FightingStyle}";
                    featCategories.Add(fightingStyle);
                }
                
                {
                    var general = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.General, PathHelper.Feats.FeatCategoriesPath);
                    general.Name = $"{nameof(NameHelper.FeatCategories)}.{NameHelper.FeatCategories.General}";
                    featCategories.Add(general);
                }
                
                {
                    var origin = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.Origin, PathHelper.Feats.FeatCategoriesPath);
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
            
                Common.EnsureFolderExists(PathHelper.Feats.FeatsPath);

                {
                    var magicInitiate = Common.CreateScriptableObject<Feat>(NameHelper.Feats.MagicInitiate, PathHelper.Feats.FeatsPath);
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