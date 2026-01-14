namespace SocialeWoning
{
    /// <summary>
    /// Controleert of een kandidaat voldoet aan de voorwaarden voor het verkrijgen van een sociale woning.
    /// </summary>
    public class SocialeWoningVoorwaardenChecker : IKandidaatChecker
    {
        private const int MIN_LEEFTIJD = 18;

        /// <summary>
        /// Bepaalt of een kandidaat voldoet aan de gestelde voorwaarden.
        /// Req01
        /// </summary>
        /// <param name="kandidaat">De kandidaat die beoordeeld wordt.</param>
        /// <returns>True als de kandidaat voldoet aan de voorwaarden, anders false.</returns>
        public virtual bool VoldoetAanVoorwaarden(Kandidaat kandidaat)
        {
            // Controleer minimum leeftijd
            if (kandidaat.Leeftijd < MIN_LEEFTIJD)
            {
                return false;
            }
            // Controleer maximum inkomen
            if (kandidaat.Inkomen >= 30000)
            {
                return false;
            }

            // Controleer huisvestingsvoorwaarden
            if (kandidaat.IsHuisvesting("eigen woning") && !kandidaat.IsHuisvesting("woning onbewoonbaar"))
            {
                return false;
            }
            if (kandidaat.IsHuisvesting("woning in vruchtgebruik"))
            {
                return false;
            }

            // Kandidaat voldoet aan alle voorwaarden
            return true;
        }
    }
}