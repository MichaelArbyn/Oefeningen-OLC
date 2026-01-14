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
        public HogerLager() : this(new TrueRandom())
        {
        }

        /// <summary>
        /// Constructor die een IRandomFunctie gebruikt zodat we in testen het getal kunnen vastleggen.
        /// </summary>
        public HogerLager(IRandomFunctie random)
        {
            number = (uint)random.Next(16);
        }

        /// <summary>
        /// Evalueert een raadpoging.
        /// </summary>
        /// <param name="guess">De raadpoging.</param>
        /// <returns>Correct, Hoger of Lager.</returns>
        public RaadResultaat RaadEens(int guess)
        {
            if (guess == number)
                return RaadResultaat.Correct;

            if (guess < number)
                return RaadResultaat.Hoger;

            return RaadResultaat.Lager;
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