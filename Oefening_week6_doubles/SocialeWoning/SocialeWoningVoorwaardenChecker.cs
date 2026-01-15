namespace SocialeWoning
{
    /// <summary>
    /// Controleert of een kandidaat voldoet aan de voorwaarden voor het verkrijgen van een sociale woning.
    /// Req01: leeftijd, inkomen en huisvestingsfeiten bepalen of de kandidaat in aanmerking komt.
    /// </summary>
    public class SocialeWoningVoorwaardenChecker : ISocialeWoningVoorwaardenChecker
    {
        private const int MIN_LEEFTIJD = 18;
        private const int MAX_INKOMEN = 30000;

        /// <summary>
        /// Req01:
        /// - Leeftijd moet minstens 18 zijn.
        /// - Inkomen moet lager zijn dan 30.000.
        /// - Woning in vruchtgebruik is nooit toegestaan.
        /// - Eigen woning is enkel toegestaan als deze onbewoonbaar is.
        /// </summary>
        public bool VoldoetAanVoorwaarden(Kandidaat kandidaat)
        {
            // Leeftijdsvoorwaarde
            if (kandidaat.Leeftijd < MIN_LEEFTIJD)
                return false;

            // Inkomensvoorwaarde
            if (kandidaat.Inkomen >= MAX_INKOMEN)
                return false;

            // Vruchtgebruik is altijd verboden
            if (kandidaat.IsHuisvesting("woning in vruchtgebruik"))
                return false;

            // Eigen woning mag enkel als ze onbewoonbaar is
            bool heeftEigenWoning = kandidaat.IsHuisvesting("eigen woning");
            bool isOnbewoonbaar = kandidaat.IsHuisvesting("woning onbewoonbaar");

            if (heeftEigenWoning && !isOnbewoonbaar)
                return false;

            // Alle voorwaarden voldaan
            return true;
        }
    }
}