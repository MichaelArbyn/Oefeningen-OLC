using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakkerijLimited.Models
{
    // Houdt het aantal bestelde broden bij en kan op basis hiervan checken of korting mogelijk is
    // en desgevallend de korting berekenen en het aantal bestelde broden bijwerken
    // REQ0004 – Bakkerij kan nagaan of klantenkaart met bepaald nummer bestaat en opzoeken
    // REQ0006 – Bakkerij kan een nieuwe klantenkaart aanmaken
    // REQ0009 – De bakkerij kan het bestelde aantal broden aan de voorgelegde klantenkaart toevoegen
    // REQ0011 – De bakkerij kan checken of er meer dan 10 (of een andere limiet) broden op de klantenkaart staan
    // REQ0012 – De bakkerij kan de korting berekenen (11de brood gratis na aankoop van 10 broden)
    // en de klantenkaart bijwerken door de aangekochte broden correct te verminderen
    public class Klantenkaart
    {
        // REQ0004 - klantenkaart opzoeken volgens nummer
        // REQ0006 - klantenkaart krijgt bij creatie een uniek nummer
        public int KlantenkaartNummer { get; private set; }

        // REQ0006 - klantenkaart wordt gemaakt voor een bepaalde klant
        public Klant Klant { get; private set; }

        // REQ0009 - Klantenkaart houdt bij hoeveel broden er gekocht werden
        // REQ0011 - Als het aantal broden een bepaalde limiet overschreid, is er een gratis brood
        // REQ0012 - Bij toekennen van een gratis brood, wordt het anatal broden bijgewerkt
        public int AantalBroden { get; set; }

        // REQ0011 en REQ0012 - Aantal broden dat recht geeft op één gratis brood.
        private const int AANTAL_BRODEN_VOOR_GRATIS_BROOD = 10;

        // REQ0006 – Bakkerij kan een nieuwe klantenkaart aanmaken
        // Input: 
        //      - klantenkaartNummer - uniek id, beheerd door repo, van type int
        //      - klant - de klant waarvoor de klantenkaart wordt gemaakt, type Klant
        // Output:
        //      - een object van type Klantenkaart
        public Klantenkaart(int klantenkaartNummer, Klant klant)
        {
            KlantenkaartNummer = klantenkaartNummer;
            Klant = klant;
            AantalBroden = 0;
        }

        // REQ0018 - Controleert of de opgegeven klant overeenkomt met de klant op de kaart
        public bool KomtOvereenMet(Klant? andereKlant)
        {
            if (andereKlant is null) return false;
            return string.Equals(Klant?.Naam, andereKlant.Naam, StringComparison.Ordinal);
        }

        // REQ0018 - Geeft een tekstuele melding terug over de match tussen kaart en klant
        public string GeefMatchMelding(Klant? huidigeKlant)
        {
            if (KomtOvereenMet(huidigeKlant))
            {
                return "Kaart met kaartnummer " + KlantenkaartNummer + " komt overeen met klant " + Klant.Naam;
            }
            return "MISMATCH! - Kaart met kaartnummer " + KlantenkaartNummer + " komt overeen met klant " + Klant.Naam;
        }

        // REQ0009 – De bakkerij kan het bestelde aantal broden aan de voorgelegde klantenkaart toevoegen
        // Input: 
        //      - aantal broden dat toegevoegd moet worden, type int
        public void VoegBestellingToeAanKlantenkaart(int aantal) 
        {
            AantalBroden += aantal;
        }

        // REQ0011 – De bakkerij kan checken of er meer dan 10 (of een andere limiet) broden op de klantenkaart staan
        // Input: -
        // Output:
        //      - true als er genoeg broden zijn om korting te kunnen toepassen, anders false
        public bool CheckKortingMogelijk()
        {
           return AantalBroden >= AANTAL_BRODEN_VOOR_GRATIS_BROOD;
        }

        // REQ0012 – De bakkerij kan de korting berekenen (11de brood gratis na aankoop van 10 broden)
        // Input: 
        //      - bestelling waarop korting toegepast zal worden, type bestelling
        //        de korting kan nooit groter zijn dan de waarde van de bestelling
        // Output:
        //      - het bedrag van de toe te kennen korting in Euro, type decimal
        //        indien geen korting mogelijk, wordt 0.0m geretourneerd
        public decimal BerekenKortingEnWerkBij(Bestelling bestelling)
        {
           if (CheckKortingMogelijk())
           {
                int aantalGratisBroden = AantalBroden / AANTAL_BRODEN_VOOR_GRATIS_BROOD;
                aantalGratisBroden = Math.Min(aantalGratisBroden, bestelling.Aantal);
                decimal korting = aantalGratisBroden * bestelling.BroodType.Prijs;
                AantalBroden -= aantalGratisBroden * AANTAL_BRODEN_VOOR_GRATIS_BROOD;
                return korting;
           }
           return 0.0m;
        }



    }
}
