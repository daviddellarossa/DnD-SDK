using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Common;
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
        public void TestAllArmourTypes(ArmourTypeModel armourTypeModel)
        {
            var armourType = _armourTypes.SingleOrDefault(ability => ability.name == armourTypeModel.Name);
            
            Assert.IsNotNull(armourType);
            Assert.AreEqual(armourType.TimeInMinutesToDoff, armourTypeModel.TimeInMinutesToDoff);
            Assert.AreEqual(armourType.TimeInMinutesToDon, armourTypeModel.TimeInMinutesToDon);
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
                    yield return new TestCaseData(
                        new ArmourTypeModel(
                        NameHelper.ArmourType.HeavyArmour,
                        10,
                        5
                        ));
                    yield return new TestCaseData(
                        new ArmourTypeModel(
                        NameHelper.ArmourType.LightArmour,
                        1,
                        1
                        ));
                    yield return new TestCaseData(
                        new ArmourTypeModel(
                        NameHelper.ArmourType.MediumArmour,
                        5,
                        1
                        ));
                }
            }
            
            public static IEnumerable ShieldTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(NameHelper.ArmourType.Shield).Returns(true);
                }
            }
        }

        public class ArmourTypeModel
        {
            public string Name { get; set; }
            public int TimeInMinutesToDon { get; set; }
            public int TimeInMinutesToDoff { get; set; }

            public ArmourTypeModel(string name, int timeInMinutesToDon, int timeInMinutesToDoff)
            {
                this.Name = name;
                this.TimeInMinutesToDon = timeInMinutesToDon;
                this.TimeInMinutesToDoff = timeInMinutesToDoff;
            }
        }
    }
}