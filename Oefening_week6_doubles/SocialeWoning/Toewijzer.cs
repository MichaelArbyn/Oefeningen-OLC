using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeWoning
{
    /// <summary>
    /// Wijst sociale woningen toe aan kandidaten op basis van bepaalde criteria.
    /// </summary>
    public class Toewijzer
    {
        // Aantal beschikbare sociale woningen
        // Dit is slechts een simplificatie voor dit voorbeeld. Beeld je veeeeeeeel variabelen en logica in om dit te beheren.
        private int aantalBeschikbareWoningen = 1;

        /// <summary>
        /// Entiteit die de voorwaarden voor sociale woningtoewijzing controleert.
        /// </summary>
        public SocialeWoningVoorwaardenChecker VoowaardenChecker { get; set; } = new();

        public Toewijzer() { }


        /// <summary>
        /// Bepaalt of een kandidaat in aanmerking komt voor toewijzing van een sociale woning.
        /// </summary>
        /// <param name="kandidaat"> te beoordelen kandidaat</param>
        /// <returns> true als in aanmerking </returns>
        private bool KomtInAanmerking(Kandidaat kandidaat)
        {
            return VoowaardenChecker.VoldoetAanVoorwaarden(kandidaat);
        }

        /// <summary>
        /// Kent een sociale woning toe aan een kandidaat indien deze in aanmerking komt en een woning beschikbaar is.
        /// req02
        /// </summary>
        /// <param name="kandidaat"></param>
        /// <returns>true als een toewijzing is gebeurd</returns>
        public bool Toewijzen(Kandidaat kandidaat)
        {
            // TODO implementeer NADAT je alle unit testen hebt gemaakt.
            if(KomtInAanmerking(kandidaat)) return false;
            return false; // Woning niet toegewezen
        }

        // en nog veel meer functies...


    }
}
