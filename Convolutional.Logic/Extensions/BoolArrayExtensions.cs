using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic.Extensions
{
    public static class BoolArrayExtensions
    {
        public static string Format(this IEnumerable<bool> bools, char trueChar = '1', char falseChar = '0', int? groupSize = null)
        {
            if (groupSize.HasValue)
                return
                    bools
                        .Buffer(groupSize.Value)
                        .Select(group => group.Select(b => b ? trueChar : falseChar).Concat())
                        .JoinStrings(" ");
            else
                return
                    bools.Select(b => b ? trueChar : falseChar).Concat();
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
                    case ' ':
                        break;
                    default:
                        throw new ArgumentException($"Invalid input character '{c}'", nameof(input));
                }
        }

        /// <summary>
        /// Gets a given no of booleans that represent the binary value of number.
        /// The LSB is returned first. 10 => 0b1010 => [true, false, true, false]
        /// </summary>
        /// <param name="number"></param>
        /// <param name="boolCount"></param>
        /// <returns></returns>
        public static IEnumerable<bool> GetBools(this int number, int boolCount = 32)
        {
            var ba = new BitArray(new[] {number});
            return ba.Cast<bool>().Take(boolCount).Reverse();
        }

        public static int GetInt(this IReadOnlyList<bool> bools)
        {
            if (bools.Count > 32) throw new InvalidOperationException("bools must not have more than 32 elements");

            var ba = new BitArray(bools.Reverse().ToArray());
            var target = new int[1];
            ba.CopyTo(target, 0);
            return target[0];
        }
    }

    public static class StringExtensions
    {
        public static string JoinStrings(this IEnumerable<string> parts, string sep)
        {
            return string.Join(sep, parts);
        }

        public static string Concat(this IEnumerable<char> chars)
        {
            return string.Concat(chars);
        }
    }
}