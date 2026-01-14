using BakkerijLimited.Controllers;
using BakkerijLimited.Models;

namespace TestBakkerij
{
    public class UC001_UC004_AllScenariosTests
    {
        private BakkerijController _controller;
        private Klantenkaart _bestaandeKlantenkaart;

        [SetUp]
        // Arrange - Act - Assert - Gemeenschappelijk deel voor de 3 scenario's
        // UC001: 1-2-3-4-5-6-7
        public void Setup()
        {
            _controller = new BakkerijController(); // Arrange
            _controller.StartNieuweBestelling("Louis"); // Act 1-2
            _bestaandeKlantenkaart = _controller.MaakNieuweKlantenkaart(); // Arrange
            IReadOnlyList<Brood> broden = _controller.HaalBeschikbareBrodenOp(); // Act 3
            Assert.That(broden, Has.Count.EqualTo(2)); // Assert 3
            bool toegevoegd = _controller.VoegBroodToeAanBestelling("wit brood", 2); // Act 4-5-6-7
            Assert.IsTrue(toegevoegd); // Assert 7
        }

        // Scenario 1: Klant bestelt, wil geen klantenkaart gebruiken, rekent af zonder korting.
        // UC001-vervolg: 8-9-10-
        // UC004: 1-2-3-4-5
        [Test]
        public void Scenario1_BestellenBetalen_ZonderKlantenkaart()
        {
            _controller.GebruikGeenKlantenkaart(); // Act UC001 8-9-10
            decimal korting = _controller.BerekenKorting(); // Act UC004 1-2-3
            Assert.That(korting, Is.EqualTo(0m)); // Assert UC004 3
            decimal totaalprijs = _controller.RekenAf(); // Act UC004 4-5
            Assert.That(totaalprijs, Is.EqualTo(7.00m)); // Assert UC004
        }

        // Scenario2: Klant bestelt, geeft nummer van klantenkaart, die gevonden wordt, en rekent af met eventuele korting.
        // UC001-vervolg: 8-9.1-9.1.1-10-
        // UC004: 1-2.1-2.1.1
        // UC005: 1
        // UC004: 4-5   
        [Test]
        public void Scenario2_BestellenBetalen_MetGevondenKlantenkaart()
        {
            // Act UC001 8-9.1-9.1.1
            Klantenkaart? klantenkaart = _controller.ZoekKlantenkaart(_bestaandeKlantenkaart.KlantenkaartNummer);
            Assert.That(klantenkaart, Is.Not.Null); // Assert UC001 9.1.1
            Bestelling? bestelling = _controller.HaalHuidigeBestellingOp(); // Act UC001 10
            Assert.That(bestelling, Is.Not.Null); // Assert UC001 10
            int aantalBrodenVoorToevoegen = klantenkaart!.AantalBroden;
            _controller.VoegBestellingToeAanKlantenkaart(); // Act UC004 1-2.1-2.1.1 UC005 1
            // Assert UC005 1
            Assert.That(klantenkaart.AantalBroden, Is.EqualTo(aantalBrodenVoorToevoegen + bestelling!.Aantal)); 
            decimal korting = _controller.BerekenKorting(); // Act UC006 enz
            decimal totaalprijs = _controller.RekenAf(); // Act UC004 4-5
            Assert.That(totaalprijs, Is.EqualTo(7.00m)); // Assert UC004
        }

        // Scenario3: Klant bestelt, geeft nummer van klantenkaart, die niet gevonden wordt.
        // UC001-vervolg: 8-9.1-9.1.1-8
        [Test]
        public void Scenario3_Bestellen_KlantenkaartNietGevonden()
        {
            // Act UC001 8-9.1-9.1.1
            Klantenkaart? klantenkaart = _controller.ZoekKlantenkaart(_bestaandeKlantenkaart.KlantenkaartNummer + 1);
            Assert.That(klantenkaart, Is.Null); // Assert UC001 9.1.1
            // Het systeem (I/O) gaat terug naar de stap waar de klant kan kiezen om een klantenkaart te gebruiken of niet (8)
        }


    }
}
