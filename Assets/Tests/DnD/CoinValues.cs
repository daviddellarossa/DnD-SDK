using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Helpers.NameHelper;
using NUnit.Framework;
using UnityEditor;

namespace Tests.DnD
{
    [TestFixture]
    public class CoinValuesUnitTests
    {
        private CoinValue[] _coinValues;

        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(CoinValue)}");
            _coinValues =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<CoinValue>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.CoinValueTestCases))]
        public void TestAllCreatureTypes(CoinValueTestModel expected)
        {
            var coinValue = _coinValues.SingleOrDefault(cv => cv.name == expected.Name);
            
            Assert.That(coinValue, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Coins, expected.Name));
            Assert.That(coinValue.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(coinValue.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(coinValue.Value, Is.EqualTo(expected.Value), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Value), expected.Value));
        }

        private class AbilitiesData
        {
            public static IEnumerable CoinValueTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new CoinValueTestModel()
                        {
                            Name = NameHelper.CoinValues.CopperPiece,
                            Value = 1f
                        });
                    yield return new TestCaseData(
                        new CoinValueTestModel()
                        {
                            Name = NameHelper.CoinValues.SilverPiece,
                            Value = 10f
                        });
                    yield return new TestCaseData(
                        new CoinValueTestModel()
                        {
                            Name = NameHelper.CoinValues.ElectrumPiece,
                            Value = 50f
                        });
                    yield return new TestCaseData(
                        new CoinValueTestModel()
                        {
                            Name = NameHelper.CoinValues.GoldPiece,
                            Value = 100f
                        });
                    yield return new TestCaseData(
                        new CoinValueTestModel()
                        {
                            Name = NameHelper.CoinValues.PlatinumPiece,
                            Value = 1000f
                        });
                }
            }
        }

        public class CoinValueTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Coins}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            public string Abbreviation => $"{DisplayName}.{NameHelper.Naming.Abbreviation}";
            public float Value { get; set; }
        }
    }
}