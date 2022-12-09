using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Util
{
    public static class StringExtensions
    {

        public static string[] Lines(this string s)
        {
            return s.Split(Environment.NewLine);
        }

    }
}
