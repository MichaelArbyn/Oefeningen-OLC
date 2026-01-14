using BakkerijLimited.Controllers;
using BakkerijLimited.Models;

namespace TestBakkerij
{
    public class UC002_KlantenkaartOpzoekenTests
    {
        private BakkerijController _controller;
        private Klantenkaart _bestaandeKlantenkaart;

        [SetUp]
        public void Setup()
        {
            _controller = new BakkerijController();
            _controller.StartNieuweBestelling("Louis");
            _bestaandeKlantenkaart = _controller.MaakNieuweKlantenkaart();
        }

        [Test]
        // Scenario1: Een geldige klantenkaart wordt opgezocht en gevonden + matcht met klant.
        //       Pad: 1-2-3
        public void Scenario1_GeldigeKlantenkaart_WordtGevonden_MatchtKlant()
        {
            // Act
            Klantenkaart? kaart = _controller.ZoekKlantenkaart(_bestaandeKlantenkaart.KlantenkaartNummer);
            string kaartKlantMatch = _controller.doeKaartKlantMatch();  // deze methode ontbreekt nog in de controller

            // Assert
            Assert.That(kaart, Is.Not.Null);
            Assert.That(kaartKlantMatch, Is.EqualTo("Kaart met kaartnummer "
                            + _bestaandeKlantenkaart.KlantenkaartNummer + " komt overeen met klant Louis"));
        }

        [Test]
        // Scenario2: Een geldige klantenkaart wordt opgezocht en gevonden.
        //       Pad: 1-2.1-2.1.1
        public void Scenario2_OngeldigKaartnummer_GeeftNull()
        {
            // Act
            var kaart = _controller.ZoekKlantenkaart(_bestaandeKlantenkaart.KlantenkaartNummer+1);

            // Assert
            Assert.That(kaart, Is.Null);
        }

        [Test]
        // Scenario3: Een geldige klantenkaart wordt opgezocht en gevonden + mismatch met klant.
        //       Pad: 1-2-3.1-3.1.1
        public void Scenario3_GeldigeKlantenkaart_WordtGevonden_MatchtKlantNiet()
        {
            // Act
            _controller.StartNieuweBestelling("Adam");
            Klantenkaart? kaart = _controller.ZoekKlantenkaart(_bestaandeKlantenkaart.KlantenkaartNummer);
            string kaartKlantMatch = _controller.doeKaartKlantMatch();  // deze methode ontbreekt nog in de controller

            // Assert
            Assert.That(kaart, Is.Not.Null);
            Assert.That(kaartKlantMatch, Is.EqualTo("MISMATCH! - Kaart met kaartnummer "
                            + _bestaandeKlantenkaart.KlantenkaartNummer + " komt overeen met klant Louis"));
        }
    }
}
