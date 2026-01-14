using BakkerijLimited.Domain;
using BakkerijLimited.Models;
using NUnit.Framework;

namespace TestBakkerijLimited
{
    public class UC006_UC007_KortingTests
    {
        private Bakkerij _bakkerij;

        [SetUp]
        public void Setup()
        {
            _bakkerij = new Bakkerij();
            _bakkerij.Klant = new Klant("Louis");
        }

        // -------------------------------------------------------------
        // UC-006 — Kortingsmogelijkheid checken
        // -------------------------------------------------------------

        [Test]
        public void UC006_GeenKortingMogelijk_AantalBrodenOnderLimiet()
        {
            // ARRANGE
            var bakkerij = new Bakkerij();
            bakkerij.Klant = new Klant("Michael");
            var kaart = bakkerij.MaakKlantenkaart();
            kaart.AantalBroden = 9; // onder limiet

            // ACT
            bool resultaat = bakkerij.CheckKortingMogelijk();

            // ASSERT
            Assert.IsFalse(resultaat, "Er mag geen korting mogelijk zijn onder de limiet van 10 broden.");
        }

        [Test]
        public void UC006_KortingMogelijk_AantalBrodenBereiktLimiet()
        {
            // ARRANGE
            var bakkerij = new Bakkerij();
            bakkerij.Klant = new Klant("Michael");
            var kaart = bakkerij.MaakKlantenkaart();
            kaart.AantalBroden = 10; // limiet bereikt

            // ACT
            bool resultaat = bakkerij.CheckKortingMogelijk();

            // ASSERT
            Assert.IsTrue(resultaat, "Korting moet mogelijk zijn bij 10 of meer broden.");
        }

        [Test]
        public void UC006_GeenKortingMogelijk_GeenKlantenkaartGeselecteerd()
        {
            // ARRANGE
            var bakkerij = new Bakkerij();
            bakkerij.Klant = new Klant("Michael");
            // geen kaart geselecteerd

            // ACT
            bool resultaat = bakkerij.CheckKortingMogelijk();

            // ASSERT
            Assert.IsFalse(resultaat, "Zonder geselecteerde klantenkaart kan geen korting mogelijk zijn.");
        }
        // -------------------------------------------------------------
        // UC-007 — Korting berekenen en klantenkaart bijwerken
        // -------------------------------------------------------------

        [Test]
        public void UC007_GeenKorting_BrodenOnderLimiet()
        {
            // ARRANGE
            var bakkerij = new Bakkerij();
            bakkerij.Klant = new Klant("Michael");
            var kaart = bakkerij.MaakKlantenkaart();
            kaart.AantalBroden = 9; // onder limiet

            var bestelling = new Bestelling(bakkerij.Klant, Bakkerij.WitBrood, 2);
            bakkerij.MaakBestelling(bakkerij.Klant, Bakkerij.WitBrood, 2);

            // ACT
            decimal korting = bakkerij.BerekenKortingEnWerkBij();

            // ASSERT
            Assert.That(korting, Is.EqualTo(0.0m), "Er mag geen korting toegekend worden bij minder dan 10 broden.");
            Assert.That(kaart.AantalBroden, Is.EqualTo(9), "Aantal broden op kaart mag niet wijzigen.");
        }

        [Test]
        public void UC007_KortingVoor1GratisBrood_Bij10Broden()
        {
            // ARRANGE
            var bakkerij = new Bakkerij();
            bakkerij.Klant = new Klant("Michael");
            var kaart = bakkerij.MaakKlantenkaart();
            kaart.AantalBroden = 10; // limiet bereikt

            var bestelling = new Bestelling(bakkerij.Klant, Bakkerij.BruinBrood, 3);
            bakkerij.MaakBestelling(bakkerij.Klant, Bakkerij.BruinBrood, 3);

            // ACT
            decimal korting = bakkerij.BerekenKortingEnWerkBij();

            // ASSERT
            Assert.That(korting, Is.EqualTo(3.50m), "Korting moet gelijk zijn aan prijs van 1 brood.");
            Assert.That(kaart.AantalBroden, Is.EqualTo(0), "Broden moeten verminderd worden met 10.");
        }

        [Test]
        public void UC007_KortingVoor2GratisBroden_Bij20Broden()
        {
            // ARRANGE
            var bakkerij = new Bakkerij();
            bakkerij.Klant = new Klant("Michael");
            var kaart = bakkerij.MaakKlantenkaart();
            kaart.AantalBroden = 20;

            var bestelling = new Bestelling(bakkerij.Klant, Bakkerij.WitBrood, 5);
            bakkerij.MaakBestelling(bakkerij.Klant, Bakkerij.WitBrood, 5);

            // ACT
            decimal korting = bakkerij.BerekenKortingEnWerkBij();

            // ASSERT
            Assert.That(korting, Is.EqualTo(7.00m), "Korting moet gelijk zijn aan 2 gratis broden.");
            Assert.That(kaart.AantalBroden, Is.EqualTo(0), "Broden moeten verminderd worden met 20.");
        }

        [Test]
        public void UC007_MaxKortingBeperktTotAantalBesteldeBroden()
        {
            // ARRANGE
            var bakkerij = new Bakkerij();
            bakkerij.Klant = new Klant("Michael");
            var kaart = bakkerij.MaakKlantenkaart();
            kaart.AantalBroden = 30; // zou 3 gratis broden geven

            var bestelling = new Bestelling(bakkerij.Klant, Bakkerij.WitBrood, 2); // maar slechts 2 besteld
            bakkerij.MaakBestelling(bakkerij.Klant, Bakkerij.WitBrood, 2);

            // ACT
            decimal korting = bakkerij.BerekenKortingEnWerkBij();

            // ASSERT
            Assert.That(korting, Is.EqualTo(7.00m), "Korting mag niet meer zijn dan waarde van 2 broden.");
            Assert.That(kaart.AantalBroden, Is.EqualTo(10), "Slechts 20 broden mogen afgetrokken worden.");
        }
    }

}