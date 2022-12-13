﻿using System;
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

        public static (T? i1, T? i2) FirstMultisearch<T>(this IEnumerable<T> values, Predicate<T> p1, Predicate<T> p2) where T : class
        {
            var i1 = default(T);
            var i2 = default(T);

            foreach (var item in values)
            {
                if (i1 == default(T) && p1(item))
                {
                    i1 = item;
                    if (i2 != default(T))
                        break;
                }
                if (i2 == default(T) && p2(item))
                {
                    i2 = item;
                    if (i1 != default(T))
                        break;
                }
            }

            return (i1, i2);
        }

        public static IEnumerable<LinkedListNode<T>> AsNodes<T>(this LinkedList<T> list, bool ignoreLast = false)
        {
            for (LinkedListNode<T> node = list.First; node != null; node = node.Next)
            {
                if(ignoreLast && node.Next == null)
                    yield break;
                yield return node;
            }
        }

        public static LinkedList<T> ToLinkedList<T>(this IDictionary<T, T> dict, T start) where T : class
        {
            var ll = new LinkedList<T>();
            ll.AddFirst(start);
            var current = dict[start];
            while (current != null)
            {
                ll.AddLast(current);
                current = dict.TryGetValue(current, out var newCurrent) ? newCurrent : null;
            }
            return ll;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> values, params T[] additionalValues)
        {
            foreach (var value in values.Concat(additionalValues.AsEnumerable()))
            {
                yield return value;
            }
        }

        public static IEnumerable<(int i, T value)> Indexed<T>(this IEnumerable<T> values)
        {
            int i = 0;
            var iter = values.GetEnumerator();
            while (iter.MoveNext())
            {
                yield return (i++, iter.Current);
            }
            iter.Dispose();
        }

    }
}
