using BakkerijLimited.Controllers;
using BakkerijLimited.Domain;
using BakkerijLimited.Models;

internal class Program
{
    private static BakkerijController controller = new BakkerijController();

    public static void Main()
    {
        bool programmaDraait = true;

        while (programmaDraait)
        {
            Console.Clear();
            Console.WriteLine("=== Welkom in Bakkerij Limited ===");
            Console.WriteLine();

            // ------------------------------
            // UC-001 — Bestelling maken
            // ------------------------------
            string? klantNaam = VraagKlantNaam();
            if (klantNaam is null)
                break;

            controller.StartNieuweBestelling(klantNaam);

            // ------------------------------
            // UC-002 / UC-003 — Klantenkaart
            // ------------------------------
            VerwerkKlantenkaartKeuze();

            // ------------------------------
            // UC-001 — Bestelling toevoegen
            // ------------------------------
            BestelBroden();

            // ------------------------------
            // UC-005 / UC-006 / UC-007 / UC-004
            // Afrekenflow
            // ------------------------------
            Afrekenen();

            Console.Write("\nVolgende klant? (j/n): ");
            programmaDraait = Console.ReadLine()?.Trim().ToLower() == "j";
        }

        Console.WriteLine("\nProgramma afgesloten. Tot ziens!");
    }

    // =====================================================================
    // KLANT NAAM VRAGEN
    // =====================================================================

    private static string? VraagKlantNaam()
    {
        Console.Write("Naam van de klant (leeg = programma stoppen): ");
        string? naam = Console.ReadLine()?.Trim();

        return string.IsNullOrWhiteSpace(naam) ? null : naam;
    }

    // =====================================================================
    // KLANTENKAART FLOW (UC-002 en UC-003)
    // =====================================================================

    private static void VerwerkKlantenkaartKeuze()
    {
        Console.WriteLine("\n--- Klantenkaart ---");
        Console.WriteLine("Geef het nummer van uw klantenkaart in.");
        Console.WriteLine(" 0 = nieuwe klantenkaart aanvragen");
        Console.WriteLine(" 9999 = geen klantenkaart gebruiken");

        while (true)
        {
            Console.Write("Uw keuze: ");
            string? invoer = Console.ReadLine()?.Trim();

            if (invoer == "9999")
            {
                controller.GebruikGeenKlantenkaart();
                Console.WriteLine("U gebruikt geen klantenkaart.");
                return;
            }

            if (invoer == "0")
            {
                var kaart = controller.MaakNieuweKlantenkaart();
                Console.WriteLine($"Nieuwe klantenkaart aangemaakt: #{kaart.KlantenkaartNummer}");
                return;
            }

            if (int.TryParse(invoer, out int kaartnummer))
            {
                var kaart = controller.ZoekKlantenkaart(kaartnummer);

                if (kaart == null)
                {
                    Console.WriteLine("Geen klantenkaart gevonden met dit nummer.");
                }
                else
                {
                    // Toon of de gevonden kaart overeenkomt met de huidige klant
                    Console.WriteLine(controller.doeKaartKlantMatch());
                    return;
                }
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Probeer opnieuw.");
            }
        }
    }

    // =====================================================================
    // UC-001 — BESTELLING MAKEN
    // =====================================================================

    private static void BestelBroden()
    {
        Console.WriteLine("\n--- Bestelling plaatsen ---");

        bool verderBestellen = true;

        while (verderBestellen)
        {
            // Brood kiezen
            Brood? brood = KiesBrood();

            if (brood == null)
            {
                Console.WriteLine("Ongeldige keuze.");
                continue;
            }

            // Aantal ingeven
            int aantal = VraagAantal();
            if (aantal <= 0)
            {
                Console.WriteLine("Ongeldig aantal.");
                continue;
            }

            // Controller aanroepen
            bool gelukt = controller.VoegBroodToeAanBestelling(brood.BroodSoort, aantal);

            if (!gelukt)
            {
                Console.WriteLine("Bestelling kon niet worden aangemaakt.");
            }
            else
            {
                Console.WriteLine($"-> {aantal} × {brood.BroodSoort} toegevoegd aan bestelling.");
            }

            // Nog iets bestellen? - doen we later
            // Console.Write("Nog iets bestellen? (j/n): ");
            // verderBestellen = Console.ReadLine()?.Trim().ToLower() == "j";

            verderBestellen = false; // Voor nu 1 broodsoort per bestelling
        }
    }

    private static Brood? KiesBrood()
    {
        var broden = controller.HaalBeschikbareBrodenOp();

        Console.WriteLine("\nBeschikbare broden:");
        for (int i = 0; i < broden.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {broden[i].BroodSoort}  ({broden[i].Prijs} EUR)");
        }

        Console.Write("Uw keuze: ");
        string? invoer = Console.ReadLine()?.Trim();

        if (int.TryParse(invoer, out int keuze) &&
            keuze >= 1 &&
            keuze <= broden.Count)
        {
            return broden[keuze - 1];
        }

        return null;
    }

    private static int VraagAantal()
    {
        Console.Write("Aantal: ");
        return int.TryParse(Console.ReadLine(), out int aantal) && aantal > 0
            ? aantal
            : -1;
    }

    // =====================================================================
    // UC-004, UC-005, UC-006, UC-007 — AFREKENEN
    // =====================================================================

    private static void Afrekenen()
    {
        Console.WriteLine("\n--- Afrekenen ---");

        // UC-005 — bestelling aan kaart koppelen
        controller.VoegBestellingToeAanKlantenkaart();

        // UC-006 — kortingsmogelijkheid checken
        bool kortingMogelijk = controller.CheckKortingMogelijk();
        if (kortingMogelijk)
        {
            Console.WriteLine("Korting is mogelijk op basis van uw klantenkaart.");
        }

        // UC-007 — korting berekenen
        decimal korting = controller.BerekenKorting();
        if (korting > 0)
        {
            Console.WriteLine($"Toegekende korting: {korting} EUR");
        }

        // UC-004 — totaalprijs berekenen
        decimal teBetalen = controller.RekenAf();
        Console.WriteLine($"Te betalen bedrag: {teBetalen} EUR");
    }
}
