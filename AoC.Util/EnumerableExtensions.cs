using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Util
{
    public static class EnumerableExtensions
    {
        public static int Multiply(this IEnumerable<int> values)
        {
            var result = 0;
            var enumerator = values.GetEnumerator();
            if(enumerator.MoveNext())
                result = enumerator.Current;
            while(enumerator.MoveNext())
                result *= enumerator.Current;
            enumerator.Dispose();
            return result;
        }

        public static long Multiply(this IEnumerable<long> values)
        {
            long result = 0;
            var enumerator = values.GetEnumerator();
            if(enumerator.MoveNext())
                result = enumerator.Current;
            while(enumerator.MoveNext())
                result *= enumerator.Current;
            enumerator.Dispose();
            return result;
        }
    }
}
