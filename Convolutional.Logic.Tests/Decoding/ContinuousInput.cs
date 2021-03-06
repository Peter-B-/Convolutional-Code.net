﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Convolutional.Logic.Extensions;
using Convolutional.Logic.Scores;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests.Decoding
{
    public class ContinuousInput
    {
        [Theory]
        [MemberData(nameof(GetCases))]
        public void TestExample(Func<IEnumerable<bool>, IEnumerable<double>, double> calcScore, ScoreMethod scoreMethod, double falseValue = 0.0)
        {
            var input = "11 10 00 01 01 11 00"
                .ParseBools()
                .Select(b => b?1.0: falseValue);

            var  expected = "1 0 1 1 0 0 0";

            var config = CodeConfig.Size3_7_5;
            var decoder = new Viterbi<double>(config.EnumerateTransitions(), calcScore, new ViterbiConfig(){InitialState = State.Zero(config.NoOfStateRegisters), ScoreMethod =scoreMethod});
            var res = decoder.Solve(input);

            res.Message.ShouldBe(expected.ParseBools());
        }


        public static IEnumerable<object[]> GetCases()
        {
            yield return new object[]{ (Func<IEnumerable<bool>, IEnumerable<double>, double>)HammingDistance.Calculate,  ScoreMethod.Minimize};
            yield return new object[]{ (Func<IEnumerable<bool>, IEnumerable<double>, double>)new LogisticFunction().CalculateScore,  ScoreMethod.Maximize};
            yield return new object[]{ (Func<IEnumerable<bool>, IEnumerable<double>, double>)SymmetricScore.MinusOneToOne.CalculateScore,  ScoreMethod.Maximize, -1.0};
        }
    }
}
