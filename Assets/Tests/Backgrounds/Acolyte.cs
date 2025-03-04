using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Characters.Backgrounds;
using NUnit.Framework;
using Tests;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

public class Acolyte
{
    private Background _acolyte;
    
    [SetUp]
    public void Setup()
    {
        string[] guids = AssetDatabase.FindAssets($"t:{nameof(Background)}");
        _acolyte =  guids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<Background>)
            .Where(asset => asset != null)
            .SingleOrDefault(asset => asset.name == Helper.Backgrounds.Acolyte);
    }

    [Test]
    public void AcolyteExists()
    {
        Assert.NotNull(_acolyte);
    }
    
    [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.OptionsTestCases))]
    public void TestOptions(string optionName, (string, int)[] equipment)
    {
        var option = _acolyte.StartingEquipment.SingleOrDefault(asset => asset.name == optionName) ;
        Assert.IsNotNull(option);
        
        foreach (var equipmentItem in equipment)
        {
            var equipmentCheckResult = option.Items.SingleOrDefault(item =>
                item.Item.name == equipmentItem.Item1 && item.Amount == equipmentItem.Item2);
            Assert.IsNotNull(equipmentCheckResult, $"Equipment  {equipmentItem.Item1} check failed.");
        }
    }
    
    [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.AbilitiesTestCases))]
    public bool TestAbilities(AbilityEnum abilityEnum)
    {
        return _acolyte.Abilities.Select(ability => ability.ToString()).SingleOrDefault(ability => ability == abilityEnum.ToString()) == abilityEnum.ToString();
    }
    
    [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.SkillProficienciesTestCases))]
    public bool TestSkillProficiencies(string skillName)
    {
        return _acolyte.SkillProficiencies.SingleOrDefault(skill => skill.name == skillName)  != null;
    }
    
    [TestCaseSource(typeof(AcolyteData), nameof(AcolyteData.FeatTestCases))]
    public void TestFeat(string feat)
    {
        Assert.AreEqual(_acolyte.Feat.name, feat);
    }
    
    private class AcolyteData
    {
        public static IEnumerable OptionsTestCases
        {
            get
            {
                yield return new TestCaseData(
                    "Acolyte's Option A", 
                    new (string, int)[]
                    {
                        ("Acolyte's Calligrapher's supplies", 1),
                        ("Acolyte's prayers book", 1),
                        ("Acolyte's Holy symbol", 1),
                        ("Acolyte's Parchment", 10),
                        ("Acolyte's Robe", 1),
                        ("GoldPiece", 8)
                    });
                yield return new TestCaseData(
                    "Acolyte's Option B", 
                    new (string, int)[]
                    {
                        ("GoldPiece", 50)
                    });
            }
        }

        public static IEnumerable AbilitiesTestCases
        {
            get
            {
                yield return new TestCaseData(AbilityEnum.Charisma).Returns(true);
                yield return new TestCaseData(AbilityEnum.Constitution).Returns(false);
                yield return new TestCaseData(AbilityEnum.Dexterity).Returns(false);
                yield return new TestCaseData(AbilityEnum.Intelligence).Returns(true);
                yield return new TestCaseData(AbilityEnum.Strength).Returns(false);
                yield return new TestCaseData(AbilityEnum.Wisdom).Returns(true);
            }
        }

        public static IEnumerable SkillProficienciesTestCases
        {
            get
            {
                yield return new TestCaseData(Helper.Skills.Acrobatics).Returns(false);
                yield return new TestCaseData(Helper.Skills.AnimalHandling).Returns(false);
                yield return new TestCaseData(Helper.Skills.Arcana).Returns(false);
                yield return new TestCaseData(Helper.Skills.Athletics).Returns(false);
                yield return new TestCaseData(Helper.Skills.Deception).Returns(false);
                yield return new TestCaseData(Helper.Skills.History).Returns(false);
                yield return new TestCaseData(Helper.Skills.Insight).Returns(true);
                yield return new TestCaseData(Helper.Skills.Intimidation).Returns(false);
                yield return new TestCaseData(Helper.Skills.Investigation).Returns(false);
                yield return new TestCaseData(Helper.Skills.Medicine).Returns(false);
                yield return new TestCaseData(Helper.Skills.Nature).Returns(false);
                yield return new TestCaseData(Helper.Skills.Perception).Returns(false);
                yield return new TestCaseData(Helper.Skills.Performance).Returns(false);
                yield return new TestCaseData(Helper.Skills.Persuasion).Returns(false);
                yield return new TestCaseData(Helper.Skills.Religion).Returns(true);
                yield return new TestCaseData(Helper.Skills.SleightOfHand).Returns(false);
                yield return new TestCaseData(Helper.Skills.Stealth).Returns(false);
                yield return new TestCaseData(Helper.Skills.Survival).Returns(false);
            }
        }

        public static IEnumerable FeatTestCases
        {
            get
            {
                yield return new TestCaseData("Magic Initiate");
            }
        }
    }
}
