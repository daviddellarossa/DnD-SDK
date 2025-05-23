using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Storage;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests.DnD
{
    [TestFixture]
    public class StorageUnitTests
    {
        private Storage[] _storages;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Storage)}");
            _storages =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Storage>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.StorageTestCases))]
        public void TestAllStandardLanguages(StorageTestModel expected)
        {
            var storage = _storages.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(storage, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Storage, expected.Name));
            Assert.That(storage.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(storage.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
        }

        
        private class AbilitiesData
        {
            public static IEnumerable StorageTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new StorageTestModel(){
                            Name = NameHelper.Storage.Case
                        });
                    yield return new TestCaseData(
                        new StorageTestModel(){
                            Name = NameHelper.Storage.Pouch
                        });
                    yield return new TestCaseData(
                        new StorageTestModel(){
                            Name = NameHelper.Storage.Quiver
                        });
                }
            }
        }

        public class StorageTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Storage}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        }
    }
}