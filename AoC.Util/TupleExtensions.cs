﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Util
{
    public static class TupleExtensions
    {
        public static bool IsIn(this (int, int) point, int min, int max)
        {
            return point.Item1 >= min && point.Item1 <= max && point.Item2 >= min && point.Item2 <= max;
        }
    }
}
