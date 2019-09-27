using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic.Extensions
{
    static class RandomExtensions
    {
        public static IReadOnlyList<int> Indices(this Random rand, int noOfIndices)
        {
            var indices = new int[noOfIndices];
            for (var i = 0; i < indices.Length; i++)
                indices[i] = i;

            var n = indices.Length;
            while (n > 1)
            {
                var k = rand.Next(n--);
                (indices[n], indices[k]) = (indices[k], indices[n]);
            }

            return indices;
        }
    }
}