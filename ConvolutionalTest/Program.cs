using System;
using Convolutional.Logic;
using Convolutional.Logic.Extensions;

namespace ConvolutionalTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = CodeConfig.Size3_7_5;

            var input = "10110".ParseBools();

            var encoder = new Encoder(config, terminateCode: false);
            var output = encoder.Encode(input);

            var viterbi = Viterbi.CreateWithHammingDistance(config);
            var restored = viterbi.Solve(output);

            Console.WriteLine("Configuration: " + config);
            Console.WriteLine();
            Console.WriteLine("Input:    " + input.Format());
            Console.WriteLine("Encoded:  " + output.Format());
            Console.WriteLine("Restored: " + restored.Format());
            Console.WriteLine();
        }
    }
}