﻿using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Species.SpecialTraits;
using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;
using Infrastructure.Helpers;
using UnityEditor;
using UnityEngine;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public class SpexHumanInitializer : SpexInitializer
    {
        protected override string SpexName => PathHelper.Species.Human.SpexName;
        protected override string SpexPath => PathHelper.Species.Human.SpexPath;
        protected override string SpexSpecialTraitsPath => PathHelper.Species.Human.SpexSpecialTraitsPath;

        public void InitializeHuman(IEnumerable<CreatureType> creatureTypes)
        {
            InitializeSpex();
        }
        
        protected override Spex CreateSpexInstance()
        {
            var spexInstance =  ScriptableObjectHelper.CreateScriptableObject<Spex>(SpexName, SpexPath);

            // Initialize spex properties here
            spexInstance.DisplayName = $"{NameHelper.Naming.Species}.{SpexName}";
            spexInstance.DisplayDescription = $"{NameHelper.Naming.Species}.{SpexName}.{NameHelper.Naming.Description}";

            spexInstance.InheritFrom = null;
            spexInstance.CreatureType = CreatureTypes.Single(creatureType => creatureType.name == NameHelper.CreatureTypes.Humanoid);
            spexInstance.Size = Size.Small | Size.Medium;
            spexInstance.Speed = 9.144f;
            
            EditorUtility.SetDirty(spexInstance);
            
            return spexInstance;
        }

        protected override SpecialTrait[] InitializeSpecialTraits()
        {
            var specialTraits = new List<SpecialTrait>();
            
            specialTraits.Add(InitializeResourceful());
            specialTraits.Add(InitializeSkillful());
            specialTraits.Add(InitializeVersatile(NameHelper.FeatCategories.Origin));
            
            return specialTraits.ToArray();
        }

        private SpecialTrait InitializeResourceful()
        {
            var resourceful = ScriptableObjectHelper.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Resourceful, SpexSpecialTraitsPath);
            resourceful.DisplayName = $"{NameHelper.Naming.SpecialTraits}.{NameHelper.SpecialTraits.Resourceful}";
            resourceful.DisplayDescription = $"{NameHelper.Naming.SpecialTraits}.{NameHelper.SpecialTraits.Resourceful}.{NameHelper.Naming.Description}";

            var heroicInspiration = InitializeHeroicInspiration(resourceful);
            resourceful.TraitTypes.Add(heroicInspiration);
                
            EditorUtility.SetDirty(resourceful);
            
            return resourceful;
        }

        private HeroicInspiration InitializeHeroicInspiration(ScriptableObject parent)
        {
            var heroicInspiration = ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<HeroicInspiration>(NameHelper.TraitTypes.HeroicInspiration, parent);
            heroicInspiration.DisplayName = $"{NameHelper.Naming.TypeTraits}.{NameHelper.TraitTypes.HeroicInspiration}";
            heroicInspiration.DisplayDescription = $"{NameHelper.Naming.TypeTraits}.{NameHelper.TraitTypes.HeroicInspiration}.{NameHelper.Naming.Description}";

            EditorUtility.SetDirty(heroicInspiration);
                    
            return heroicInspiration;
        }
        

        private SpecialTrait InitializeSkillful()
        {
            var skillful = ScriptableObjectHelper.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Skillful, SpexSpecialTraitsPath);
            skillful.DisplayName = $"{NameHelper.Naming.SpecialTraits}.{NameHelper.SpecialTraits.Skillful}";
            skillful.DisplayDescription = $"{NameHelper.Naming.SpecialTraits}.{NameHelper.SpecialTraits.Skillful}.{NameHelper.Naming.Description}";

            var proficiency = InitializeProficiency(skillful);
            skillful.TraitTypes.Add(proficiency);
            
            EditorUtility.SetDirty(skillful);

            return skillful;
        }

        private Proficiency InitializeProficiency(ScriptableObject parent)
        {
            var proficiency = ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<Proficiency>(NameHelper.TraitTypes.Proficiency, parent);
            proficiency.DisplayName = $"{NameHelper.Naming.TypeTraits}.{NameHelper.TraitTypes.Proficiency}";
            proficiency.DisplayDescription = $"{NameHelper.Naming.TypeTraits}.{NameHelper.TraitTypes.Proficiency}.{NameHelper.Naming.Description}";
            
            EditorUtility.SetDirty(proficiency);
                
            return proficiency;
        }
        

        private SpecialTrait InitializeVersatile(string featCategoryName)
        {
            var versatile = ScriptableObjectHelper.CreateScriptableObject<SpecialTrait>(NameHelper.SpecialTraits.Versatile, SpexSpecialTraitsPath);
            versatile.DisplayName = $"{NameHelper.Naming.SpecialTraits}.{NameHelper.SpecialTraits.Versatile}";
            versatile.DisplayDescription = $"{NameHelper.Naming.SpecialTraits}.{NameHelper.SpecialTraits.Versatile}.{NameHelper.Naming.Description}";

            var hasFeatByCategory = InitializeHasFeatByCategory(versatile, featCategoryName);
            versatile.TraitTypes.Add(hasFeatByCategory);
            
            EditorUtility.SetDirty(versatile);
            
            return versatile;
        }

        private HasFeatByCategory InitializeHasFeatByCategory(ScriptableObject parent, string featCategoryName)
        {
            var featCategory = FeatCategories.Single(featCategory => featCategory.name == featCategoryName);
            
            var hasFeatByCategory = ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<HasFeatByCategory>(NameHelper.TraitTypes.HasFeatByCategory, parent);
            hasFeatByCategory.DisplayName = $"{NameHelper.Naming.TypeTraits}.{NameHelper.TraitTypes.HasFeatByCategory}";
            hasFeatByCategory.DisplayDescription = $"{NameHelper.Naming.TypeTraits}.{NameHelper.TraitTypes.HasFeatByCategory}.{NameHelper.Naming.Description}";
            hasFeatByCategory.FeatCategory = featCategory;
            
            EditorUtility.SetDirty(hasFeatByCategory);

            return hasFeatByCategory;
        }
    }
}