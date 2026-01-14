using BakkerijLimited.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakkerijLimited.Domain
{
    // REQ0001 Klant kan een bestelling voor een aantal broden aanmaken   
    // REQ0004 – Bakkerij kan nagaan of klantenkaart met bepaald nummer bestaat en opzoeken (en bijhouden)
    // REQ0006 – Bakkerij kan een nieuwe klantenkaart aanmaken
    // De Bakkerij houdt de gegevens van een lopende bestelling bij REQ0008, REQ0009, REQ0010, REQ0012
    // De Bakkerij fungeert als doorgeefluik naar objecten van andere klassen - REQ00XX
    public class Bakkerij
    {
        // REQ0001 - Soorten Brood die beschikbaar zijn
        public static Brood WitBrood = new Brood("Wit brood", 3.50m);
        public static Brood BruinBrood = new Brood("Bruin brood", 3.50m);

        // REQ0001 - Klant die de bestelling aanmaakt
        public Klant? Klant { get; set; }

        // REQ0008, REQ0009 – De Bakkerij houdt de bestelling in uitvoering bij.
        public Bestelling? BestellingInUitvoering { get; private set; }

        private KlantenkaartenRepo _klantenkaartenRepo;

        // REQ0008, REQ0010 – De Bakkerij houdt de geselecteerde klantenkaart bij.
        public Klantenkaart? GeselecteerdeKlantenkaart { get; private set; }

        // REQ0012 - De Bakkerij houdt de berekende korting bij
        public decimal BerekendeKorting { get; private set; }

        private Kassa _kassa;

        // Constructor, noodzakelijke initialisaties - tot nu toe
        public Bakkerij()
        {
            // REQ0001
            BestellingInUitvoering = null;
            // REQ0002
            _klantenkaartenRepo = new KlantenkaartenRepo();
            GeselecteerdeKlantenkaart = null;
            BerekendeKorting = 0.0m;
            _kassa = new Kassa();
        }

        // REQ0001 Klant kan een bestelling voor een aantal broden aanmaken
        // De aangemaakte bestelling wordt bijgehouden als BestellingInUitvoering
        // Input: 
        //      - klant die bestelt
        //      - broodType dat gewenst is (wit of bruin) als Brood (instantie)
        //      - aantal broden dat gewenst is als int
        // Output: 
        //      - bestelling die aangemaakt is als instantie van Bestelling
        public void MaakBestelling(Klant klant, Brood broodType, int aantal)
        {
            BestellingInUitvoering = new Bestelling(klant, broodType, aantal);
        }

        // REQ0006 – De Bakkerij kan een nieuwe klantenkaart aanmaken via de repo.
        // De aangemaakte klantenkaart wordt bijgehouden als GeselecteerdeKlantenkaart
        // Vereist: property `Klant` is ingesteld vóór aanroepen.
        // Input: -
        // Output: 
        //      - klantenkaart voor de aangemelde Klant van type Klantenkaart
        public Klantenkaart MaakKlantenkaart()
        {
            if (Klant is null)
            {
                throw new InvalidOperationException("Er is geen Klant ingesteld op de bakkerij.");
            }

            GeselecteerdeKlantenkaart = _klantenkaartenRepo.AddKlantenkaart(Klant);
            return GeselecteerdeKlantenkaart;
        }

        // REQ0004 – Zoekt een klantenkaart op nummer via de repo.
        // Input:
        //      - klantenkaartnummer van type int
        // Output: 
        //      - Klantenkaart object indien gevonden en anders null
        public Klantenkaart? ZoekEnSelecteerKlantenkaart(int klantenkaartNummer)
        {
            var gevonden = _klantenkaartenRepo.ZoekOpNummer(klantenkaartNummer);
            GeselecteerdeKlantenkaart = gevonden;
            return gevonden;
        }

        // REQ0009 – De bakkerij kan het bestelde aantal broden aan de voorgelegde klantenkaart toevoegen
        // De bakkerij kent de bestelling en de klantenkaart waar ze aan moet worden toegevoegd
        // Input: -
        // Output: -
        public void VoegBestellingToeAanKlantenkaart()
        {
            GeselecteerdeKlantenkaart?.VoegBestellingToeAanKlantenkaart(BestellingInUitvoering!.Aantal);
        }

        // REQ0011 – De bakkerij kan checken of er meer dan 10 (of een andere limiet) broden op de klantenkaart staan
        // De bakkerij kent de klantenkaart die gecheckt moet worden
        // Input: -
        // Output: 
        //      - true als er genoeg broden op de kaart staan om korting toe te kennen
        //        false als dat niet het geval is
        public bool CheckKortingMogelijk()
        {
            return GeselecteerdeKlantenkaart?.CheckKortingMogelijk() ?? false;
        }

        // REQ0012 – De bakkerij kan de korting berekenen (11de brood gratis na aankoop van 10 broden) 
        // en de klantenkaart bijwerken door de aangekochte broden correct te verminderen
        // De bakkerij kent de klantenkaart die berekend moet worden
        // Input: -
        // Output: 
        //      - de korting die toegekend wordt bij deze bestelling, van type decimal
        public decimal BerekenKortingEnWerkBij()
        {
            if (GeselecteerdeKlantenkaart == null || BestellingInUitvoering == null)
            {
                BerekendeKorting = 0.0m;
                return BerekendeKorting;
            }

            BerekendeKorting = GeselecteerdeKlantenkaart.BerekenKortingEnWerkBij(BestellingInUitvoering);
            return BerekendeKorting;
        }

        // REQ0016 en REQ0017 - De bakkerij kan de prijs met of zonder korting berekenen
        // De bakkerij kent Bestelling en BerekendeKorting
        // Input: -
        // Output:
        //      - de berekende prijs van type decimal
        public decimal RekenAf()
        {
            return _kassa.RekenAf(BestellingInUitvoering!, BerekendeKorting);
        }

        // Controleert of de geselecteerde klantenkaart overeenkomt met de huidige klant
        public string DoeKaartKlantMatch()
        {
            if (GeselecteerdeKlantenkaart == null)
                return "Geen klantenkaart geselecteerd";

            return GeselecteerdeKlantenkaart.GeefMatchMelding(Klant);
        }

    }


}
