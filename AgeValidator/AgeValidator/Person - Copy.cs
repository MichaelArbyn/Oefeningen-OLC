using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAgeValidator
{
    public class Persoon
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        //remove dependency with AgeValidator by using interface
        private IAgeValidator ageValidator;

        // default constructor using the age validator
        public Persoon(string firstName, string lastName, int age)
            : this(firstName, lastName, age, new AgeValidator())
        {

        }

        //set Agevalidator in constructor
        public Persoon(string firstName, string lastName, int age, IAgeValidator validator)
        {
            ageValidator = validator;
            if (!ageValidator.IsValidAge(age))
            {
                throw new Exception("age invalid");
            }
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }
}
