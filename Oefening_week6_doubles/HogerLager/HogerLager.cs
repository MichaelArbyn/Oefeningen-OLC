using System;

namespace HogerLager
{
    /// <summary>
    /// Het spel Hoger Lager waarbij een speler een getal moet raden tussen 0 en 15.
    /// </summary>
    public class HogerLager
    {
        private readonly uint number;

        /// <summary>
        /// Maakt een nieuw spel waarvan het nummer bepaald is met een random number generator.
        /// </summary>
        public HogerLager():this(new TrueRandom())
        {

        }
        public HogerLager(IRandomFunctie random)
        {
            number = (uint) random.Next(16);
        }

        /// <summary>
        /// Evauleert een raadpoging
        /// </summary>
        /// <param name="guess"> de raadpoging</param>
        /// <returns> resultaat van de evaluatie: correct, hoger of lager </returns>
        public RaadResultaat RaadEens(int guess)
        {
            return RaadResultaat.Hoger;
        }
    }
    /// <summary>
    /// Resultaat van een raadpoging.
    /// </summary>
    public enum RaadResultaat
    {
        Correct,
        Hoger,
        Lager
    }
}
