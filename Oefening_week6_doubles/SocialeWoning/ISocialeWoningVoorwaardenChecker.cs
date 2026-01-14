using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeWoning
{
    internal interface ISocialeWoningVoorwaardenChecker
    {
        bool VoldoetAanVoorwaarden(Kandidaat kandidaat);
    }
}
