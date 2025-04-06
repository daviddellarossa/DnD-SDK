using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Feats;
using DnD.Code.Scripts.Helpers.NameHelper;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    [TestFixture]
    public class Feats
    {
        private FeatCategory[] _featCategories;
        private Feat[] _feats;

        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(FeatCategory)}");
            _featCategories =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<FeatCategory>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(Feat)}");
            _feats =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Feat>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [TestCaseSource(typeof(FeatsData), nameof(FeatsData.FeatCategoriesTestCases))]
        public void TestFeatCategoriesTestCases(FeatCategoryTestModel expected)
        {
            var featCategory = _featCategories.SingleOrDefault(featCategory => featCategory.name == expected.Name);
            
            Assert.That(featCategory, Is.Not.Null, $"FeatCategory {expected.Name} was null.");
            Assert.That(featCategory.DisplayName, Is.EqualTo(expected.DisplayName),  $"{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(featCategory.DisplayDescription, Is.EqualTo(expected.DisplayDescription),  $"{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");
        }
        
        [TestCaseSource(typeof(FeatsData), nameof(FeatsData.FeatsTestCases))]
        public void TestFeatsTestCases(FeatTestModel expected)
        {
            var feat = _feats.SingleOrDefault(feat => feat.name == expected.Name);
            
            Assert.That(feat, Is.Not.Null, $"FeatCategory {expected.Name} was null.");
            Assert.That(feat.DisplayName, Is.EqualTo(expected.DisplayName),  $"{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(feat.DisplayDescription, Is.EqualTo(expected.DisplayDescription),  $"{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");
            Assert.That(feat.Category.name, Is.EqualTo(expected.Category),  $"{expected.DisplayName}: {nameof(expected.Category)} not equal to {expected.Category}.");
            Assert.That(feat.Repeatable, Is.EqualTo(expected.Repeatable),  $"{expected.DisplayName}: {nameof(expected.Repeatable)} not equal to {expected.Repeatable}.");
        }

        private class FeatsData
        {
            public static IEnumerable FeatCategoriesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new FeatCategoryTestModel()
                        {
                            Name = $"{NameHelper.FeatCategories.BarbarianFeat}",
                        });
                    yield return new TestCaseData(
                        new FeatCategoryTestModel()
                        {
                            Name = $"{NameHelper.FeatCategories.EpicBoon}",
                        });
                    yield return new TestCaseData(
                        new FeatCategoryTestModel()
                        {
                            Name = $"{NameHelper.FeatCategories.FightingStyle}",
                        });
                    yield return new TestCaseData(
                        new FeatCategoryTestModel()
                        {
                            Name = $"{NameHelper.FeatCategories.General}",
                        });
                    yield return new TestCaseData(
                        new FeatCategoryTestModel()
                        {
                            Name = $"{NameHelper.FeatCategories.Origin}",
                        });
                }
            }

            public static IEnumerable FeatsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new FeatTestModel()
                        {
                            Name = $"{NameHelper.Feats.MagicInitiate}",
                            Category = NameHelper.FeatCategories.Origin,
                            Repeatable = Repeatable.Once
                        }
                    );
                }
            }
        }
    }
    
    public class FeatCategoryTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.FeatCategories}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
    }

    public class FeatTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.Feats}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public string Category { get; set; }
        public Repeatable Repeatable { get; set; }
    }
}