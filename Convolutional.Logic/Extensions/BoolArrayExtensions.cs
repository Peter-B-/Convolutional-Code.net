using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Convolutional.Logic.Extensions
{
    public static class BoolArrayExtensions
    {
        public static string Format(this IEnumerable<bool> bools,char trueChar = '1', char falseChar = '0')
        {
            return string.Concat(bools.Select(b => b ? trueChar : falseChar));
        }

        public static IEnumerable<bool> ParseBools(this string input)
        {
            foreach (var c in input)
            {
                switch (c)
                {
                    case '0':
                        yield return false;
                        break;
                    case '1':
                        yield return true;
                        break;
                    default:
                        throw new ArgumentException($"Invalid input character '{c}'", nameof(input));
                }
            }
        }

        public static IEnumerable<bool> GetBools(this int number, int boolCount = 32)
        {
            var bv = new BitArray(new []{number});
            return bv.Cast<bool>().Take(boolCount);
        }

    }
}