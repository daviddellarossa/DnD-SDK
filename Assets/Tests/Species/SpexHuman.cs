using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;
using NUnit.Framework;
using UnityEditor;

namespace Tests.Species
{
    public class SpexHuman
    {
        private Spex _spex;
        private CreatureType[] _creatureTypes;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Spex)}");
            _spex =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Spex>)
                .Single(asset => asset != null);
            
            guids = AssetDatabase.FindAssets($"t:{nameof(CreatureType)}");
            _creatureTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<CreatureType>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(SpexData), nameof(SpexData.SpexTestCases))]
        public void TestSpex(SpexTestModel expected)
        {
            Assert.That(_spex, Is.Not.Null,  $"Spex {expected.Name} not found");
            Assert.That(_spex.DisplayName, Is.EqualTo(expected.DisplayName),  $"{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(_spex.DisplayDescription, Is.EqualTo(expected.DisplayDescription),  $"{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");
            Assert.That(_spex.InheritFrom, Is.EqualTo(expected.InheritFrom),  $"{expected.DisplayName}: {nameof(expected.InheritFrom)} not equal to {expected.InheritFrom}.");
            Assert.That(_spex.CreatureType.name, Is.EqualTo(expected.CreatureType),  $"{expected.DisplayName}: {nameof(expected.CreatureType)} not equal to {expected.CreatureType}.");
            Assert.That(_spex.Size, Is.EqualTo(expected.Size),  $"{expected.DisplayName}: {nameof(expected.Size)} not equal to {expected.Size}.");
            Assert.That(_spex.Speed, Is.EqualTo(expected.Speed),  $"{expected.DisplayName}: {nameof(expected.Speed)} not equal to {expected.Speed}.");
            
            Assert.That(_spex.SpecialTraits.Count, Is.EqualTo(expected.SpecialTraits.Length));
            foreach (var expectedSpecialTrait in expected.SpecialTraits)
            {
                var specialTrait = _spex.SpecialTraits.Single(st => st.name == expectedSpecialTrait.Name);
                Assert.That(specialTrait.name, Is.EqualTo(expectedSpecialTrait.Name));
                
                foreach (var expectedTraitType in expectedSpecialTrait.TraitTypes)
                {
                    var traitType = specialTrait.TraitTypes.Single(tt => tt.name == expectedTraitType.Name);
                    expectedTraitType.AssertEqual(traitType);
                }
            }
        }
        
        private class SpexData
        {
            public static IEnumerable SpexTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new SpexTestModel()
                        {
                            Name = NameHelper.Species.Human,
                            InheritFrom = null,
                            CreatureType = NameHelper.CreatureTypes.Humanoid,
                            Size = Size.Small | Size.Medium,
                            Speed = 9.144f,
                            SpecialTraits = new[]
                            {
                                new SpecialTraitTestModel()
                                {
                                    Name = NameHelper.SpecialTraits.Resourceful,
                                    TraitTypes = new TypeTraitTestModel []
                                    {
                                        new HeroicInspirationTestModel()
                                        {
                                            Name = NameHelper.TraitTypes.HeroicInspiration,
                                        }
                                    }
                                },
                                new  SpecialTraitTestModel()
                                {
                                    Name = NameHelper.SpecialTraits.Skillful,
                                    TraitTypes = new TypeTraitTestModel []
                                    {
                                        new ProficiencyTestModel()
                                        {
                                            Name = NameHelper.TraitTypes.Proficiency,
                                        }
                                    }
                                },
                                new  SpecialTraitTestModel()
                                {
                                    Name = NameHelper.SpecialTraits.Versatile,
                                    TraitTypes = new TypeTraitTestModel []
                                    {
                                        new HasFeatByCategoryTestModel()
                                        {
                                            Name = NameHelper.TraitTypes.HasFeatByCategory,
                                            FeatCategoryName = NameHelper.FeatCategories.Origin
                                        }
                                    }
                                }
                            }
                            
                        });
                }
            }
        }

        public class SpexTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Species}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            public string InheritFrom;
            public string CreatureType;
            public Size Size;
            public float Speed;
            public SpecialTraitTestModel[] SpecialTraits;
        }

        public class SpecialTraitTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.SpecialTraits}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";

            public TypeTraitTestModel[] TraitTypes { get; set; }
        }

        public abstract class TypeTraitTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.TypeTraits}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            
            public abstract void AssertEqual(TypeTrait typeTrait);
        }

        public class HeroicInspirationTestModel : TypeTraitTestModel
        {
            public override void AssertEqual(TypeTrait typeTrait)
            {
                Assert.That(typeTrait, Is.Not.Null);
                Assert.That(typeTrait, Is.TypeOf(typeof(HeroicInspiration)));
                var heroicInspiration = (HeroicInspiration)typeTrait;
                Assert.That(heroicInspiration.name, Is.EqualTo(Name));
                Assert.That(heroicInspiration.DisplayName, Is.EqualTo(DisplayName));
                Assert.That(heroicInspiration.DisplayDescription, Is.EqualTo(DisplayDescription));
            }
        }
        public class ProficiencyTestModel: TypeTraitTestModel
        {
            public override void AssertEqual(TypeTrait typeTrait)
            {
                Assert.That(typeTrait, Is.Not.Null);
                Assert.That(typeTrait, Is.TypeOf(typeof(Proficiency)));
                var proficiency = (Proficiency)typeTrait;
                Assert.That(proficiency.name, Is.EqualTo(Name));
                Assert.That(proficiency.DisplayName, Is.EqualTo(DisplayName));
                Assert.That(proficiency.DisplayDescription, Is.EqualTo(DisplayDescription));
            }
        }
        public class HasFeatByCategoryTestModel : TypeTraitTestModel
        {
            public string FeatCategoryName { get; set; }
            public override void AssertEqual(TypeTrait typeTrait)
            {
                Assert.That(typeTrait, Is.Not.Null);
                Assert.That(typeTrait, Is.TypeOf(typeof(HasFeatByCategory)));
                var hasFeatByCategory = (HasFeatByCategory)typeTrait;
                Assert.That(hasFeatByCategory.name, Is.EqualTo(Name));
                Assert.That(hasFeatByCategory.DisplayName, Is.EqualTo(DisplayName));
                Assert.That(hasFeatByCategory.DisplayDescription, Is.EqualTo(DisplayDescription));
                Assert.That(hasFeatByCategory.FeatCategory.name, Is.EqualTo(FeatCategoryName));
            }
        }
    }
}