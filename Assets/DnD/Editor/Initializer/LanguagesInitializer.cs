using System.Collections.Generic;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Languages;
using Infrastructure.Helpers;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class LanguagesInitializer
    {
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Languages Data")]

        public static void InitializeLanguages()
        {
            FileSystemHelper.EnsureFolderExists(PathHelper.Languages.LanguagesPath);

            var origins = InitializeOrigins();

            InitializeRareLanguages(origins);

            InitializeStandardLanguages(origins);
        }

        private static void InitializeStandardLanguages(Dictionary<string, LanguageOrigin> origins)
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                FileSystemHelper.EnsureFolderExists(PathHelper.Languages.StandardLanguagesPath);

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Common, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Common}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Common}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Sigil];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.CommonSign, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.CommonSign}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.CommonSign}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Sigil];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Draconic, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Draconic}";;
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Draconic}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Dragons];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Dwarvish, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Dwarvish}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Dwarvish}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Dwarves];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Elvish, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Elvish}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Elvish}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Elves];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Giant, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Giant}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Giant}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Giants];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Gnomish, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Gnomish}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Gnomish}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Gnomes];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Goblin, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Goblin}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Goblin}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Goblinoids];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Halfling, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Halfling}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Halfling}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Halflings];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Orc, PathHelper.Languages.StandardLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Orc}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Orc}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Orcs];
                    
                    EditorUtility.SetDirty(language);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void InitializeRareLanguages(IDictionary<string, LanguageOrigin> origins)
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                FileSystemHelper.EnsureFolderExists(PathHelper.Languages.RareLanguagesPath);

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Abyssal, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Abyssal}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Abyssal}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.DemonsOfTheAbyss];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Celestial, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Celestial}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Celestial}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Celestials];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.DeepSpeech, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.DeepSpeech}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.DeepSpeech}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Aberrations];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Druidic, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Druidic}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Druidic}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.DruidicCircles];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Infernal, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Infernal}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Infernal}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.DevilsOfTheNineHells];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Primordial, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Primordial}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Primordial}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.Elementals];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Sylvan, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Sylvan}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Sylvan}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.TheFeywild];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.ThievesCant, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.ThievesCant}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.ThievesCant}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.VariousCriminalGuilds];
                    
                    EditorUtility.SetDirty(language);
                }

                {
                    var language = ScriptableObjectHelper.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Undercommon, PathHelper.Languages.RareLanguagesPath);
                    language.DisplayName = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Undercommon}";
                    language.DisplayDescription = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Undercommon}.{NameHelper.Naming.Description}";
                    language.Origin = origins[NameHelper.LanguageOrigins.TheUnderdark];
                    
                    EditorUtility.SetDirty(language);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        public static Dictionary<string, LanguageOrigin> InitializeOrigins()
        {
            var languageOrigins = new Dictionary<string, LanguageOrigin>();

            try
            {
                AssetDatabase.StartAssetEditing();

                FileSystemHelper.EnsureFolderExists(PathHelper.Languages.OriginsPath);

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Aberrations, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Aberrations}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Aberrations}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Aberrations, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Celestials, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Celestials}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Celestials}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Celestials, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DemonsOfTheAbyss, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.DemonsOfTheAbyss}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.DemonsOfTheAbyss}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.DemonsOfTheAbyss, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DevilsOfTheNineHells, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.DevilsOfTheNineHells}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.DevilsOfTheNineHells}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.DevilsOfTheNineHells, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Dragons, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Dragons}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Dragons}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Dragons, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DruidicCircles, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.DruidicCircles}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.DruidicCircles}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.DruidicCircles, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Dwarves, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Dwarves}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Dwarves}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Dwarves, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Elementals, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Elementals}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Elementals}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Elementals, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Elves, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Elves}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Elves}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Elves, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Giants, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Giants}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Giants}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Giants, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Gnomes, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Gnomes}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Gnomes}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Gnomes, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Goblinoids, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Goblinoids}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Goblinoids}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Goblinoids, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Halflings, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Halflings}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Halflings}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Halflings, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Orcs, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Orcs}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Orcs}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Orcs, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Sigil, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Sigil}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.Sigil}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.Sigil, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.TheFeywild, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.TheFeywild}";;
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.TheFeywild}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.TheFeywild, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.TheUnderdark, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.TheUnderdark}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.TheUnderdark}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.TheUnderdark, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                {
                    var origin = ScriptableObjectHelper.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.VariousCriminalGuilds, PathHelper.Languages.OriginsPath);
                    origin.DisplayName = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.VariousCriminalGuilds}";
                    origin.DisplayDescription = $"{NameHelper.Naming.LanguageOrigins}.{NameHelper.LanguageOrigins.VariousCriminalGuilds}.{NameHelper.Naming.Description}";
                    languageOrigins.Add(NameHelper.LanguageOrigins.VariousCriminalGuilds, origin);
                    
                    EditorUtility.SetDirty(origin);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
            
            return languageOrigins;
        }
    }
}
