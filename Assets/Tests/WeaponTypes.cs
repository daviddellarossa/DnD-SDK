using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Weapons;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    [TestFixture]
    public class WeaponTypes
    {
        private WeaponType[] _weaponTypes;

        [SetUp]
        public void Setup()
        {

            var guids = AssetDatabase.FindAssets($"t:{nameof(WeaponType)}");
            _weaponTypes =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<WeaponType>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(WeaponTypesData), nameof(WeaponTypesData.WeaponTypesTestCases))]
        public void TestAllWeaponTypes(WeaponTypeModel expected)
        {
            var weaponType = _weaponTypes.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(weaponType, Is.Not.Null);
            Assert.That(weaponType.DisplayName, Is.EqualTo(expected.DisplayName), $"{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(weaponType.DisplayDescription, Is.EqualTo(expected.DisplayDescription), $"{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");
        }
        
        private class WeaponTypesData
        {
            public static IEnumerable WeaponTypesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new WeaponTypeModel()
                        {
                            Name = NameHelper.WeaponTypes.MartialMeleeWeapon
                        });
                    yield return new TestCaseData(
                        new WeaponTypeModel()
                        {
                            Name = NameHelper.WeaponTypes.MartialRangedWeapon
                        });
                    yield return new TestCaseData(
                        new WeaponTypeModel()
                        {
                            Name = NameHelper.WeaponTypes.SimpleMeleeWeapon
                        });
                    yield return new TestCaseData(
                        new WeaponTypeModel()
                        {
                            Name = NameHelper.WeaponTypes.SimpleRangedWeapon
                        });
                }
            }
        }
        
        public class WeaponTypeModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.WeaponTypes}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        }
    }
}