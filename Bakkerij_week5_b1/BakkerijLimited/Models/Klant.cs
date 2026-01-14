using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakkerijLimited.Models
{
    public class Klant
    {
        public String Naam { get; set; }
        public Klant(string naam)
        {
            Naam = naam;
        }
    }
}
