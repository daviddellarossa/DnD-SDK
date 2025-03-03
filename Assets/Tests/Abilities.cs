using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Abilities;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    public class Abilities
    {
        private Ability[] _abilities;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Ability)}");
            _abilities =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Ability>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.TestCases))]
        public bool TestAllAbilities(string name)
        {
            return _abilities.Any(ability => ability.name == name);
        }

        private class AbilitiesData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData("Charisma").Returns(true);
                    yield return new TestCaseData("Constitution").Returns(true);
                    yield return new TestCaseData("Dexterity").Returns(true);
                    yield return new TestCaseData("Intelligence").Returns(true);
                    yield return new TestCaseData("Strength").Returns(true);
                    yield return new TestCaseData("Wisdom").Returns(true);
                }
            }
        }
    }
}