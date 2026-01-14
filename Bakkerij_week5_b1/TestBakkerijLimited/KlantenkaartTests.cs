using BakkerijLimited.Models;
using System;
using System.Text.RegularExpressions;

namespace TestBakkerij
{
    public class KlantenkaartTests
    {
        private BakkerijLimited.Models.Klantenkaart _klantenkaart;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _klantenkaart = new BakkerijLimited.Models.Klantenkaart(1, new BakkerijLimited.Models.Klant("Jan"));
        }

        [Test]
        // REQ0018 – De bakkerij kan checken of de voorgelegde klantenkaart matcht met de klant
        public void KomtOvereenMet_ReturnsTrue_WhenSameKlantNaam()
        {
            // Arrange
            var kaart = new Klantenkaart(2, new Klant("Pietro"));

            // Act
            bool komtOvereen = kaart.KomtOvereenMet(new Klant("Pietro"));

            // Assert
            Assert.That(komtOvereen, Is.True);
        }

        [Test]
        // REQ0018 – De bakkerij kan checken of de voorgelegde klantenkaart matcht met de klant

        public void KomtOvereenMet_ReturnsFalse_WhenDifferentKlantNaam()
        {
            // Arrange
            var kaart = new Klantenkaart(3, new Klant("Yasmina"));

            // Act
            bool komtOvereen = kaart.KomtOvereenMet(new Klant("Phaedra"));

            // Assert
            Assert.That(komtOvereen, Is.False);
        }

        [Test]
        // REQ0018 – De bakkerij kan checken of de voorgelegde klantenkaart matcht met de klant

        public void GeefMatchMelding_ReturnsExpectedMessages_ForMatchAndMismatch()
        {
            // Arrange
            var kaart = new Klantenkaart(5, new Klant("Louis"));

            // Act
            string matchMsg = kaart.GeefMatchMelding(new Klant("Louis"));
            string mismatchMsg = kaart.GeefMatchMelding(new Klant("Adam"));

            // Assert
            Assert.That(matchMsg, Is.EqualTo("Kaart met kaartnummer 5 komt overeen met klant Louis"));
            Assert.That(mismatchMsg, Is.EqualTo("MISMATCH! - Kaart met kaartnummer 5 komt overeen met klant Louis"));
        }

        [Test]
        public void Klantenkaart_ShouldInitializeWithZeroBroden()
        {
            // Act
            int aantalBroden = _klantenkaart.AantalBroden;
            // Assert
            Assert.That(aantalBroden, Is.EqualTo(0));
        }

        // REQ0009 – De bakkerij kan het bestelde aantal broden aan de voorgelegde klantenkaart toevoegen
        [TestCase(0, 5, 5)]
        [TestCase(4, 5, 9)]
        [TestCase(9, 5, 14)]
        public void VoegBestellingToeAanKlantenkaart_ShouldIncreaseAantalBrodenCorrectly
                                            (int initAantal, int toeTeVoegenAantal, int nieuwAantal)
        {
            //Arrange
            _klantenkaart.AantalBroden = initAantal;
            // Act
            _klantenkaart.VoegBestellingToeAanKlantenkaart(toeTeVoegenAantal);
            int aantalBroden = _klantenkaart.AantalBroden;
            // Assert
            Assert.That(aantalBroden, Is.EqualTo(nieuwAantal));
        }
        // REQ0011 – De bakkerij kan checken of er meer dan 10 (of een andere limiet) broden op de klantenkaart staan
        [TestCase(9, false)]
        [TestCase(10, true)]
        [TestCase(15, true)]
        [TestCase(25, true)]
        public void CheckKortingMogelijk_ShouldEvaluateKortingMogelijkCorrectly
                                                    (int aantalBroden, bool mogelijkeKorting)
        {
            // Arrange
            _klantenkaart.VoegBestellingToeAanKlantenkaart(9);
            // Act
            bool kortingMogelijk = _klantenkaart.CheckKortingMogelijk();
            // Assert
            Assert.That(kortingMogelijk, Is.False);
        }

        // REQ0012 – De bakkerij kan de korting berekenen (11de brood gratis na aankoop van 10 broden) 
        // en de klantenkaart bijwerken door de aangekochte broden correct te verminderen
        [TestCase(7, 2, 0.0, 7)]
        [TestCase(12, 2, 3.50, 2)]
        [TestCase(12, 1, 3.50, 2)]
        [TestCase(25, 2, 7.00, 5)]
        [TestCase(25, 1, 3.50, 15)]
        public void BerekenEnWerkBij_ReturnsCorrectKortingAndReducesAantalBrodenCorrectly
                         (int initAantalBroden, int besteldAantalbroden, 
                            decimal verwachteKorting, int nieuwAantalBroden)
        {
            // Arrange
            _klantenkaart.VoegBestellingToeAanKlantenkaart(initAantalBroden);
            Bestelling bestelling = new Bestelling(new Klant("Ilias"), 
                BakkerijLimited.Domain.Bakkerij.WitBrood, besteldAantalbroden);
            // Act
            decimal korting = _klantenkaart.BerekenKortingEnWerkBij(bestelling);
            int aantalBrodenNaKorting = _klantenkaart.AantalBroden;
            // Assert
            Assert.That(korting, Is.EqualTo(verwachteKorting));
            Assert.That(aantalBrodenNaKorting, Is.EqualTo(nieuwAantalBroden)); 
        }
    }
}
