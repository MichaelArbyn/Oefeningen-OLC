
using NUnit.Framework;

namespace HogerLager.Tests_opgave
{
    public class FakeRandom : IRandomFunctie
    {
        private readonly int _value;
        public FakeRandom(int value)
        {
            _value = value;
        }

        public int Next(int maxValue)
        {
            return _value;
        }

    }

    [TestFixture]
    public class HogerLagerTests
    {
        [Test]
        public void RaadEens_WithLowerNumber_ReturnsHigher()
        {
            //Arrange
            IRandomFunctie random = new FakeRandom(8);
            HogerLager hogerLager = new(random);
            int guess = 3;

            //Act
            RaadResultaat result = hogerLager.RaadEens(guess);

            //Assert
            Assert.That(result, Is.EqualTo(RaadResultaat.Hoger));

        }

        [Test]
        public void RaadEens_WithCorrectNumber_ReturnsCorrect()
        {
            //Arrange
            IRandomFunctie random = new FakeRandom(8);
            HogerLager hogerLager = new(random);
            int guess = 8;

            //act
            RaadResultaat result = hogerLager.RaadEens(guess);

            //Assert
            Assert.That(result, Is.EqualTo(RaadResultaat.Correct));
        }

        [Test]
        public void RaadEens_WithHigerNumber_ReturnsLower()
        {
            //Arrange
            IRandomFunctie random = new FakeRandom(8);
            HogerLager hogerLager = new(random);
            int guess = 12;

            //Act
            RaadResultaat result = hogerLager.RaadEens(guess);

            //Assert
            Assert.That(result, Is.EqualTo(RaadResultaat.Lager));

        }
    }
}