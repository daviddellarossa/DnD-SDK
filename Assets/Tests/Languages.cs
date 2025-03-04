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
                            "Aberrations"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Celestials"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Demons of the Abyss"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Devils of the Nine Hells"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Dragons"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Druidic circles"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Dwarves"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Elementals"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Elves"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Giants"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Gnomes"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Goblinoids"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Halflings"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Orcs"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Sigil"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "The Feywild"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "The Underdark"
                        ));
                    yield return new TestCaseData(
                        new LanguageOriginModel(
                            "Various criminal guilds"
                        ));
                }
            }
            
            public static IEnumerable RareLanguagesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Abyssal",
                            "Demons of the Abyss"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Celestial",
                            "Celestials"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Deep speech",
                            "Aberrations"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Druidic",
                            "Druidic circles"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Infernal",
                            "Devils of the Nine Hells"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Primordial",
                            "Elementals"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Sylvan",
                            "The Feywild"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Thieves' cant",
                            "Various criminal guilds"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Undercommon",
                            "The Underdark"
                        ));
                }
            }
            
            public static IEnumerable StandardLanguagesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Common",
                            "Sigil"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Common sign",
                            "Sigil"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Draconic",
                            "Dragons"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Dwarvish",
                            "Dwarves"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Elvish",
                            "Elves"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Giant",
                            "Giants"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Gnomish",
                            "Gnomes"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Goblin",
                            "Goblinoids"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Halfling",
                            "Halflings"
                        ));
                    yield return new TestCaseData(
                        new LanguageModel(
                            "Orc",
                            "Orcs"
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