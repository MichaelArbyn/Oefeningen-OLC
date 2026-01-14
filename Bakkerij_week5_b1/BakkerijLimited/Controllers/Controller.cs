using BakkerijLimited.Domain;
using BakkerijLimited.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace BakkerijLimited.Controllers
{
    /// <summary>
    /// Controller die de Use Cases orkestreert.
    /// Geen enkele vorm van I/O gebeurt hier — volledig testbaar.
    ///
    /// Deze controller ondersteunt:
    /// - UC-001  Bestelling maken
    /// - UC-002  Klantenkaart opzoeken
    /// - UC-003  Klantenkaart toevoegen
    /// - UC-004  Bestelling afrekenen
    /// - UC-005  Bestelling aan klantenkaart koppelen
    /// - UC-006  Kortingsmogelijkheid checken
    /// - UC-007  Korting berekenen en kaart bijwerken
    ///
    /// Elke methode bevat REQ-verwijzingen.
    /// </summary>
    public class BakkerijController
    {
        private readonly Bakkerij _bakkerij;

        public BakkerijController()
        {
            _bakkerij = new Bakkerij();
        }

        // ======================================================================
        // UC-001 — BESTELLING MAKEN
        // ======================================================================

        /// <summary>
        /// UC-001 — Een klant start een nieuwe bestelling.
        /// REQ0001 — Klant kan een bestelling voor een aantal broden aanmaken.
        /// </summary>
        public void StartNieuweBestelling(string klantNaam)
        {
            if (string.IsNullOrWhiteSpace(klantNaam))
                throw new ArgumentException("Klantnaam ongeldig.", nameof(klantNaam));

            _bakkerij.Klant = new Klant(klantNaam);
        }

        /// <summary>
        /// UC-001 — Het systeem toont de beschikbare broden.
        /// REQ0001 — Het systeem weet welke broden besteld kunnen worden.
        /// </summary>
        public IReadOnlyList<Brood> HaalBeschikbareBrodenOp()
        {
            return new List<Brood>
            {
                Bakkerij.WitBrood,
                Bakkerij.BruinBrood
            };
        }

        /// <summary>
        /// UC-001 — Het systeem maakt een bestelling aan voor de klant.
        /// REQ0001 — Bestelling wordt aangemaakt op basis van broodtype en aantal.
        /// </summary>
        public bool VoegBroodToeAanBestelling(string broodSoort, int aantal)
        {
            if (_bakkerij.Klant == null)
                return false;

            if (aantal <= 0)
                return false;

            Brood? brood = broodSoort?.Trim().ToLower() switch
            {
                "wit brood" => Bakkerij.WitBrood,
                "bruin brood" => Bakkerij.BruinBrood,
                _ => null
            };

            if (brood == null)
                return false;

            _bakkerij.MaakBestelling(_bakkerij.Klant, brood, aantal);
            return true;
        }

        // ======================================================================
        // UC-002 — KLANTENKAART OPZOEKEN
        // ======================================================================

        /// <summary>
        /// UC-002 — Klant geeft een nummer van een bestaande klantenkaart.
        /// REQ0003 — Nummer kan door de klant worden ingevoerd.
        /// REQ0004 — Bakkerij kan nagaan of kaart bestaat.
        /// REQ0002 — Het systeem kent de beschikbare kaartnummers.
        /// </summary>
        public Klantenkaart? ZoekKlantenkaart(int kaartnummer)
        {
            return _bakkerij.ZoekEnSelecteerKlantenkaart(kaartnummer);
        }

        /// <summary>
        /// UC-002 — Alternatief pad: klant gebruikt geen klantenkaart.
        /// REQ0007 — Klant kan aangeven geen kaart te willen gebruiken.
        /// </summary>
        public void GebruikGeenKlantenkaart()
        {
            _bakkerij.ZoekEnSelecteerKlantenkaart(-1);
        }

        // ======================================================================
        // UC-003 — NIEUWE KLANTENKAART AANMAKEN
        // ======================================================================

        /// <summary>
        /// UC-003 — Klant vraagt een nieuwe klantenkaart aan.
        /// REQ0005 — Kaartnummer "0" betekent: klant wenst een nieuwe kaart.
        /// REQ0006 — Bakkerij kan een nieuwe klantenkaart aanmaken.
        /// </summary>
        public Klantenkaart MaakNieuweKlantenkaart()
        {
            if (_bakkerij.Klant == null)
                throw new InvalidOperationException("Geen klant ingesteld.");

            return _bakkerij.MaakKlantenkaart();
        }

        // ======================================================================
        // UC-005 — BESTELLING AAN KLANTENKAART TOEVOEGEN
        // ======================================================================

        /// <summary>
        /// UC-005 — Bestelling wordt gekoppeld aan de geselecteerde klantenkaart.
        /// REQ0008 — Bakkerij kent het aantal broden en de kaart.
        /// REQ0009 — Bestelling wordt toegevoegd aan de kaart.
        /// </summary>
        public bool VoegBestellingToeAanKlantenkaart()
        {
            if (_bakkerij.GeselecteerdeKlantenkaart == null ||
                _bakkerij.BestellingInUitvoering == null)
                return false;

            _bakkerij.VoegBestellingToeAanKlantenkaart();
            return true;
        }

        // ======================================================================
        // UC-006 — KORTINGSMOGELIJKHEID CHECKEN
        // ======================================================================

        /// <summary>
        /// UC-006 — Het systeem controleert of korting mogelijk is.
        /// REQ0011 — Check of er voldoende broden op de kaart staan.
        /// </summary>
        public bool CheckKortingMogelijk()
        {
            return _bakkerij.CheckKortingMogelijk();
        }

        // ======================================================================
        // UC-007 — KORTING BEREKENEN & KAART BIJWERKEN
        // ======================================================================

        /// <summary>
        /// UC-007 — Korting berekenen en kaart aanpassen.
        /// REQ0012 — 11de brood gratis na aankoop van 10 broden.
        /// REQ0010 — Bakkerij kent de kaart waarvoor korting toegepast wordt.
        /// </summary>
        public decimal BerekenKorting()
        {
            if (_bakkerij.GeselecteerdeKlantenkaart == null)
                return 0m;

            return _bakkerij.BerekenKortingEnWerkBij();
        }

        // ======================================================================
        // UC-004 — BESTELLING AFREKENEN
        // ======================================================================

        /// <summary>
        /// UC-004 — Bereken te betalen bedrag.
        /// REQ0016 — prijs met korting
        /// REQ0017 — prijs zonder korting
        /// REQ0013 — systeem kent de toe te passen korting
        /// REQ0014 — systeem kent aantal broden
        /// REQ0015 — systeem kent prijs per brood
        /// </summary>
        public decimal RekenAf()
        {
            if (_bakkerij.BestellingInUitvoering == null)
                return 0m;

            return _bakkerij.RekenAf();
        }

        public string doeKaartKlantMatch()
        {
            return _bakkerij.DoeKaartKlantMatch();
        }

        public Bestelling? HaalHuidigeBestellingOp()
        {
            return _bakkerij.BestellingInUitvoering;
        }
    }
}
