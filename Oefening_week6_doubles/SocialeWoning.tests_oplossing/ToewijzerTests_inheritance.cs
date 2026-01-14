using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeWoning.tests_oplossing
{
    public class ToewijzerTests_inheritance
    {

        // Test double: Stub die altijd true teruggeeft (kandidaat komt in aanmerking)
        private class StubVoorwaardenCheckerTrue : SocialeWoningVoorwaardenChecker
        {
            public override bool VoldoetAanVoorwaarden(Kandidaat kandidaat)
            {
                return true;
            }
        }

        // Test double: Stub die altijd false teruggeeft (kandidaat komt niet in aanmerking)
        private class StubVoorwaardenCheckerFalse : SocialeWoningVoorwaardenChecker
        {
            public override bool VoldoetAanVoorwaarden(Kandidaat kandidaat)
            {
                return false;
            }
        }

        // req02
        [Test]
        public void Toewijzen_GivenWoningBeschikbaarAndKandidaatKomtInAanmerking_ReturnsTrue()
        {
            // Arrange
            Toewijzer toewijzer = new Toewijzer();
            toewijzer.voowaardenChecker = new StubVoorwaardenCheckerTrue();
            Kandidaat kandidaat = new Kandidaat(25, 20000, new Dictionary<string, bool>());

            // Act
            bool result = toewijzer.Toewijzen(kandidaat);

            // Assert
            Assert.That(result, Is.True);
        }

        // req02
        [Test]
        public void Toewijzen_GivenKandidaatKomtNietInAanmerking_ReturnsFalse()
        {
            // Arrange
            Toewijzer toewijzer = new Toewijzer();
            toewijzer.voowaardenChecker = new StubVoorwaardenCheckerFalse();
            Kandidaat kandidaat = new Kandidaat(25, 20000, new Dictionary<string, bool>());

            // Act
            bool result = toewijzer.Toewijzen(kandidaat);

            // Assert
            Assert.That(result, Is.False);
        }

        // req02
        [Test]
        public void Toewijzen_GivenGeenWoningenBeschikbaar_ReturnsFalse()
        {
            // Arrange
            Toewijzer toewijzer = new Toewijzer();
            toewijzer.voowaardenChecker = new StubVoorwaardenCheckerTrue();
            Kandidaat kandidaat1 = new Kandidaat(25, 20000, new Dictionary<string, bool>());
            Kandidaat kandidaat2 = new Kandidaat(30, 15000, new Dictionary<string, bool>());

            // Act eerste toewijzing gebruikt de enige beschikbare woning
            // Tweede toewijzing moet falen omdat er geen woningen meer zijn
            bool result1 = toewijzer.Toewijzen(kandidaat1);
            bool result2 = toewijzer.Toewijzen(kandidaat2);

            // Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.False);
        }
    }
}
