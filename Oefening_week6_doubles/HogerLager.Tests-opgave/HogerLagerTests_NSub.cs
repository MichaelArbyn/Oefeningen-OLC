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
            var random = Substitute.For<IRandomFunctie>();
            random.Next(16).Returns(8);

            var hogerLager = new HogerLager(random);
            int guess = 3;

            //Act
            RaadResultaat result = hogerLager.RaadEens(guess);

            //Assert
            Assert.That(result, Is.EqualTo(RaadResultaat.Hoger));
        }

        [Test]
        public void RaadEens_WithHigherNumber_ReturnsLower()
        {
            //Arrange
            var random = Substitute.For<IRandomFunctie>();
            random.Next(16).Returns(8);   

            var hogerLager = new HogerLager(random);
            int guess = 12;

            //Act
            RaadResultaat result = hogerLager.RaadEens(guess);

            //Assert
            Assert.That(result, Is.EqualTo(RaadResultaat.Lager));
        }

        [Test]
        public void RaadEens_WithCorrectNumber_ReturnsCorrect()
        {
            //Arrange
            var random = Substitute.For<IRandomFunctie>();
            random.Next(16).Returns(8);   

            var hogerLager = new HogerLager(random);
            int guess = 8;

            //Act
            RaadResultaat result = hogerLager.RaadEens(guess);

            //Assert
            Assert.That(result, Is.EqualTo(RaadResultaat.Correct));
        }
    }
}