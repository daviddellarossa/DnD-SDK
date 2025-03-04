using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Characters.Species;
using DnD.Code.Scripts.Characters.Species.SpecialTraits.TraitTypes;
using NUnit.Framework;
using UnityEditor;

namespace Tests.Species
{
    [TestFixture]
    public class Species
    {
        private DnD.Code.Scripts.Characters.Species.Species[] _species;

        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(DnD.Code.Scripts.Characters.Species.Species)}");
            _species =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<DnD.Code.Scripts.Characters.Species.Species>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.SpeciesTestCases))]
        public void TestAllSpecies(SpeciesModel expected)
        {
            var species = _species.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(species, Is.Not.Null);
            Assert.That(species.InheritFrom, Is.EqualTo(expected.InheritFrom));
            Assert.That(species.CreatureType.Name, Is.EqualTo(expected.CreatureType));
            Assert.That(species.Size, Is.EqualTo(expected.Size));
            Assert.That(species.Speed, Is.EqualTo(expected.Speed));
            Assert.That(species.Traits.Count, Is.EqualTo(expected.Traits.Count()));
            foreach (var specialTrait in species.Traits)
            {
                var expectedSpecialTrait = expected.Traits.SingleOrDefault(x => x.Name == specialTrait.Name);
                
                Assert.That(expectedSpecialTrait, Is.Not.Null);
                Assert.That(expectedSpecialTrait.Name,  Is.EqualTo(specialTrait.Name));
                Assert.That(expectedSpecialTrait.TraitTypes.Count, Is.EqualTo(specialTrait.TraitTypes.Count()));

                foreach (var traitType in specialTrait.TraitTypes)
                {
                    var expectedTraitType = expectedSpecialTrait.TraitTypes.SingleOrDefault(x => x.Name == traitType.Name);
                    
                    Assert.That(expectedTraitType, Is.Not.Null);
                    expectedTraitType.AssertEquals(traitType);
                }
            }
        }

        private static class Assertor
        {
            public static void AssertEqual(DamageResistance actual, DamageResistance expected)
            {
                
            }

            public static void AssertEqual(HasFeatByCategory actual, HasFeatByCategoryModel expected)
            {
                
            }

            public static void AssertEqual(HeroicInspiration actual, HeroicInspirationModel expected)
            {
                
            }

            public static void AssertEqual(Proficiency actual, ProficiencyModel expected)
            {
                
            }

            public static void AssertEqual(SpeedBoost actual, SpeedBoostModel expected)
            {
                
            }

        }
        
        private class AbilitiesData
        {
            public static IEnumerable SpeciesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new Species.SpeciesModel(
                            Helper.Species.Human,
                            null,
                            Helper.CreatureTypes.Humanoid,
                            Size.Small | Size.Medium,
                            9.144f,
                            new Species.SpecialTraitModel[]
                            {
                                new Species.SpecialTraitModel(
                                    Helper.SpecialTraits.Resourceful,
                                    new Species.TraitTypeModel[]
                                    {
                                        new Species.HeroicInspirationModel(
                                            Helper.TypeTraits.HeroicInspiration,
                                            false)
                                    }),
                                new Species.SpecialTraitModel(
                                    Helper.SpecialTraits.Skillful,
                                    new Species.TraitTypeModel[]
                                    {
                                        new Species.ProficiencyModel(
                                            Helper.TypeTraits.Proficiency,
                                            null)
                                    }),
                                new Species.SpecialTraitModel(
                                    Helper.SpecialTraits.Versatile,
                                    new Species.TraitTypeModel[]
                                    {
                                        new Species.HasFeatByCategoryModel(
                                            Helper.TypeTraits.HasFeatByCategory,
                                            Helper.FeatCategories.Origin)
                                    }),
                            }));
                }
            }
        }
        
        
        public class SpeciesModel
        {
            public string Name { get; set; }
            public string InheritFrom { get; set; }
            public string CreatureType { get; set; }
            public DnD.Code.Scripts.Characters.Species.Size Size { get; set; }
            public float Speed { get; set; }
            public SpecialTraitModel[] Traits { get; set; }

            public SpeciesModel(
                string name,
                string inheritFrom,
                string creatureType,
                DnD.Code.Scripts.Characters.Species.Size size,
                float speed,
                SpecialTraitModel[] traits
                )
            {
                this.Name = name;
                this.InheritFrom = inheritFrom;
                this.CreatureType = creatureType;
                this.Size = size;
                this.Speed = speed;
                this.Traits = traits;
            }
        }
        
        public class SpecialTraitModel
        {
            public string Name { get; set; }
            
            public TraitTypeModel[] TraitTypes { get; set; }

            public SpecialTraitModel(string name, TraitTypeModel[] traitTypes)
            {
                this.Name = name;
                this.TraitTypes = traitTypes;
            }
        }
        
        public abstract class TraitTypeModel
        {
            public string Name { get; set; }

            public TraitTypeModel(string name)
            {
                this.Name = name;
            }

            public abstract void AssertEquals(TraitType traitType);
        }

        public class DamageResistanceModel : TraitTypeModel
        {
            public string DamageType { get; set; }
            public float Percent { get; set; }
            public DamageResistanceModel(string name, string damageType, float percent) : base(name)
            {
                this.DamageType = damageType;
                this.Percent = percent;
            }

            public override void AssertEquals(TraitType traitType)
            {
                DamageResistance tt = traitType as DamageResistance;
                Assert.That(tt, Is.Not.Null);
                
                Assert.That(this.Percent, Is.EqualTo(tt.Percent));
                Assert.That(this.DamageType, Is.EqualTo(tt.DamageType.Name));
                Assert.That(this.Name, Is.EqualTo(tt.Name));
            }
        }

        public class HasFeatByCategoryModel : TraitTypeModel
        {
            public string FeatCategory { get; set; }

            public HasFeatByCategoryModel(string name, string featCategory) : base(name)
            {
                this.FeatCategory = featCategory;
            }
            
            public override void AssertEquals(TraitType traitType)
            {
                HasFeatByCategory tt = traitType as HasFeatByCategory;
                Assert.That(tt, Is.Not.Null);
                
                Assert.That(this.Name, Is.EqualTo(tt.Name));
                Assert.That(this.FeatCategory, Is.EqualTo(tt.FeatCategory?.Name));
            }
        }

        public class HeroicInspirationModel : TraitTypeModel
        {
            public bool IsInspired { get; set; }
            public HeroicInspirationModel(string name, bool isInspired) : base(name)
            {
                this.IsInspired = isInspired;
            }
            
            public override void AssertEquals(TraitType traitType)
            {
                HeroicInspiration tt = traitType as HeroicInspiration;
                Assert.That(tt, Is.Not.Null);

                Assert.That(this.Name, Is.EqualTo(tt.Name));
                Assert.That(this.IsInspired, Is.EqualTo(tt.IsInspired));
            }
        }

        public class ProficiencyModel : TraitTypeModel
        {
            public string Skill { get; set; }
            public ProficiencyModel(string name, string skill) : base(name)
            {
                this.Skill = skill;
            }
            
            public override void AssertEquals(TraitType traitType)
            {
                Proficiency tt = traitType as Proficiency;
                Assert.That(tt, Is.Not.Null);
                
                Assert.That(this.Name, Is.EqualTo(tt.Name));
                Assert.That(this.Skill, Is.EqualTo(tt.Skill?.Name));
            }
        }

        public class SpeedBoostModel : TraitTypeModel
        {
            public float SpeedMultiplier { get; set; }
            public SpeedBoostModel(string name, float speedMultiplier) : base(name)
            {
                this.SpeedMultiplier = speedMultiplier;
            }
            
            public override void AssertEquals(TraitType traitType)
            {
                SpeedBoost tt = traitType as SpeedBoost;
                Assert.That(tt, Is.Not.Null);

                Assert.That(this.Name, Is.EqualTo(tt.Name));
                Assert.That(this.SpeedMultiplier, Is.EqualTo(tt.speedMultiplier));
            }
        }
    }
}