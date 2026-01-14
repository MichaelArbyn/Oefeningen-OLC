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
        public IKandidaatChecker voowaardenChecker { get; set; } = new SocialeWoningVoorwaardenChecker();

        public Toewijzer() { }


        /// <summary>
        /// Bepaalt of een kandidaat in aanmerking komt voor toewijzing van een sociale woning.
        /// </summary>
        /// <param name="kandidaat"> te beoordelen kandidaat</param>
        /// <returns> true als in aanmerking </returns>
        private bool KomtInAanmerking(Kandidaat kandidaat)
        {
            return voowaardenChecker.VoldoetAanVoorwaarden(kandidaat);
        }

        /// <summary>
        /// Kent een sociale woning toe aan een kandidaat indien deze in aanmerking komt en een woning beschikbaar is.
        /// req02
        /// </summary>
        /// <param name="kandidaat"></param>
        /// <returns>true als een toewijzing is gebeurd</returns>
        public bool Toewijzen(Kandidaat kandidaat)
        {
            if (aantalBeschikbareWoningen > 0 && KomtInAanmerking(kandidaat))
            {
                aantalBeschikbareWoningen--;
                return true; // Woning toegewezen
            }
            return false;
        }

        // en nog veel meer functies...


    }
}
