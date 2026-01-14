using NSubstitute;
using NUnit.Framework;

namespace HogerLager.Tests
{

    [TestFixture]
    public class HogerLagerTests_NSub
    {
        [Test]
        public void RaadEens_WithLowerNumber_ReturnsHigher()
        {
            //Arrange
            IRandomFunctie random = Substitute.For<IRandomFunctie>();
            random.Next(Arg.Any<int>()).Returns(8); // een nieuwe random Functie die altijd 8 teruggeeft
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