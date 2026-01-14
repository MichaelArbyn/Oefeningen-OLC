using System;
using System.Collections.Generic;

using BakkerijLimited.Models;

namespace BakkerijLimited.Domain
{
    public class KlantenkaartenRepo
    {
        private readonly List<Klantenkaart> _klantenkaarten;
        private int _nextKlantenkaartNummer = 1;

        public KlantenkaartenRepo()
        {
            _klantenkaarten = new List<Klantenkaart>();
        }

        // REQ0006 – Een nieuwe klantenkaart aanmaken en toevoegen aan de repo
        // De repo maakt zelf de Klantenkaart aan en kent een autoincrement nummer toe.
        public Klantenkaart AddKlantenkaart(Klant klant)
        {
            if (klant is null)
                throw new ArgumentNullException(nameof(klant));

            var nummer = _nextKlantenkaartNummer++;
            var klantenkaart = new Klantenkaart(nummer, klant);
            _klantenkaarten.Add(klantenkaart);
            return klantenkaart;
        }

        // REQ0004 – Een klantenkaart opzoeken op nummer
        public Klantenkaart? ZoekOpNummer(int klantenkaartNummer)
        {
            if (klantenkaartNummer <= 0)
                return null;

            foreach (var kaart in _klantenkaarten)
            {
                if (kaart.KlantenkaartNummer == klantenkaartNummer)
                    return kaart;
            }

            return null;
        }
    }
}
