using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Characters.Languages;
using DnD.Code.Scripts.Common;
using System;
using System.Collections.Generic;
using DnD.Code.Scripts.Helpers.PathHelper;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class LanguagesInitializer
    {
        // public static readonly string PathHelper.Languages.LanguagesPath = $"{Common.FolderPath}/{NameHelper.Naming.Languages}";
        // public static readonly string PathHelper.Languages.OriginsPath = $"{PathHelper.Languages.LanguagesPath}/{NameHelper.Naming.Origins}";
        // public static readonly string PathHelper.Languages.RareLanguagesPath = $"{PathHelper.Languages.LanguagesPath}/{NameHelper.Naming.RareLanguages}";
        // public static readonly string PathHelper.Languages.StandardLanguagesPath = $"{PathHelper.Languages.LanguagesPath}/{NameHelper.Naming.StandardLanguages}";


        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Languages Data")]

        public static void InitializeLanguages()
        {
            Common.EnsureFolderExists(PathHelper.Languages.LanguagesPath);

            var origins = InitializeOrigins();

            InitializeRareLanguages(origins);

            InitializeStandardLanguages(origins);
        }

        private static void InitializeStandardLanguages(Dictionary<string, LanguageOrigin> origins)
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(PathHelper.Languages.StandardLanguagesPath);
                
                var language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Common, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Common}";
                language.Origin = origins[NameHelper.LanguageOrigins.Sigil];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.CommonSign, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.CommonSign}";
                language.Origin = origins[NameHelper.LanguageOrigins.Sigil];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Draconic, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Draconic}";;
                language.Origin = origins[NameHelper.LanguageOrigins.Dragons];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Dwarvish, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Dwarvish}";
                language.Origin = origins[NameHelper.LanguageOrigins.Dwarves];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Elvish, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Elvish}";
                language.Origin = origins[NameHelper.LanguageOrigins.Elves];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Giant, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Giant}";
                language.Origin = origins[NameHelper.LanguageOrigins.Giants];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Gnomish, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Gnomish}";
                language.Origin = origins[NameHelper.LanguageOrigins.Gnomes];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Goblin, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Goblin}";
                language.Origin = origins[NameHelper.LanguageOrigins.Goblinoids];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Halfling, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Halfling}";
                language.Origin = origins[NameHelper.LanguageOrigins.Halflings];

                language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Orc, PathHelper.Languages.StandardLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Orc}";
                language.Origin = origins[NameHelper.LanguageOrigins.Orcs];

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

                Common.EnsureFolderExists(PathHelper.Languages.RareLanguagesPath);

                var language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Abyssal, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Abyssal}";
                language.Origin = origins[NameHelper.LanguageOrigins.DemonsOfTheAbyss];

                language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Celestial, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Celestial}";
                language.Origin = origins[NameHelper.LanguageOrigins.Celestials];

                language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.DeepSpeech, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.DeepSpeech}";
                language.Origin = origins[NameHelper.LanguageOrigins.Aberrations];

                language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Druidic, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Druidic}";
                language.Origin = origins[NameHelper.LanguageOrigins.DruidicCircles];

                language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Infernal, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Infernal}";
                language.Origin = origins[NameHelper.LanguageOrigins.DevilsOfTheNineHells];

                language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Primordial, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Primordial}";
                language.Origin = origins[NameHelper.LanguageOrigins.Elementals];

                language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Sylvan, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Sylvan}";
                language.Origin = origins[NameHelper.LanguageOrigins.TheFeywild];

                language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.ThievesCant, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.ThievesCant}";
                language.Origin = origins[NameHelper.LanguageOrigins.VariousCriminalGuilds];

                language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Undercommon, PathHelper.Languages.RareLanguagesPath);
                language.Name = $"{nameof(NameHelper.Languages)}.{NameHelper.Languages.Undercommon}";
                language.Origin = origins[NameHelper.LanguageOrigins.TheUnderdark];

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

                Common.EnsureFolderExists(PathHelper.Languages.OriginsPath);
                
                var origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Aberrations, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Aberrations}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Aberrations, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Celestials, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Celestials}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Celestials, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DemonsOfTheAbyss, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.DemonsOfTheAbyss}";
                languageOrigins.Add(NameHelper.LanguageOrigins.DemonsOfTheAbyss, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DevilsOfTheNineHells, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.DevilsOfTheNineHells}";
                languageOrigins.Add(NameHelper.LanguageOrigins.DevilsOfTheNineHells, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Dragons, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Dragons}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Dragons, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DruidicCircles, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.DruidicCircles}";
                languageOrigins.Add(NameHelper.LanguageOrigins.DruidicCircles, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Dwarves, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Dwarves}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Dwarves, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Elementals, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Elementals}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Elementals, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Elves, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Elves}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Elves, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Giants, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Giants}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Giants, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Gnomes, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Gnomes}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Gnomes, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Goblinoids, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Goblinoids}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Goblinoids, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Halflings, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Halflings}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Halflings, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Orcs, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Orcs}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Orcs, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Sigil, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.Sigil}";
                languageOrigins.Add(NameHelper.LanguageOrigins.Sigil, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.TheFeywild, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.TheFeywild}";;
                languageOrigins.Add(NameHelper.LanguageOrigins.TheFeywild, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.TheUnderdark, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.TheUnderdark}";
                languageOrigins.Add(NameHelper.LanguageOrigins.TheUnderdark, origin);

                origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.VariousCriminalGuilds, PathHelper.Languages.OriginsPath);
                origin.Name = $"{nameof(NameHelper.LanguageOrigins)}.{NameHelper.LanguageOrigins.VariousCriminalGuilds}";
                languageOrigins.Add(NameHelper.LanguageOrigins.VariousCriminalGuilds, origin);

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
