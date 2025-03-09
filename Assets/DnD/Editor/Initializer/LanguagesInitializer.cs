using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Characters.Languages;
using DnD.Code.Scripts.Common;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class LanguagesInitializer
    {
        public static readonly string LanguagesPath = $"{Common.FolderPath}/Languages";
        public static readonly string OriginsPath = $"{LanguagesPath}/Origins";
        public static readonly string RareLanguagesPath = $"{LanguagesPath}/RareLanguages";
        public static readonly string StandardLanguagesPath = $"{LanguagesPath}/StandardLanguages";


        [MenuItem("D&D Game/Game Data Initializer/Generate Languages Data")]

        public static void InitializeLanguages()
        {
            AssetDatabase.StartAssetEditing();

            Common.EnsureFolderExists(LanguagesPath);

            var origins = InitializeOrigins();

            InitializeRareLanguages(origins);

            InitializeStandardLanguages(origins);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetDatabase.StopAssetEditing();
        }

        private static void InitializeStandardLanguages(Dictionary<string, LanguageOrigin> origins)
        {
            Common.EnsureFolderExists(StandardLanguagesPath);

            var language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Common, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Common;
            language.Origin = origins[NameHelper.LanguageOrigins.Sigil];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.CommonSign, StandardLanguagesPath);
            language.Name = NameHelper.Languages.CommonSign;
            language.Origin = origins[NameHelper.LanguageOrigins.Sigil];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Draconic, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Draconic;
            language.Origin = origins[NameHelper.LanguageOrigins.Dragons];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Dwarvish, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Dwarvish;
            language.Origin = origins[NameHelper.LanguageOrigins.Dwarves];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Elvish, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Elvish;
            language.Origin = origins[NameHelper.LanguageOrigins.Elves];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Giant, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Giant;
            language.Origin = origins[NameHelper.LanguageOrigins.Giants];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Gnomish, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Gnomish;
            language.Origin = origins[NameHelper.LanguageOrigins.Gnomes];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Goblin, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Goblin;
            language.Origin = origins[NameHelper.LanguageOrigins.Goblinoids];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Halfling, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Halfling;
            language.Origin = origins[NameHelper.LanguageOrigins.Halflings];

            language = Common.CreateScriptableObject<StandardLanguage>(NameHelper.Languages.Orc, StandardLanguagesPath);
            language.Name = NameHelper.Languages.Orc;
            language.Origin = origins[NameHelper.LanguageOrigins.Orcs];

        }

        private static void InitializeRareLanguages(IDictionary<string, LanguageOrigin> origins)
        {
            Common.EnsureFolderExists(RareLanguagesPath);

            var language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Abyssal, RareLanguagesPath);
            language.Name = NameHelper.Languages.Abyssal;
            language.Origin = origins[NameHelper.LanguageOrigins.DemonsOfTheAbyss];

            language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Celestial, RareLanguagesPath);
            language.Name = NameHelper.Languages.Celestial;
            language.Origin = origins[NameHelper.LanguageOrigins.Celestials];

            language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.DeepSpeech, RareLanguagesPath);
            language.Name = NameHelper.Languages.DeepSpeech;
            language.Origin = origins[NameHelper.LanguageOrigins.Aberrations];

            language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Druidic, RareLanguagesPath);
            language.Name = NameHelper.Languages.Druidic;
            language.Origin = origins[NameHelper.LanguageOrigins.DruidicCircles];

            language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Infernal, RareLanguagesPath);
            language.Name = NameHelper.Languages.Infernal;
            language.Origin = origins[NameHelper.LanguageOrigins.DevilsOfTheNineHells];

            language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Primordial, RareLanguagesPath);
            language.Name = NameHelper.Languages.Primordial;
            language.Origin = origins[NameHelper.LanguageOrigins.Elementals];

            language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Sylvan, RareLanguagesPath);
            language.Name = NameHelper.Languages.Sylvan;
            language.Origin = origins[NameHelper.LanguageOrigins.TheFeywild];

            language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.ThievesCant, RareLanguagesPath);
            language.Name = NameHelper.Languages.ThievesCant;
            language.Origin = origins[NameHelper.LanguageOrigins.VariousCriminalGuilds];

            language = Common.CreateScriptableObject<RareLanguage>(NameHelper.Languages.Undercommon, RareLanguagesPath);
            language.Name = NameHelper.Languages.Undercommon;
            language.Origin = origins[NameHelper.LanguageOrigins.TheUnderdark];

        }

        public static Dictionary<string, LanguageOrigin> InitializeOrigins()
        {
            Common.EnsureFolderExists(OriginsPath);

            var languageOrigins = new Dictionary<string, LanguageOrigin>();

            var origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Aberrations, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Aberrations;
            languageOrigins.Add(NameHelper.LanguageOrigins.Aberrations, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Celestials, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Celestials;
            languageOrigins.Add(NameHelper.LanguageOrigins.Celestials, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DemonsOfTheAbyss, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.DemonsOfTheAbyss;
            languageOrigins.Add(NameHelper.LanguageOrigins.DemonsOfTheAbyss, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DevilsOfTheNineHells, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.DevilsOfTheNineHells;
            languageOrigins.Add(NameHelper.LanguageOrigins.DevilsOfTheNineHells, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Dragons, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Dragons;
            languageOrigins.Add(NameHelper.LanguageOrigins.Dragons, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.DruidicCircles, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.DruidicCircles;
            languageOrigins.Add(NameHelper.LanguageOrigins.DruidicCircles, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Dwarves, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Dwarves;
            languageOrigins.Add(NameHelper.LanguageOrigins.Dwarves, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Elementals, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Elementals;
            languageOrigins.Add(NameHelper.LanguageOrigins.Elementals, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Elves, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Elves;
            languageOrigins.Add(NameHelper.LanguageOrigins.Elves, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Giants, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Giants;
            languageOrigins.Add(NameHelper.LanguageOrigins.Giants, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Gnomes, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Gnomes;
            languageOrigins.Add(NameHelper.LanguageOrigins.Gnomes, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Goblinoids, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Goblinoids;
            languageOrigins.Add(NameHelper.LanguageOrigins.Goblinoids, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Halflings, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Halflings;
            languageOrigins.Add(NameHelper.LanguageOrigins.Halflings, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Orcs, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Orcs;
            languageOrigins.Add(NameHelper.LanguageOrigins.Orcs, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.Sigil, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.Sigil;
            languageOrigins.Add(NameHelper.LanguageOrigins.Sigil, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.TheFeywild, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.TheFeywild;
            languageOrigins.Add(NameHelper.LanguageOrigins.TheFeywild, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.TheUnderdark, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.TheUnderdark;
            languageOrigins.Add(NameHelper.LanguageOrigins.TheUnderdark, origin);

            origin = Common.CreateScriptableObject<LanguageOrigin>(NameHelper.LanguageOrigins.VariousCriminalGuilds, OriginsPath);
            origin.Name = NameHelper.LanguageOrigins.VariousCriminalGuilds;
            languageOrigins.Add(NameHelper.LanguageOrigins.VariousCriminalGuilds, origin);

            return languageOrigins;
        }
    }
}
