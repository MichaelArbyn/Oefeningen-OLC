namespace SocialeWoning
{
    /// <summary>
    /// Interface voor het controleren van de voorwaarden voor een sociale woning.
    /// Req01: bepaalt of een kandidaat voldoet aan alle voorwaarden.
    /// </summary>
    public interface ISocialeWoningVoorwaardenChecker
    {
        bool VoldoetAanVoorwaarden(Kandidaat kandidaat);
    }
}
