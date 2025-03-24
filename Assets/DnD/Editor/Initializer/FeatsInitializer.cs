using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Feats;
using DnD.Code.Scripts.Helpers.PathHelper;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class FeatsInitializer
    {
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
                    barbarianFeat.DisplayName = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.BarbarianFeat}";
                    barbarianFeat.DisplayDescription = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.BarbarianFeat}.{NameHelper.Naming.Description}";
                    
                    EditorUtility.SetDirty(barbarianFeat);
                    
                    featCategories.Add(barbarianFeat);
                }
                
                {
                    var epicBoon = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.EpicBoon, PathHelper.Feats.FeatCategoriesPath);
                    epicBoon.DisplayName = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.EpicBoon}";
                    epicBoon.DisplayDescription = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.EpicBoon}.{NameHelper.Naming.Description}";
                    
                    EditorUtility.SetDirty(epicBoon);
                    
                    featCategories.Add(epicBoon);
                }
                
                {
                    var fightingStyle = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.FightingStyle, PathHelper.Feats.FeatCategoriesPath);
                    fightingStyle.DisplayName = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.FightingStyle}";
                    fightingStyle.DisplayDescription = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.FightingStyle}.{NameHelper.Naming.Description}";
                    
                    EditorUtility.SetDirty(fightingStyle);
                    
                    featCategories.Add(fightingStyle);
                }
                
                {
                    var general = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.General, PathHelper.Feats.FeatCategoriesPath);
                    general.DisplayName = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.General}";
                    general.DisplayDescription = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.General}.{NameHelper.Naming.Description}";
                    
                    EditorUtility.SetDirty(general);
                    
                    featCategories.Add(general);
                }
                
                {
                    var origin = Common.CreateScriptableObject<FeatCategory>(NameHelper.FeatCategories.Origin, PathHelper.Feats.FeatCategoriesPath);
                    origin.DisplayName = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.Origin}";
                    origin.DisplayDescription = $"{NameHelper.Naming.FeatCategories}.{NameHelper.FeatCategories.Origin}.{NameHelper.Naming.Description}";
                    
                    EditorUtility.SetDirty(origin);
                    
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
                    magicInitiate.DisplayDescription = $"{nameof(NameHelper.Feats)}.{NameHelper.Feats.MagicInitiate}.{NameHelper.Naming.Description}";
                    magicInitiate.Category = featCategories.Single(category => category.name == nameof(NameHelper.FeatCategories.Origin));
                    magicInitiate.Repeatable = Repeatable.Once;
                    magicInitiate.Benefit = null; // TODO
                    magicInitiate.Prerequisite = null; // TODO
                    
                    EditorUtility.SetDirty(magicInitiate);
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