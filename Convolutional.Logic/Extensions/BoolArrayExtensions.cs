using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic.Extensions
{
    public static class BoolArrayExtensions
    {
        public static string Format(this IEnumerable<bool> bools, char trueChar = '1', char falseChar = '0')
        {
            return string.Concat(bools.Select(b => b ? trueChar : falseChar));
        }

        public static IEnumerable<bool> ParseBools(this string input)
        {
            foreach (var c in input)
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

        public static IEnumerable<bool> GetBools(this int number, int boolCount = 32)
        {
            var ba = new BitArray(new[] {number});
            return ba.Cast<bool>().Take(boolCount);
        }

        public static int GetInt(this IReadOnlyList<bool> bools)
        {
            if (bools.Count > 32) throw new InvalidOperationException("bools must not have more than 32 elements");

            var ba = new BitArray(bools.ToArray());
            var target = new int[1];
            ba.CopyTo(target, 0);
            return target[0];
        }
    }
}