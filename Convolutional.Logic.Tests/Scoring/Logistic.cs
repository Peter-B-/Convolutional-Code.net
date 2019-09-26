using System;
using System.Linq;
using Convolutional.Logic.Scores;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests.Scoring
{
    public class Logistic
    {
        [Theory]
        [InlineData(new[] { true }, new[] { 1.0 }, 1)]
        [InlineData(new[] { true }, new[] { 0.0 }, 0)]
        [InlineData(new[] { false }, new[] { 1.0 }, 0)]
        [InlineData(new[] { false }, new[] { 0.0 }, 1)]
        [InlineData(new[] { true }, new[] { 0.5 }, 0.5)]
        [InlineData(new[] { false }, new[] { 0.5 }, 0.5)]


        [InlineData(new[] { true, false, true }, new[] { 1.0, 0.0, 1.0 }, 3.0)]
        [InlineData(new[] { true, false, true }, new[] { 0.0, 1.0, 0.0 }, 0.0)]
        [InlineData(new[] { true, false, true }, new[] { 0.5, 0.5, 0.5 }, 1.5)]

        public void Check(bool[] a, double[] b, double expected)
        {
            var res = new LogisticFunction().CalculateScore(a, b);
            res.ShouldBe(expected, 0.01);
        }
    }
}