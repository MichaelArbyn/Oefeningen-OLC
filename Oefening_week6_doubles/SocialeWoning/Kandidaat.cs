namespace SocialeWoning
{
    /// <summary>
    /// Domeinklasse die een kandidaat voorstelt.
    /// Bevat leeftijd, inkomen en huisvestingsfeiten.
    /// </summary>
    public class Kandidaat
    {
        public int Leeftijd { get; }
        public int Inkomen { get; }
        private readonly Dictionary<string, bool> huisvesting;

        public Kandidaat(int leeftijd, int inkomen, Dictionary<string, bool> huisvesting)
        {
            Leeftijd = leeftijd;
            Inkomen = inkomen;
            this.huisvesting = huisvesting ?? new Dictionary<string, bool>();
        }

        /// <summary>
        /// Controleert of een bepaald huisvestingsfeit aanwezig is.
        /// </summary>
        public bool IsHuisvesting(string sleutel)
        {
            return huisvesting.TryGetValue(sleutel, out bool waarde) && waarde;
        }
    }
}