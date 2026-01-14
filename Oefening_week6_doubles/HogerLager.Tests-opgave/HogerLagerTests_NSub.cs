using NSubstitute;
using NUnit.Framework;

namespace HogerLager.Tests_opgave
{

    [TestFixture]
    public class HogerLagerTests_NSub
    {
        [Test]
        public void RaadEens_WithLowerNumber_ReturnsHigher()
        {
            //Arrange
            // We kunnen niet testen met de echte random generator omdat we dan niet weten welk getal er gekozen wordt.
            // Daarom is IRandomFunctie random = new TrueRandom(); hier niet bruikbaar.
            IRandomFunctie random = ???;// een nieuwe random Functie die altijd 8 teruggeeft
            HogerLager higherLower = new(random);
            int guess = 3;

            //Act
            RaadResultaat result = higherLower.RaadEens(guess);

            //Assert
            Assert.That(result, Is.EqualTo(RaadResultaat.Hoger));

        }

        [Test]
        public void RaadEens_WithHigherNumber_ReturnsLower()
        {

        }

        [Test]
        public void RaadEens_WithCorrectNumber_ReturnsUhThatOtherThing()
        {

        }
    }
}