using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace SocialeWoning.tests_oplossing
{
    public class ToewijzerTests_NSub
    {
        // req02
        [Test]
        public void Toewijzen_GivenWoningBeschikbaarAndKandidaatKomtInAanmerking_ReturnsTrue()
        {
            // Arrange
            Toewijzer toewijzer = new Toewijzer();
            var stubChecker = Substitute.For<SocialeWoningVoorwaardenChecker>();
            stubChecker.VoldoetAanVoorwaarden(Arg.Any<Kandidaat>()).Returns(true);
            toewijzer.voowaardenChecker = stubChecker;
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
            var stubChecker = Substitute.For<SocialeWoningVoorwaardenChecker>();
            stubChecker.VoldoetAanVoorwaarden(Arg.Any<Kandidaat>()).Returns(false);
            toewijzer.voowaardenChecker = stubChecker;
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
            var stubChecker = Substitute.For<SocialeWoningVoorwaardenChecker>();
            stubChecker.VoldoetAanVoorwaarden(Arg.Any<Kandidaat>()).Returns(true);
            toewijzer.voowaardenChecker = stubChecker;
            Kandidaat kandidaat1 = new Kandidaat(25, 20000, new Dictionary<string, bool>());
            Kandidaat kandidaat2 = new Kandidaat(30, 15000, new Dictionary<string, bool>());

            // Act - eerste toewijzing gebruikt de enige beschikbare woning
            // Tweede toewijzing moet falen omdat er geen woningen meer zijn
            bool result1 = toewijzer.Toewijzen(kandidaat1);
            bool result2 = toewijzer.Toewijzen(kandidaat2);

            // Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.False);
        }
    }
}
