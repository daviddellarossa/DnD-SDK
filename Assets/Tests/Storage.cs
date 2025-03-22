using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests
{
    [TestFixture]
    public class Storage
    {
        private DnD.Code.Scripts.Characters.Storage.Storage[] _storages;

        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(DnD.Code.Scripts.Characters.Storage.Storage)}");
            _storages =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<DnD.Code.Scripts.Characters.Storage.Storage>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.StorageTestCases))]
        public void TestAllStandardLanguages(StorageModel expected)
        {
            var standardLanguage = _storages.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(standardLanguage, Is.Not.Null);
        }

        
        private class AbilitiesData
        {
            public static IEnumerable StorageTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new StorageModel(
                            NameHelper.Storage.Case
                        ));
                    yield return new TestCaseData(
                        new StorageModel(
                            NameHelper.Storage.Pouch
                        ));
                    yield return new TestCaseData(
                        new StorageModel(
                            NameHelper.Storage.Quiver
                        ));
                }
            }
        }

        public class StorageModel
        {
            public string Name { get; set; }

            public StorageModel(string name)
            {
                this.Name = name;
            }
        }
    }
}