namespace SocialeWoning
{
    /// <summary>
    /// Controleert of een kandidaat voldoet aan de voorwaarden voor het verkrijgen van een sociale woning.
    /// </summary>
    public class SocialeWoningVoorwaardenChecker
    {
        private const int MIN_LEEFTIJD = 18;
        private const int MAX_INKOMEN = 30000;

        /// <summary>
        /// Bepaalt of een kandidaat voldoet aan de gestelde voorwaarden.
        /// Req01
        /// </summary>
        /// <param name="kandidaat">De kandidaat die beoordeeld wordt.</param>
        /// <returns>True als de kandidaat voldoet aan de voorwaarden, anders false.</returns>
        public bool VoldoetAanVoorwaarden(Kandidaat kandidaat)
        {

            {
                if (kandidaat.Leeftijd < MIN_LEEFTIJD)
                    return false;

                if (kandidaat.Inkomen >= MAX_INKOMEN)
                    return false;

                if (kandidaat.IsHuisvesting("woning in vruchtgebruik"))
                    return false;

                bool heefEigenWoning = kandidaat.IsHuisvesting("eigen woning");
                bool isOnbewoonbaar = kandidaat.IsHuisvesting("woning onbewoonbaar");

                if (heefEigenWoning || isOnbewoonbaar)
                    return false;

                // Kandidaat voldoet aan alle voorwaarden
                return true;
            }

        }
    }
}