using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Abilities;
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
        public void TestAllSkills(SpeciesModel expected)
        {
            var species = _species.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(species, Is.Not.Null);
            Assert.That(species.InheritFrom, Is.EqualTo(expected.InheritFrom));
            Assert.That(species.CreatureType.Name, Is.EqualTo(expected.CreatureType));
            Assert.That(species.Size, Is.EqualTo(expected.Size));
            Assert.That(species.Speed, Is.EqualTo(expected.Speed));
            Assert.That(species.Traits.Select(tr => tr.Name), Is.EquivalentTo(expected.Traits));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable SpeciesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new SpeciesModel(
                            Helper.Species.Human,
                            null,
                            Helper.CreatureTypes.Humanoid,
                            DnD.Code.Scripts.Characters.Species.Size.Small | DnD.Code.Scripts.Characters.Species.Size.Medium,
                            9.144f,
                            new []
                            {
                                Helper.HumanSpecialTraits.Resourceful,
                                Helper.HumanSpecialTraits.Skillful,
                                Helper.HumanSpecialTraits.Versatile,

                            }
                        ));
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
            public string[] Traits { get; set; }

            public SpeciesModel(
                string name,
                string inheritFrom,
                string creatureType,
                DnD.Code.Scripts.Characters.Species.Size size,
                float speed,
                string[] traits
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
    }
}