namespace SocialeWoning
{
    /// <summary>
    /// Interface voor het controleren van voorwaarden
    /// </summary>
    public interface IKandidaatChecker
    {
        /// <summary>
        /// Bepaalt of een kandidaat voldoet aan de gestelde voorwaarden.
        /// </summary>
        /// <param name="kandidaat">De kandidaat die beoordeeld wordt.</param>
        /// <returns>True als de kandidaat voldoet aan de voorwaarden, anders false.</returns>
        bool VoldoetAanVoorwaarden(Kandidaat kandidaat);
    }
}
