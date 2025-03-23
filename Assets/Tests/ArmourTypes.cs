using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests
{
    [TestFixture]
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
        public void TestAllArmourTypes(ArmourTypeTestModel expected)
        {
            var armourType = _armourTypes.SingleOrDefault(armourType => armourType.name == expected.Name);
            
            Assert.That(armourType, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.ArmourTypes, expected.Name));
            Assert.That(armourType.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(armourType.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(armourType.TimeInMinutesToDoff, Is.EqualTo(expected.TimeInMinutesToDoff), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.TimeInMinutesToDoff), expected.TimeInMinutesToDoff));
            Assert.That(armourType.TimeInMinutesToDon, Is.EqualTo(expected.TimeInMinutesToDon), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.TimeInMinutesToDon), expected.TimeInMinutesToDon));
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.ShieldTypeTestCases))]
        public void TestAllShieldTypes(ShieldTypeTestModel expected)
        {
            var shieldType = _shieldTypes.SingleOrDefault(d => d.name == expected.Name);

            Assert.That(shieldType, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.ShieldTypes, expected.Name));
            Assert.That(shieldType.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(shieldType.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
        }

        private class AbilitiesData
        {
            public static IEnumerable ArmourTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ArmourTypeTestModel()
                        {
                            Name = NameHelper.ArmourType.HeavyArmour,
                            TimeInMinutesToDon = 10,
                            TimeInMinutesToDoff = 5
                        });
                    yield return new TestCaseData(
                        new ArmourTypeTestModel()
                        {
                            Name = NameHelper.ArmourType.LightArmour,
                            TimeInMinutesToDon = 1,
                            TimeInMinutesToDoff = 1
                        });
                    yield return new TestCaseData(
                        new ArmourTypeTestModel()
                        {
                            Name = NameHelper.ArmourType.MediumArmour,
                            TimeInMinutesToDon = 5,
                            TimeInMinutesToDoff = 1
                        });
                }
            }
            
            public static IEnumerable ShieldTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ShieldTypeTestModel()
                        {
                            Name = NameHelper.ArmourType.Shield
                        });
                }
            }
        }
    }
    
    public class ArmourTypeTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.ArmourTypes}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public int TimeInMinutesToDon { get; set; }
        public int TimeInMinutesToDoff { get; set; }
    }
    public class ShieldTypeTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.ShieldTypes}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
    }
}