using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Species.SpecialTraits;
using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static partial class SpeciesInitializer
    {
        private static void InitializeHuman(CreatureType[] creatureTypes)
        {
            // var humanPath = $"{SpeciesPath}/{NameHelper.Species.Human}";

            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(PathHelper.Species.Human.HumanPath);
                
                var specialTraits = InitializeHumanSpecialTraits();
                
                var human = Common.CreateScriptableObject<Code.Scripts.Species.Species>(NameHelper.Species.Human, PathHelper.Species.Human.HumanPath);
                human.Name = $"{nameof(NameHelper.Species)}.{NameHelper.Species.Human}";
                human.InheritFrom = null;
                human.CreatureType = creatureTypes.Single(creatureType => creatureType.name == NameHelper.CreatureTypes.Humanoid);
                human.Size = Size.Small | Size.Medium;
                human.Speed = 9.144f;
                human.Traits.AddRange(specialTraits);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }
        
        private static SpecialTrait[] InitializeHumanSpecialTraits()
        {
            // var humanSpecialTraitsPath = $"{SpeciesPath}/{NameHelper.Species.Human}/{NameHelper.Naming.SpecialTraits}";

            var specialTraits = new List<SpecialTrait>();
            
            var featCategories = FeatsInitializer.GetAllFeatCategories();
        
            Common.EnsureFolderExists(PathHelper.Species.Human.SpecialTraitsPath);

            {
                var resourceful = Common.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Resourceful, PathHelper.Species.Human.SpecialTraitsPath);
                resourceful.Name = $"{nameof(NameHelper.SpecialTraits)}.{NameHelper.SpecialTraits.Resourceful}";

                var heroicInspiration = Common.CreateScriptableObjectAndAddToObject<HeroicInspiration>(NameHelper.TypeTraits.HeroicInspiration, resourceful);
                heroicInspiration.Name = $"{nameof(NameHelper.TypeTraits)}.{NameHelper.TypeTraits.HeroicInspiration}";;
                
                AssetDatabase.SaveAssets();

                resourceful.TraitTypes.Add(heroicInspiration);
                
                specialTraits.Add(resourceful);
            }

            {
                var skillful = Common.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Skillful, PathHelper.Species.Human.SpecialTraitsPath);
                skillful.Name = $"{nameof(NameHelper.SpecialTraits)}.{NameHelper.SpecialTraits.Skillful}";
                
                var proficiency = Common.CreateScriptableObjectAndAddToObject<Proficiency>(NameHelper.TypeTraits.Proficiency, skillful);
                proficiency.Name = $"{nameof(NameHelper.TypeTraits)}.{NameHelper.TypeTraits.Proficiency}";;
                
                AssetDatabase.SaveAssets();

                skillful.TraitTypes.Add(proficiency);
                
                specialTraits.Add(skillful);
            }
            
            {
                var versatile = Common.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Versatile, PathHelper.Species.Human.SpecialTraitsPath);
                versatile.Name = $"{nameof(NameHelper.SpecialTraits)}.{NameHelper.SpecialTraits.Versatile}";
                
                var hasFeatByCategory = Common.CreateScriptableObjectAndAddToObject<HasFeatByCategory>(NameHelper.TypeTraits.HasFeatByCategory, versatile);
                hasFeatByCategory.Name = $"{nameof(NameHelper.TypeTraits)}.{NameHelper.TypeTraits.HasFeatByCategory}";;
                hasFeatByCategory.FeatCategory = featCategories.Single(featCategory => featCategory.name == NameHelper.FeatCategories.Origin);
                
                AssetDatabase.SaveAssets();

                versatile.TraitTypes.Add(hasFeatByCategory);
                
                specialTraits.Add(versatile);
            }
            
            return specialTraits.ToArray();
        }
    }
}