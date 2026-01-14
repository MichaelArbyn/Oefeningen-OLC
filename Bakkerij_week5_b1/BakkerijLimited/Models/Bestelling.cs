using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakkerijLimited.Models;

// REQ0001 Klant kan een bestelling voor een aantal broden aanmaken
public class Bestelling
{
    public Klant Klant { get; set; }
    public Brood BroodType { get; set; }
    public int Aantal { get; set; }

    // REQ0008 – De Bakkerij houdt de bestelling in uitvoering bij, inclusief het aantal broden.
    public Bestelling(Klant klant, Brood broodType, int aantal)
    {
        Klant = klant;
        BroodType = broodType;
        Aantal = aantal;
    }

}
