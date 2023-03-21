using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHashTable
{
    internal class GetPrimeNumber
    {
        private int _current;
        readonly int[] _primes = { 11, 29, 61, 127, 257, 523, 1087,
            2213, 4519, 9619, 19717, 40009, 62851, 75431, 90523,
            108631, 130363, 156437,  187751, 225307, 270371, 324449,
            389357, 467237, 560689, 672827, 807403, 968897,
            1162687, 1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287,
            4999559, 5999471, 7199369};

        public int Next()
        {
            if (_current < _primes.Length)
            {
                var value = _primes[_current];
                _current++;
                return value;
            }
            _current++;
            return (_current - _primes.Length) * _primes[_primes.Length - 1];
        }
        public int GetMin()
        {
            _current = 0;
            return _primes[_current];
        }
    }
}
