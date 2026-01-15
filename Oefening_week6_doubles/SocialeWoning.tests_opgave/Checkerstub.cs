using SocialeWoning;

namespace SocialeWoning.tests_opgave
{
    /// <summary>
    /// Test double voor REQ02.
    /// Geeft altijd een vooraf ingestelde waarde terug zodat REQ02
    /// getest wordt zonder REQ01 opnieuw uit te voeren.
    /// </summary>
    public class CheckerStub : ISocialeWoningVoorwaardenChecker
    {
        private readonly bool _result;

        public CheckerStub(bool result)
        {
            _result = result;
        }

        public bool VoldoetAanVoorwaarden(Kandidaat kandidaat)
        {
            return _result;
        }
    }
}