using BakkerijLimited.Domain;
using BakkerijLimited.Models;
using System;

namespace TestBakkerij
{
     public class KassaTests
    {
        private Kassa _kassa;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _kassa = new Kassa();
        }

        // REQ0016 – De bakkerij kan de prijs met korting berekenen
        // REQ0017 – De bakkerij kan de prijs zonder korting berekenen
        [TestCase(3, 0.00, 10.50)]
        [TestCase(3, 3.50, 7.00)]
        [TestCase(3, 10.50, 0.00)]
        public void RekenAf_ReturnsCorrectePrijs
            (int aantalBroden, decimal korting, double verwachtePrijs)
        {
            // Arrange
            Bestelling bestelling = new Bestelling(new Klant("Sara"), Bakkerij.BruinBrood, aantalBroden);
            // Act
            decimal berekendePrijs = _kassa.RekenAf(bestelling, korting);
            // Assert
            Assert.That(berekendePrijs, Is.EqualTo((decimal)verwachtePrijs));
        }
    }
}
