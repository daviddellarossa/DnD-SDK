using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Classes;
using NUnit.Framework;
using UnityEditor;

namespace Tests.DnD.Classes.Barbarian
{
    [TestFixture]
    public class LevelsUnitTests
    {
        private Level[] _levels;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Level)}", new []{ "Assets/DnD/Code/Instances/Classes/Barbarian/Levels"});
            _levels =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Level>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [Test]
        public void TestLevel()
        {
            
        }
        
        private class AbilitiesData
        {
            public static IEnumerable SpeciesTestCases
            {
                get
                {
                    yield return new TestCaseData();
                }
            }
        }

        public class LevelModel
        {
            public int Level { get; set; }
            public int ProficiencyBonus { get; set; }
            public ClassFeatureModel[] ClassFeatures { get; set; }
        }

        public class ClassFeatureModel
        {
            public string Name { get; set; }
        }
    }
}