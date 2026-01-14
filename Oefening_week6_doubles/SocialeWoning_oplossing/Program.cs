namespace SocialeWoning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Voorbeeld mogelijk gebruik van de klasses.
            Kandidaat kandidaat = new(
                leeftijd: 30,
                inkomen: 25000,
                huisvesting: new Dictionary<string, bool>
                {
                    { "eigen woning", true },
                    { "woning onbewoonbaar", true },
                    { "woning in vruchtgebruik", false }
                }
            );
            Toewijzer toewijzer = new Toewijzer();
            kandidaat.IsHuisvesting("eigen woning"); // is true
            if (toewijzer.Toewijzen(kandidaat))
            {
                Console.WriteLine("Done.");

            }
            else
            {
                Console.WriteLine("Kandidaat komt niet in aanmerking voor een sociale woning.");
            }
        }
    }
}
