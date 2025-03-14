using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Characters.Species;
using DnD.Code.Scripts.Characters.Species.SpecialTraits;
using DnD.Code.Scripts.Characters.Species.SpecialTraits.TraitTypes;
using DnD.Code.Scripts.Common;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static partial class SpeciesInitializer
    {
        private static void InitializeHuman(CreatureType[] creatureTypes)
        {
            var humanPath = $"{SpeciesPath}/{NameHelper.Species.Human}";

            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(humanPath);
                
                var specialTraits = InitializeHumanSpecialTraits();
                
                var human = Common.CreateScriptableObject<Code.Scripts.Characters.Species.Species>(NameHelper.Species.Human, humanPath);
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
            var humanSpecialTraitsPath = $"{SpeciesPath}/{NameHelper.Species.Human}/{NameHelper.Naming.SpecialTraits}";

            var specialTraits = new List<SpecialTrait>();
            
            var featCategories = FeatsInitializer.GetAllFeatCategories();
        
            Common.EnsureFolderExists(humanSpecialTraitsPath);

            {
                var resourceful = Common.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Resourceful, humanSpecialTraitsPath);
                resourceful.Name = $"{nameof(NameHelper.SpecialTraits)}.{NameHelper.SpecialTraits.Resourceful}";

                var heroicInspiration = Common.CreateScriptableObjectAndAddToObject<HeroicInspiration>(NameHelper.TypeTraits.HeroicInspiration, resourceful);
                heroicInspiration.Name = $"{nameof(NameHelper.TypeTraits)}.{NameHelper.TypeTraits.HeroicInspiration}";;
                
                AssetDatabase.SaveAssets();

                resourceful.TraitTypes.Add(heroicInspiration);
                
                specialTraits.Add(resourceful);
            }

            {
                var skillful = Common.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Skillful, humanSpecialTraitsPath);
                skillful.Name = $"{nameof(NameHelper.SpecialTraits)}.{NameHelper.SpecialTraits.Skillful}";
                
                var proficiency = Common.CreateScriptableObjectAndAddToObject<Proficiency>(NameHelper.TypeTraits.Proficiency, skillful);
                proficiency.Name = $"{nameof(NameHelper.TypeTraits)}.{NameHelper.TypeTraits.Proficiency}";;
                
                AssetDatabase.SaveAssets();

                skillful.TraitTypes.Add(proficiency);
                
                specialTraits.Add(skillful);
            }
            
            {
                var versatile = Common.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Versatile, humanSpecialTraitsPath);
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