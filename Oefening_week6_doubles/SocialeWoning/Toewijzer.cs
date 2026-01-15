namespace SocialeWoning
{
    /// <summary>
    /// Req02: bepaalt of een woning kan worden toegewezen aan een kandidaat.
    /// Toewijzing gebeurt enkel als:
    /// - de kandidaat voldoet aan alle voorwaarden (REQ01)
    /// - er nog beschikbare woningen zijn
    /// </summary>
    public class Toewijzer
    {
        /// <summary>
        /// Interface‑gedreven afhankelijkheid zodat REQ02 getest kan worden met een test double.
        /// </summary>
        public ISocialeWoningVoorwaardenChecker VoowaardenChecker { get; set; }
            = new SocialeWoningVoorwaardenChecker();

        /// <summary>
        /// Aantal beschikbare woningen (startwaarde: 1).
        /// </summary>
        public int AantalBeschikbareWoningen { get; private set; } = 1;

        /// <summary>
        /// Req02:
        /// - Als kandidaat niet voldoet aan voorwaarden → false
        /// - Als geen woningen beschikbaar → false
        /// - Anders: woning toewijzen, voorraad verminderen → true
        /// </summary>
        public bool Toewijzen(Kandidaat kandidaat)
        {
            // Voorwaardenchecker beslist of kandidaat geldig is
            if (!VoowaardenChecker.VoldoetAanVoorwaarden(kandidaat))
                return false;

            // Geen woningen meer beschikbaar
            if (AantalBeschikbareWoningen <= 0)
                return false;

            // Woning toewijzen
            AantalBeschikbareWoningen--;
            return true;
        }
    }
}