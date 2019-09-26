using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Convolutional.Logic;
using Convolutional.Logic.Extensions;

namespace Benchmark
{
    [ClrJob(), CoreJob()]
    public class DecodeBenchmark
    {
        private IReadOnlyList<bool> encoded;
        private Viterbi<bool> viterbi;

        [GlobalSetup]
        public void Setup()
        {
            var config = CodeConfig.Size7_6d_4f;

            var input = 
                15003.GetBools(14)
                    .Concat(Enumerable.Repeat(false, 6));

            var encoder = new Encoder(config, terminateCode: true);
            encoded = encoder.Encode(input);

            viterbi = Viterbi.CreateWithHammingDistance(config);
        }

        [Benchmark]
        public void Decode()
        {
            var restored = viterbi.Solve(encoded);
        }
    }
}