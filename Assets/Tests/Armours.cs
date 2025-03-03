using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Characters.Abilities;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    public class Armours
    {
        private Armour[] _armours;
        private Shield[] _shields;

        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Armour)}");
            _armours =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Armour>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(Shield)}");
            _shields =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Shield>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ArmoursTestCases))]
        public bool TestAllArmours(string name, string armourTypeName)
        {
            return _armours.Any(armour => armour.name == name && armour.Type.name == armourTypeName);
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ShieldsTestCases))]
        public bool TestAllShields(string name, string shieldTypeName)
        {
            return _shields.Any(shield => shield.name == name && shield.Type.name == shieldTypeName);
        }
        
        private class AbilitiesData
        {
            public static IEnumerable ArmoursTestCases
            {
                get
                {
                    yield return new TestCaseData("Chain Mail", "Heavy Armour Type").Returns(true);
                    yield return new TestCaseData("Plate Armour", "Heavy Armour Type").Returns(true);
                    yield return new TestCaseData("Ring Mail", "Heavy Armour Type").Returns(true);
                    yield return new TestCaseData("Splint Armour", "Heavy Armour Type").Returns(true);

                    yield return new TestCaseData("Leather Armour", "Light Armour Type").Returns(true);
                    yield return new TestCaseData("Padded Armour", "Light Armour Type").Returns(true);
                    yield return new TestCaseData("Studded Leather Armour", "Light Armour Type").Returns(true);

                    yield return new TestCaseData("Breastplate", "Medium Armour Type").Returns(true);
                    yield return new TestCaseData("Chain Shirt", "Medium Armour Type").Returns(true);
                    yield return new TestCaseData("Half Plate Armour", "Medium Armour Type").Returns(true);
                    yield return new TestCaseData("Hide Armour", "Medium Armour Type").Returns(true);
                    yield return new TestCaseData("Scale Mail", "Medium Armour Type").Returns(true);
                }
            }
            
            public static IEnumerable ShieldsTestCases
            {
                get
                {
                    yield return new TestCaseData("Shield", "Shield Type").Returns(true);
                }
            }
        }
    }
}