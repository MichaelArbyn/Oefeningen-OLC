using BakkerijLimited.Domain;
using BakkerijLimited.Models;

namespace TestBakkerijLimited
{
    public class BakkerijTests
    {
        private BakkerijLimited.Domain.Bakkerij _bakkerij;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _bakkerij = new BakkerijLimited.Domain.Bakkerij();
        }

        [Test]
        // REQ0001 Klant kan een bestelling voor een aantal broden aanmaken
        // Als dit nog niet gebeurd is, is de bestelling in uitvoering null
        public void Bakkerij_ShouldHaveNullBestelling_WhenConstructed()
        {
            // Act
            Bestelling? bestelling = _bakkerij.BestellingInUitvoering;
            // Assert
            Assert.That(bestelling, Is.Null);
        }

        [Test]
        // REQ0001 Klant kan een bestelling voor een aantal broden aanmaken
        // Als dit nog gebeurd is, is de bestelling in uitvoering bekend
        public void Bakkerij_ShouldHaveBestelling_AfterMaakBestelling()
        {
            // Act
            _bakkerij.MaakBestelling(new Klant("Hans Vandenbogaerde"),Bakkerij.BruinBrood, 3);
            Bestelling? bestelling = _bakkerij.BestellingInUitvoering;
            // Assert
            Assert.That(bestelling, Is.Not.Null);
            Assert.That(bestelling!.Klant.Naam, Is.EqualTo("Hans Vandenbogaerde"));
            Assert.That(bestelling.BroodType, Is.EqualTo(Bakkerij.BruinBrood));
            Assert.That(bestelling.Aantal, Is.EqualTo(3));
        }


    }
}