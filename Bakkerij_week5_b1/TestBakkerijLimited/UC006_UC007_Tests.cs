using BakkerijLimited.Controllers;
using NUnit.Framework;

namespace TestBakkerijLimited
{
    [TestFixture]
    public class UseCase_KortingTests
    {
        private BakkerijController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new BakkerijController();
        }

        [Test]
        public void UC006_UC007_Scenario1_KortingWordtCorrectToegepast()
        {
            //Arrange
            _controller.StartNieuweBestelling("Michael");
            var kaart = _controller.MaakNieuweKlantenkaart();
            kaart.AantalBroden = 10;

            _controller.VoegBroodToeAanBestelling("Wit", 3);
            _controller.VoegBestellingToeAanKlantenkaart();

            //Act
            bool kortingMogelijk = _controller.CheckKortingMogelijk();
            decimal korting = _controller.BerekenKorting();

            //Assert
            Assert.IsTrue(kortingMogelijk, "Korting moet mogelijk zijn bij 10 broden.");
            Assert.That(korting, Is.EqualTo(3.50m), "Korting moet gelijk zijn aan 1 gratis wit brood.");
            Assert.That(kaart.AantalBroden, Is.EqualTo(0), "Aantal broden op kaart moet verminderd worden met 10.");
        }
    }
}