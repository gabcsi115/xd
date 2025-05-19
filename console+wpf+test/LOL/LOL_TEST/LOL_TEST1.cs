using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using LolCLI;

namespace LolCLI.Tests
{
    [TestClass]
    public class HosTests
    {
        [TestMethod]
        public void HpErtek_TestCases()
        {
            var testData = new[]
            {
                new { Level = 10, ExpectedHp = 1500.0 },
                new { Level = 1, ExpectedHp = 600.0 },
                new { Level = 5, ExpectedHp = 1000.0 },
                new { Level = 18, ExpectedHp = 2300.0 }
            };

            var line = "Parzival;a mágányos Hős;Fighter;A cél a kulcs! A cél a tojás!;500;100;0;0;0;0";
            var hero = new Hos(line);
            var heroes = new List<Hos> { hero };

            foreach (var test in testData)
            {
                var result = Program.HpErtek("Parzival", test.Level, heroes);
                Assert.AreEqual(test.ExpectedHp, result, 0.001, $"Level {test.Level} test failed.");
            }
        }
    }
}