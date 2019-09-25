using System.Collections.Generic;
using Convolutional.Logic.Scoring;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests.Scoring
{
    public class Hamming
    {
        [Theory]
        [InlineData(new []{true}, new  [] {true}, 0)]
        [InlineData(new []{true}, new  [] {false}, 1)]
        [InlineData(new []{true, false, true}, new  [] {false, true, true}, 2)]
        public void Check(bool[] a, bool[] b, int expected)
        {
            HammingDistance.Calculate(a, b).ShouldBe(expected);
        }
    }
}