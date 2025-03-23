using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Languages;
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
        public void TestAllLanguageOrigins(LanguageOriginTestModel expected)
        {
            var languageOrigin = _languageOrigins.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(languageOrigin, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.LanguageOrigins, expected.Name));
            Assert.That(languageOrigin.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(languageOrigin.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.RareLanguagesTestCases))]
        public void TestAllRareLanguages(LanguageTestModel expected)
        {
            var rareLanguage = _rareLanguages.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(rareLanguage, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Languages, expected.Name));
            Assert.That(rareLanguage.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(rareLanguage.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(rareLanguage.Origin.name, Is.EqualTo(expected.Origin), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Origin), expected.Origin));
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.StandardLanguagesTestCases))]
        public void TestAllStandardLanguages(LanguageTestModel expected)
        {
            var standardLanguage = _standardLanguages.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(standardLanguage, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Languages, expected.Name));
            Assert.That(standardLanguage.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(standardLanguage.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(standardLanguage.Origin.name, Is.EqualTo(expected.Origin), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Origin), expected.Origin));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable LanguageOriginsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Aberrations,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Celestials,
                        });
                    
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.DemonsOfTheAbyss,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.DevilsOfTheNineHells,
                        });
                    
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Dragons,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.DruidicCircles,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Dwarves,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Elementals,
                        });
                    
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Elves,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Giants,
                        });
                    
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Gnomes,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Goblinoids,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Halflings,
                        });
                    
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Orcs,
                        });
                    
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.Sigil,
                        });
                    
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.TheFeywild,
                        });
                    
                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.TheUnderdark,
                        });

                    yield return new TestCaseData(
                        new LanguageOriginTestModel()
                        {
                            Name = NameHelper.LanguageOrigins.VariousCriminalGuilds,
                        });
                }
            }
            
            public static IEnumerable RareLanguagesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Abyssal,
                            Origin = NameHelper.LanguageOrigins.DemonsOfTheAbyss
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Celestial,
                            Origin = NameHelper.LanguageOrigins.Celestials
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.DeepSpeech,
                            Origin = NameHelper.LanguageOrigins.Aberrations
                        });

                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Druidic,
                            Origin = NameHelper.LanguageOrigins.DruidicCircles
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Infernal,
                            Origin = NameHelper.LanguageOrigins.DevilsOfTheNineHells
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Primordial,
                            Origin = NameHelper.LanguageOrigins.Elementals
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Sylvan,
                            Origin = NameHelper.LanguageOrigins.TheFeywild
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.ThievesCant,
                            Origin = NameHelper.LanguageOrigins.VariousCriminalGuilds
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Undercommon,
                            Origin = NameHelper.LanguageOrigins.TheUnderdark
                        });
                }
            }
            
            public static IEnumerable StandardLanguagesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Common,
                            Origin = NameHelper.LanguageOrigins.Sigil
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.CommonSign,
                            Origin = NameHelper.LanguageOrigins.Sigil
                        });
                    
                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Draconic,
                            Origin = NameHelper.LanguageOrigins.Dragons
                        });

                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Dwarvish,
                            Origin = NameHelper.LanguageOrigins.Dwarves
                        });

                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Elvish,
                            Origin = NameHelper.LanguageOrigins.Elves
                        });

                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Giant,
                            Origin = NameHelper.LanguageOrigins.Giants
                        });

                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Gnomish,
                            Origin = NameHelper.LanguageOrigins.Gnomes
                        });

                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Goblin,
                            Origin = NameHelper.LanguageOrigins.Goblinoids
                        });

                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Halfling,
                            Origin = NameHelper.LanguageOrigins.Halflings
                        });

                    yield return new TestCaseData(
                        new LanguageTestModel()
                        {
                            Name = NameHelper.Languages.Orc,
                            Origin = NameHelper.LanguageOrigins.Orcs
                        });
                }
            }
        }
        
        
        public class LanguageOriginTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.LanguageOrigins}.{Name}";
            public string DisplayDescription =>  $"{DisplayName}.{NameHelper.Naming.Description}";
        }

        public class LanguageTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Languages}.{Name}";
            public string DisplayDescription =>  $"{DisplayName}.{NameHelper.Naming.Description}";
            public string Origin { get; set; }
        }
    }
}