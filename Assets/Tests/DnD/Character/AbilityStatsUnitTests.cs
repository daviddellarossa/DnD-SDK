using System.Collections;
using System.Collections.Generic;
using DnD.Code.Scripts;
using DnD.Code.Scripts.Characters;
using NUnit.Framework;

namespace Tests.Character
{
    [TestFixture]
    public class AbilityStatsUnitTests
    {
        [TestCaseSource(typeof(AbilityStatsTestData), nameof(AbilityStatsTestData.ModifierTestData))]
        public int Modifier_Should_Return_CorrectValues(int score)
        {
            var sut = new AbilityStats()
            {
                Score = score
            };

            return sut.Modifier;
        }
    }
    
        public class AbilityStatsTestData
    {
        public static IEnumerable ModifierTestData
        {
            get
            {
                yield return new TestCaseData(Constants.BasePassivePerception - 10).Returns(-5);
                yield return new TestCaseData(Constants.BasePassivePerception - 8).Returns(-4);
                yield return new TestCaseData(Constants.BasePassivePerception - 6).Returns(-3);
                yield return new TestCaseData(Constants.BasePassivePerception - 4).Returns(-2);
                yield return new TestCaseData(Constants.BasePassivePerception - 2).Returns(-1);
                yield return new TestCaseData(Constants.BasePassivePerception - 1).Returns(-1);
                yield return new TestCaseData(Constants.BasePassivePerception).Returns(0);
                yield return new TestCaseData(Constants.BasePassivePerception + 1).Returns(0);
                yield return new TestCaseData(Constants.BasePassivePerception + 2).Returns(1);
                yield return new TestCaseData(Constants.BasePassivePerception + 3).Returns(1);
                yield return new TestCaseData(Constants.BasePassivePerception + 4).Returns(2);
                yield return new TestCaseData(Constants.BasePassivePerception + 6).Returns(3);
                yield return new TestCaseData(Constants.BasePassivePerception + 8).Returns(4);
                yield return new TestCaseData(Constants.BasePassivePerception + 10).Returns(5);

            }
        }
    }
}