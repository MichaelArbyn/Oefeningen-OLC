using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HogerLager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HogerLager hogerLager = new();
            RaadResultaat? result=null;

            do
            {
                Console.WriteLine("Raad het nummer");
                string? input = Console.ReadLine();

                int number;
                if (int.TryParse(input, out number))
                {

                    result = hogerLager.RaadEens(number);
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("Invalid entry");
                }

            } while ( result==null || result!= RaadResultaat.Correct);

            Console.WriteLine("Yay! You guessed it!");

            Console.ReadLine();
        }
    }
}
