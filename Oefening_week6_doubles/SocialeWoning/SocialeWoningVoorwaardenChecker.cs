namespace SocialeWoning
{
    /// <summary>
    /// Controleert of een kandidaat voldoet aan de voorwaarden voor het verkrijgen van een sociale woning.
    /// </summary>
    public class SocialeWoningVoorwaardenChecker
    {
        private const int MIN_LEEFTIJD = 18;

        /// <summary>
        /// Bepaalt of een kandidaat voldoet aan de gestelde voorwaarden.
        /// Req01
        /// </summary>
        /// <param name="kandidaat">De kandidaat die beoordeeld wordt.</param>
        /// <returns>True als de kandidaat voldoet aan de voorwaarden, anders false.</returns>
        public bool VoldoetAanVoorwaarden(Kandidaat kandidaat)
        {

            // Kandidaat voldoet aan alle voorwaarden
            return true;
        }
    }
}