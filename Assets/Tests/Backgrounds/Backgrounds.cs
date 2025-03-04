using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Characters.Backgrounds;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    public class Backgrounds
    {
        private Background[] _backgrounds;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Background)}");
            _backgrounds =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Background>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(BackgroundsData), nameof(BackgroundsData.BackgroundsTestCases))]
        public bool TestAllBackgroundExistence(string name)
        {
            return _backgrounds.Any(ability => ability.name == name);
        }
        
        private class BackgroundsData
        {
            public static IEnumerable BackgroundsTestCases
            {
                get
                {
                    yield return new TestCaseData("Acolyte").Returns(true);
                }
            }
        }
    }
}