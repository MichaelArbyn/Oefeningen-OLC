using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HogerLager
{
    /// <summary>
    /// Berekent randomgetallen 
    /// </summary>
    public interface IRandomFunctie
    {
        /// <summary>
        /// Geeft een random getal tussen 0 (inclusief) en maxValue (exclusief).
        /// </summary>
        /// <param name="maxValue"> return moet kleiner zijn dan deze waarde</param>
        /// <returns>een random getal</returns>
        public int Next(int maxValue);
    }
}
