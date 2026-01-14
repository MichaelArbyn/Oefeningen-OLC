using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakkerijLimited.Models
{
    public class Brood
    {
        public String BroodSoort { get; set; }
        public Decimal Prijs { get; set; }

        public Brood(string broodSoort, decimal prijs)
        {
            BroodSoort = broodSoort;
            Prijs = prijs;
        }

    }
}
