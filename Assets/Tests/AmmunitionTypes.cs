using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Weapons;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    [TestFixture]
    public class AmmunitionTypes
    {
        private AmmunitionType[] _ammunitionTypes;

        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(AmmunitionType)}");
            _ammunitionTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<AmmunitionType>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AmmunitionTypesData), nameof(AmmunitionTypesData.AmmunitionTypesTestCases))]
        public void TestAllAmmunitionTypes(AmmunitionTypeTestModel expected)
        {
            var ammunitionType = _ammunitionTypes.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(ammunitionType, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.AmmunitionTypes, expected.Name));
            Assert.That(ammunitionType.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(ammunitionType.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(ammunitionType.Amount, Is.EqualTo(expected.Amount), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Amount), expected.Amount));
            Assert.That(ammunitionType.Storage.name, Is.EqualTo(expected.Storage), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Storage), expected.Storage));
            Assert.That(ammunitionType.Weight, Is.EqualTo(expected.Weight), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Weight), expected.Weight));
            Assert.That(ammunitionType.Cost, Is.EqualTo(expected.Cost), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Cost), expected.Cost));
        }
    }
    
    public class AmmunitionTypesData
    {
        public static IEnumerable AmmunitionTypesTestCases
                    {
                        get
                        {
                            yield return new TestCaseData(
                                new AmmunitionTypeTestModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.Arrows,
                                    Amount = 20,
                                    Cost =  1f,
                                    Weight = 0.5f,
                                    Storage = NameHelper.Storage.Quiver
                                });
                            
                            yield return new TestCaseData(
                                new AmmunitionTypeTestModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.Bolts,
                                    Amount = 20,
                                    Cost =  1f,
                                    Weight = 0.75f,
                                    Storage = NameHelper.Storage.Case
                                });

                            yield return new TestCaseData(
                                new AmmunitionTypeTestModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.BulletsFirearm,
                                    Amount = 10,
                                    Cost =  3f,
                                    Weight = 1f,
                                    Storage = NameHelper.Storage.Pouch
                                });

                            yield return new TestCaseData(
                                new AmmunitionTypeTestModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.BulletsSling,
                                    Amount = 20,
                                    Cost =  4f,
                                    Weight = 0.75f,
                                    Storage = NameHelper.Storage.Pouch
                                });
                            
                            yield return new TestCaseData(
                                new AmmunitionTypeTestModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.Needles,
                                    Amount = 50,
                                    Cost =  1f,
                                    Weight = 0.5f,
                                    Storage = NameHelper.Storage.Pouch
                                });
                        }
                    }
                    
    }
    
    public class AmmunitionTypeTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.AmmunitionTypes}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public int Amount { get; set; }
        public string Storage { get; set; }
        public float Weight { get; set; }
        public float Cost { get; set; }
    }
}