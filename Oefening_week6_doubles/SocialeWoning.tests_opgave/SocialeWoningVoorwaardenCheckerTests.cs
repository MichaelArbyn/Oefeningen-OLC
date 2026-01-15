using NUnit.Framework;
using SocialeWoning;
using System.Collections.Generic;

namespace SocialeWoning.tests_opgave
{
    /// <summary>
    /// Unit tests voor Req01: controleren of de voorwaardenchecker correct werkt.
    /// </summary>
    public class SocialeWoningVoorwaardenCheckerTests
    {
        private ISocialeWoningVoorwaardenChecker checker;

        [SetUp]
        public void Setup()
        {
            // Arrange: nieuwe checker voor elke test
            checker = new SocialeWoningVoorwaardenChecker();
        }

        /// <summary>
        /// Req01: leeftijd of inkomen fout → kandidaat voldoet niet.
        /// </summary>
        [TestCase(5, 0)]
        [TestCase(17, 0)]
        [TestCase(18, 50000)]
        [TestCase(20, 30000)]
        [TestCase(20, 50000)]
        public void VoldoetAanVoorwaarden_GegevenLeeftijdOfInkomenNietOk_DanFalse(int leeftijd, int inkomen)
        {
            // Arrange
            var kandidaat = new Kandidaat(leeftijd, inkomen, new Dictionary<string, bool>());

            // Act
            bool resultaat = checker.VoldoetAanVoorwaarden(kandidaat);

            // Assert
            Assert.That(resultaat, Is.False);
        }

        /// <summary>
        /// Req01: huisvestingsfeiten die niet toegestaan zijn → false.
        /// </summary>
        [TestCaseSource(nameof(FouteHuisvesting))]
        public void VoldoetAanVoorwaarden_GegevenHuisvestingNietOk_DanFalse(Dictionary<string, bool> huisvesting)
        {
            var kandidaat = new Kandidaat(20, 10000, huisvesting);
            bool resultaat = checker.VoldoetAanVoorwaarden(kandidaat);
            Assert.That(resultaat, Is.False);
        }

        private static IEnumerable<TestCaseData> FouteHuisvesting()
        {
            yield return new TestCaseData(new Dictionary<string, bool> { { "eigen woning", true } });
            yield return new TestCaseData(new Dictionary<string, bool> { { "woning in vruchtgebruik", true } });
            yield return new TestCaseData(new Dictionary<string, bool> { { "eigen woning", true }, { "woning onbewoonbaar", false } });
        }

        /// <summary>
        /// Req01: alle voorwaarden correct → true.
        /// </summary>
        [TestCaseSource(nameof(GoedeHuisvesting))]
        public void VoldoetAanVoorwaarden_GegevenAlleVoorwaardenOk_DanTrue(int leeftijd, int inkomen, Dictionary<string, bool> huisvesting)
        {
            var kandidaat = new Kandidaat(leeftijd, inkomen, huisvesting);
            bool resultaat = checker.VoldoetAanVoorwaarden(kandidaat);
            Assert.That(resultaat, Is.True);
        }

        private static IEnumerable<TestCaseData> GoedeHuisvesting()
        {
            yield return new TestCaseData(18, 29999, new Dictionary<string, bool>());
            yield return new TestCaseData(20, 20000, new Dictionary<string, bool> { { "eigen woning", true }, { "woning onbewoonbaar", true } });
            yield return new TestCaseData(18, 29999, new Dictionary<string, bool> { { "woning in vruchtgebruik", false } });
        }
    }
}