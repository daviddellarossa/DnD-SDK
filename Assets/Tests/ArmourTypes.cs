using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Characters.Abilities;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    public class ArmourTypes
    {
        private ArmourType[] _armourTypes;
        private ShieldType[] _shieldTypes;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(ArmourType)}");
            _armourTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ArmourType>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(ShieldType)}");
            _shieldTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ShieldType>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ArmourTypeTestCases))]
        public bool TestAllArmourTypes(string name)
        {
            return _armourTypes.Any(ability => ability.name == name);
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ShieldTypeTestCases))]
        public bool TestAllShieldTypes(string name)
        {
            return _shieldTypes.Any(ability => ability.name == name);
        }

        private class AbilitiesData
        {
            public static IEnumerable ArmourTypeTestCases
            {
                get
                {
                    yield return new TestCaseData("Heavy Armour Type").Returns(true);
                    yield return new TestCaseData("Light Armour Type").Returns(true);
                    yield return new TestCaseData("Medium Armour Type").Returns(true);
                }
            }
            
            public static IEnumerable ShieldTypeTestCases
            {
                get
                {
                    yield return new TestCaseData("Shield Type").Returns(true);
                }
            }
        }
    }
}