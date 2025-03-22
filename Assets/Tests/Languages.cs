using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Languages;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests
{
    [TestFixture]
    public class Languages
    {
        private LanguageOrigin[] _languageOrigins;
        private RareLanguage[] _rareLanguages;
        private StandardLanguage[] _standardLanguages;

        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(LanguageOrigin)}");
            _languageOrigins =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<LanguageOrigin>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(RareLanguage)}");
            _rareLanguages =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<RareLanguage>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(StandardLanguage)}");
            _standardLanguages =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<StandardLanguage>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.LanguageOriginsTestCases))]
        public void TestAllLanguageOrigins(LanguageOriginModel expected)
        {
            var languageOrigin = _languageOrigins.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(languageOrigin, Is.Not.Null);
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.RareLanguagesTestCases))]
        public void TestAllRareLanguages(LanguageModel expected)
        {
            var rareLanguage = _rareLanguages.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(rareLanguage, Is.Not.Null);
            Assert.That(rareLanguage.Origin.Name, Is.EqualTo(expected.Origin));
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.StandardLanguagesTestCases))]
        public void TestAllStandardLanguages(LanguageModel expected)
        {
            var standardLanguage = _standardLanguages.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(standardLanguage, Is.Not.Null);
            Assert.That(standardLanguage.Origin.Name, Is.EqualTo(expected.Origin));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable LanguageOriginsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Aberrations
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Celestials
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.DemonsOfTheAbyss
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.DevilsOfTheNineHells
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Dragons
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.DruidicCircles
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Dwarves
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Elementals
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Elves
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Giants
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Gnomes
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Goblinoids
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Halflings
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Orcs
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.Sigil
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.TheFeywild
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.TheUnderdark
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            NameHelper.LanguageOrigins.VariousCriminalGuilds
                        ));
                }
            }
            
            public static IEnumerable RareLanguagesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Abyssal,
                            NameHelper.LanguageOrigins.DemonsOfTheAbyss
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Celestial,
                            NameHelper.LanguageOrigins.Celestials
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.DeepSpeech,
                            NameHelper.LanguageOrigins.Aberrations
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Druidic,
                            NameHelper.LanguageOrigins.DruidicCircles
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Infernal,
                            NameHelper.LanguageOrigins.DevilsOfTheNineHells
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Primordial,
                            NameHelper.LanguageOrigins.Elementals
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Sylvan,
                            NameHelper.LanguageOrigins.TheFeywild
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.ThievesCant,
                            NameHelper.LanguageOrigins.VariousCriminalGuilds
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Undercommon,
                            NameHelper.LanguageOrigins.TheUnderdark
                        ));
                }
            }
            
            public static IEnumerable StandardLanguagesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Common,
                            NameHelper.LanguageOrigins.Sigil
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.CommonSign,
                            NameHelper.LanguageOrigins.Sigil
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Draconic,
                            NameHelper.LanguageOrigins.Dragons
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Dwarvish,
                            NameHelper.LanguageOrigins.Dwarves
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Elvish,
                            NameHelper.LanguageOrigins.Elves
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Giant,
                            NameHelper.LanguageOrigins.Giants
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Gnomish,
                            NameHelper.LanguageOrigins.Gnomes
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Goblin,
                            NameHelper.LanguageOrigins.Goblinoids
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Halfling,
                            NameHelper.LanguageOrigins.Halflings
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            NameHelper.Languages.Orc,
                            NameHelper.LanguageOrigins.Orcs
                        ));
                }
            }
        }
        
        
        public class LanguageOriginModel
        {
            public string Name { get; set; }
            public LanguageOriginModel(string name)
            {
                this.Name = name;
            }
        }

        public class LanguageModel
        {
            public string Name { get; set; }
            public string Origin { get; set; }

            public LanguageModel(string name,  string origin)
            {
                this.Name = name;
                this.Origin = origin;
            }
        }
    }
}