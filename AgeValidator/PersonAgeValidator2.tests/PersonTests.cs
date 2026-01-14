using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAgeValidator2.Tests
{
    // Een stub klasse die altijd true of false teruggeeft, afhankelijk van wat we willen testen
    class AgeValidatorStub : IAgeValidator
    {
        private readonly bool _isValid;
        public AgeValidatorStub(bool isValid)   // set the return value via constructor
        {
            _isValid = isValid;
        }
        public bool IsValidAge(int age)   // always return the value set in the constructor
        {
            return _isValid;
        }
    }

    [TestFixture]
    public class PersonTests
    {
        [SetUp]
        public void Setup()
        {
            IAgeValidator altijdTrue = new AgeValidatorStub(true);
            IAgeValidator altijdFalse = new AgeValidatorStub(false);
        }

        // Test person created (valid age)
        [Test]
        public void Ctor_ValidAges_PersonCreated()
        {
            //Arrange
            string firstname = "John";
            string lastname = "Doe";

            //Act
            Person person = new Person(firstname, lastname, 20,new AgeValidatorStub(true));

            //Assert
            Assert.That(person.FirstName, Is.EqualTo(firstname));
            Assert.That(person.LastName, Is.EqualTo(lastname));
            Assert.That(person.Age, Is.EqualTo(20));
            //OF
            Assert.DoesNotThrow(() => new Person(firstname, lastname, 20, new AgeValidatorStub(true)));
        }

        // Test person not created (invalid age/ exception throwed)
        [Test]
        public void Ctor_InvalidAges_ThrowsException()
        {
            //Arrange
            string firstname = "John";
            string lastname = "Doe";

            //Act is de new person in de assert

            //Assert
            Assert.Throws<Exception>(() => new Person(firstname, lastname, -20, new AgeValidatorStub(false)));
        }
    }
}
