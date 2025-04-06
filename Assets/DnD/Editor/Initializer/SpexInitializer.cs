using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Feats;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Species.SpecialTraits;
using Infrastructure.Helpers;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public abstract class BaseSpexInitializer
    {
        
    }
    
    public abstract class SpexInitializer : BaseSpexInitializer
    {
        protected abstract string SpexName { get; }
        protected abstract string SpexPath { get; }
        protected abstract string SpexSpecialTraitsPath { get; }

        protected FeatCategory[] FeatCategories => FeatsInitializer.GetAllFeatCategories();
        protected CreatureType[] CreatureTypes => CreatureTypesInitializer.GetAllCreatureTypes();
        protected Skill[] Skills => AbilitiesInitializer.GetAllSkills();
        

        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Species Data")]
        public static void InitializeSpecies()
        {
            FileSystemHelper.EnsureFolderExists(PathHelper.Species.SpeciesPath);
            
            var creatureTypes = CreatureTypesInitializer.GetAllCreatureTypes();
            
            var humanInitializer = new SpexHumanInitializer();
            humanInitializer.InitializeHuman(creatureTypes);
        }
        
        protected void InitializeSpex()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                FileSystemHelper.EnsureFolderExists(SpexPath);
                FileSystemHelper.EnsureFolderExists(SpexSpecialTraitsPath);
                
                var spexInstance = CreateSpexInstance();

                // Create Special Traits
                var specialTraits = InitializeSpecialTraits();
                spexInstance.SpecialTraits.AddRange(specialTraits);
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        protected abstract Spex CreateSpexInstance();

        protected abstract SpecialTrait[] InitializeSpecialTraits();
    }
}