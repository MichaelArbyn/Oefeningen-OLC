namespace SocialeWoning
{
    /// <summary>
    /// Stelt 1 persoon voor.   
    /// </summary>
    /// <param name="leeftijd">in aantal jaren</param>
    /// <param name="inkomen">van laatste kalenderjaar in euro</param>
    /// <param name="huisvesting"> Waarden zoals bepaald door Vlaams woningregister</param> 
    public class Kandidaat(int leeftijd, int inkomen, Dictionary<string, bool> huisvesting)
    {
        public int Leeftijd { get; } = leeftijd;
        public int Inkomen { get; } = inkomen;
        private Dictionary<string, bool> Huisvesting { get; } = new Dictionary<string, bool>(huisvesting);

        /// <summary>
        /// Controleert of de kandidaat een bepaalde huisvestingssituatie heeft.
        /// </summary>
        /// <param name="type"> de situatie te ondervragen</param>
        /// <returns>resultaat van de bevraging</returns>
        public bool IsHuisvesting(string type)
        {
            return Huisvesting.ContainsKey(type) && Huisvesting[type];
        }

    }

}