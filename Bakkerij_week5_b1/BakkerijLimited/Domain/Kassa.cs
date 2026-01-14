using BakkerijLimited.Models;
using System;

namespace BakkerijLimited.Domain
{
    // Rekent de bestelling af 
    // al dan (REQ0016)
    // of niet (REQ0017) met korting
    public class Kassa
    {
        // REQ0016 – De bakkerij kan de prijs met korting berekenen
        // REQ0017 – De bakkerij kan de prijs zonder korting berekenen
        // Input:
        //      - bestelling die moet afgerekend worden, van type Bestelling
        //      - korting, die eventueel kan toegepast worden, eventueel is ze 0.0m, van type decimal
        // Output:
        //      - de berekende totaalprijs, na aftrek van eventuele korting
        public decimal RekenAf(Bestelling bestelling, decimal korting)
        {
            return bestelling.BroodType.Prijs * bestelling.Aantal - korting;
        }
    }
}
