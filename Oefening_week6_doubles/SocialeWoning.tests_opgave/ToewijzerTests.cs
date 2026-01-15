using NUnit.Framework;
using SocialeWoning;
using System.Collections.Generic;

namespace SocialeWoning.tests_opgave
{
    /// <summary>
    /// Unit tests voor Req02: testen van de toewijzingslogica.
    /// Test double wordt gebruikt om REQ01 te isoleren.
    /// </summary>
    public class ToewijzerTests
    {
        /// <summary>
        /// Req02: geldige kandidaat + woning beschikbaar → true
        /// </summary>
        [Test]
        public void Toewijzen_GeldigeKandidaatEnWoningBeschikbaar_DanTrue()
        {
            // Arrange
            var checker = new CheckerStub(true); // kandidaat is geldig
            var toewijzer = new Toewijzer { VoowaardenChecker = checker };
            var kandidaat = new Kandidaat(20, 20000, new Dictionary<string, bool>());

            // Act
            bool resultaat = toewijzer.Toewijzen(kandidaat);

            // Assert
            Assert.That(resultaat, Is.True);
            Assert.That(toewijzer.AantalBeschikbareWoningen, Is.EqualTo(0));
        }

        /// <summary>
        /// Req02: geldige kandidaat maar geen woningen meer → false
        /// </summary>
        [Test]
        public void Toewijzen_GeldigeKandidaatMaarGeenWoningBeschikbaar_DanFalse()
        {
            // Arrange
            var checker = new CheckerStub(true);
            var toewijzer = new Toewijzer { VoowaardenChecker = checker };
            var kandidaat = new Kandidaat(20, 20000, new Dictionary<string, bool>());

            // Eerste toewijzing verbruikt de woning
            toewijzer.Toewijzen(kandidaat);

            // Act
            bool resultaat = toewijzer.Toewijzen(kandidaat);

            // Assert
            Assert.That(resultaat, Is.False);
            Assert.That(toewijzer.AantalBeschikbareWoningen, Is.EqualTo(0));
        }

        /// <summary>
        /// Req02: kandidaat voldoet niet aan voorwaarden → false
        /// </summary>
        [Test]
        public void Toewijzen_KandidaatVoldoetNietAanVoorwaarden_DanFalse()
        {
            // Arrange
            var checker = new CheckerStub(false); // kandidaat ongeldig
            var toewijzer = new Toewijzer { VoowaardenChecker = checker };
            var kandidaat = new Kandidaat(20, 20000, new Dictionary<string, bool>());

            // Act
            bool resultaat = toewijzer.Toewijzen(kandidaat);

            // Assert
            Assert.That(resultaat, Is.False);
            Assert.That(toewijzer.AantalBeschikbareWoningen, Is.EqualTo(1));
        }
    }
}