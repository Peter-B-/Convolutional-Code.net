using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Convolutional.Logic.Extensions;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests.Decoding
{
    public class ContinuousInput
    {
        [Fact]
        public void HamingDistance()
        {
            var input = "11 10 00 01 01 11 00"
                .ParseBools()
                .Select(b => b?1.0: 0.0);

            var  expected = "1 0 1 1 0 0 0";

            var decoder = new Viterbi<double>(CodeConfig.Size3_7_5.EnumerateTransitions(), HammingDistance.Calculate, ScoreMethod.Minimize);
            var res = decoder.Solve(input);

            res.ShouldBe(expected.ParseBools());
        }

    }
}
