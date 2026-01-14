namespace SocialeWoning.tests_oplossing
{
 
    public class SocialeWoningVoorwaardenCheckerTests
    {
        private SocialeWoningVoorwaardenChecker checker;

        [SetUp]
        public void Setup()
        {
            checker = new();
        }

        //req01  leeftijd en inkomen niet ok
        [TestCase(5, 0)]
        [TestCase(17, 0)]
        [TestCase(18, 50000)]
        [TestCase(20, 30000)]
        [TestCase(20, 50000)]
        public void VoldoetAanVoorwaarden_Given1VoorwaardeFaalt_ThenVoldoetNiet(int leeftijd, int inkomen)
        {
            Kandidaat kandidaat = new Kandidaat(leeftijd, inkomen, null!);
            Assert.That(checker.VoldoetAanVoorwaarden(kandidaat), Is.False);
        }

        
        //req01 niet ok
        [TestCaseSource(nameof(SimpeleHuisvesting))]
        public void VoldoetAanVoorwaarden_Given1VoorwaardeFaalt_ThenVoldoetNiet(Dictionary<string, bool> huisvesting)
        {
            Kandidaat kandidaat = new Kandidaat(20, 10000, huisvesting);
            Assert.That(checker.VoldoetAanVoorwaarden(kandidaat), Is.False);
        }
        private static IEnumerable<TestCaseData> SimpeleHuisvesting()
        {
            yield return new TestCaseData(new Dictionary<string, bool> { { "eigen woning", true } });
            yield return new TestCaseData(new Dictionary<string, bool> { { "woning in vruchtgebruik", true } });
            yield return new TestCaseData(new Dictionary<string, bool> { { "eigen woning", true }, { "woning onbewoonbaar", false }, });
        }



        //req01 huisvestingsfeiten wel ok
        [TestCaseSource(nameof(OKHuisvesting))]
        public void VoldoetAanVoorwaarden_GivenVoorwaardenok_ThenVoldoet(int leeftijd, int inkomen, Dictionary<string, bool> huisvesting)
        {
            Kandidaat kandidaat = new Kandidaat(leeftijd, inkomen, huisvesting);
            Assert.That(checker.VoldoetAanVoorwaarden(kandidaat), Is.True);
        }
        private static IEnumerable<TestCaseData> OKHuisvesting()
        {
            yield return new TestCaseData(18, 29999, new Dictionary<string, bool> { { "eigen woning", false } });
            yield return new TestCaseData(18, 29999, new Dictionary<string, bool> { { "woning in vruchtgebruik", false } });
            yield return new TestCaseData(20, 20000, new Dictionary<string, bool> { });
            yield return new TestCaseData(18, 29999, new Dictionary<string, bool> { { "eigen woning", true }, { "woning onbewoonbaar", true } });
            yield return new TestCaseData(20, 20000, new Dictionary<string, bool> { { "eigen woning", true }, { "woning onbewoonbaar", true }, { "woning in vruchtgebruik", false } });
        }
    }
}