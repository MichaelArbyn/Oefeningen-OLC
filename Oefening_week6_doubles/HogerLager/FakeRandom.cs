using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HogerLager
{
    internal class FakeRandom : IRandomFunctie
    {
        private readonly int _value;

        public FakeRandom(int value)
        {
            _value = value;
        }

        public int Next(int maxValue)
        {
            return _value;
        }
    }
}
