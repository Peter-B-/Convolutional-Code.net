using System;
using System.Collections.Generic;
using Convolutional.Logic.Extensions;
using System.Linq;

namespace Convolutional.Logic.Interleaver
{
    public class BlockInterleaver
    {
        private readonly int blockSize;

        public BlockInterleaver(int blockSize, int seed = 0) : this(new Random(seed).Indices(blockSize))
        {
        }


        public BlockInterleaver(IEnumerable<int> indicesInterleave)
        {
            IndicesInterleave = indicesInterleave.ToArray();
            blockSize = IndicesInterleave.Count;

            if (IndicesInterleave.Any(i => i >= blockSize) || IndicesInterleave.NonUnique().Any())
                throw new ArgumentException("The interleave indices must be an array containing n distinct values from 0 to n-1.", nameof(indicesInterleave));

            var reverse = new int[blockSize];
            for (var i = 0; i < reverse.Length; i++)
                reverse[IndicesInterleave[i]] = i;

            IndicesDeinterleave = reverse;
        }

        public IReadOnlyList<int> IndicesDeinterleave { get; }
        public IReadOnlyList<int> IndicesInterleave { get; }

        public IReadOnlyList<T> Deinterleave<T>(IReadOnlyList<T> input)
        {
            var res = new T[blockSize];
            for (var i = 0; i < res.Length; i++)
                res[i] = input[IndicesDeinterleave[i]];

            return res;
        }

        public IReadOnlyList<T> Interleave<T>(IReadOnlyList<T> input)
        {
            AssertInputSize(input);

            var res = new T[blockSize];
            for (var i = 0; i < res.Length; i++)
                res[i] = input[IndicesInterleave[i]];

            return res;
        }

        private void AssertInputSize<T>(IReadOnlyList<T> input)
        {
            if (input.Count != blockSize)
                throw new InvalidOperationException($"This Interleaver can only handle inputs with {blockSize} elements. Got input with {input.Count} elements.");
        }
    }
}