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
        public void TestAllAmmunitionTypes(AmmunitionTypeModel expected)
        {
            var ammunitionType = _ammunitionTypes.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(ammunitionType, Is.Not.Null, $"AmmunitionType {expected.Name} not found.");
            Assert.That(ammunitionType.DisplayName, Is.EqualTo(expected.DisplayName), $"{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(ammunitionType.DisplayDescription, Is.EqualTo(expected.DisplayDescription), $"{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");
            Assert.That(ammunitionType.Amount, Is.EqualTo(expected.Amount), $"{expected.DisplayName}: {nameof(expected.Amount)} not equal to {expected.Amount}.");
            Assert.That(ammunitionType.Storage.name, Is.EqualTo(expected.Storage), $"{expected.DisplayName}: {nameof(expected.Storage)} not equal to {expected.Storage}.");
            Assert.That(ammunitionType.Weight, Is.EqualTo(expected.Weight), $"{expected.DisplayName}: {nameof(expected.Weight)} not equal to {expected.Weight}.");
            Assert.That(ammunitionType.Cost, Is.EqualTo(expected.Cost), $"{expected.DisplayName}: {nameof(expected.Cost)} not equal to {expected.Cost}.");
        }
    }

    public class AmmunitionTypesData
    {
        public static IEnumerable AmmunitionTypesTestCases
                    {
                        get
                        {
                            yield return new TestCaseData(
                                new AmmunitionTypeModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.Arrows,
                                    Amount = 20,
                                    Cost =  1f,
                                    Weight = 0.5f,
                                    Storage = NameHelper.Storage.Quiver
                                });
                            
                            yield return new TestCaseData(
                                new AmmunitionTypeModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.Bolts,
                                    Amount = 20,
                                    Cost =  1f,
                                    Weight = 0.75f,
                                    Storage = NameHelper.Storage.Case
                                });

                            yield return new TestCaseData(
                                new AmmunitionTypeModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.BulletsFirearm,
                                    Amount = 10,
                                    Cost =  3f,
                                    Weight = 1f,
                                    Storage = NameHelper.Storage.Pouch
                                });

                            yield return new TestCaseData(
                                new AmmunitionTypeModel()
                                {
                                    Name =  NameHelper.AmmunitionTypes.BulletsSling,
                                    Amount = 20,
                                    Cost =  4f,
                                    Weight = 0.75f,
                                    Storage = NameHelper.Storage.Pouch
                                });
                            
                            yield return new TestCaseData(
                                new AmmunitionTypeModel()
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
    
    public class AmmunitionTypeModel
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