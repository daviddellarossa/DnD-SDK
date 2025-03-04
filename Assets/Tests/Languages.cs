using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Languages;
using NUnit.Framework;
using UnityEditor;

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
                            Helper.LanguageOrigins.Aberrations
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Celestials
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.DemonsOfTheAbyss
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.DevilsOfTheNineHells
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Dragons
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.DruidicCircles
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Dwarves
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Elementals
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Elves
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Giants
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Gnomes
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Goblinoids
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Halflings
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Orcs
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.Sigil
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.TheFeywild
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.TheUnderdark
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            Helper.LanguageOrigins.VariousCriminalGuilds
                        ));
                }
            }
            
            public static IEnumerable RareLanguagesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Abyssal,
                            Helper.LanguageOrigins.DemonsOfTheAbyss
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Celestial,
                            Helper.LanguageOrigins.Celestials
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.DeepSpeech,
                            Helper.LanguageOrigins.Aberrations
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Druidic,
                            Helper.LanguageOrigins.DruidicCircles
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Infernal,
                            Helper.LanguageOrigins.DevilsOfTheNineHells
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Primordial,
                            Helper.LanguageOrigins.Elementals
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Sylvan,
                            Helper.LanguageOrigins.TheFeywild
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.ThievesCant,
                            Helper.LanguageOrigins.VariousCriminalGuilds
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Undercommon,
                            Helper.LanguageOrigins.TheUnderdark
                        ));
                }
            }
            
            public static IEnumerable StandardLanguagesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Common,
                            Helper.LanguageOrigins.Sigil
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.CommonSign,
                            Helper.LanguageOrigins.Sigil
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Draconic,
                            Helper.LanguageOrigins.Dragons
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Dwarvish,
                            Helper.LanguageOrigins.Dwarves
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Elvish,
                            Helper.LanguageOrigins.Elves
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Giant,
                            Helper.LanguageOrigins.Giants
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Gnomish,
                            Helper.LanguageOrigins.Gnomes
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Goblin,
                            Helper.LanguageOrigins.Goblinoids
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Halfling,
                            Helper.LanguageOrigins.Halflings
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            Helper.Languages.Orc,
                            Helper.LanguageOrigins.Orcs
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