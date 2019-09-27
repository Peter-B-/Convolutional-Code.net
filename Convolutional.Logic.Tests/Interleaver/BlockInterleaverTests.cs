using System;
using System.Collections.Generic;
using System.Linq;
using Convolutional.Logic.Interleaver;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests.Interleaver
{
    public class BlockInterleaverTests
    {
        [Fact]
        public void SimpleExample()
        {
            var interleaver = new BlockInterleaver(new []{1, 0, 2});

            interleaver.Interleave(new []{10, 20, 30})
                .ShouldBe(new []{20, 10, 30});
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(10)]
        public void InterleaveAndDeinterleave(int elementCount)
        {
            var input = Enumerable.Range(0, elementCount)
                .ToArray();

            var interleaver = new BlockInterleaver(input.Length, 0);

            var interleaved = interleaver.Interleave(input);
            var result = interleaver.Deinterleave(interleaved);

            input.ShouldBe(result);
        }
    }
}