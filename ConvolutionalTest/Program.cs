using System;
using System.Linq;
using Convolutional.Logic;
using Convolutional.Logic.Extensions;

namespace ConvolutionalTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = CodeConfig.Size7_6d_4f;

            var input = 15003.GetBools(14)
                .Concat(Enumerable.Repeat(false, 6));
            //var input = "11101010011011".ParseBools();

            var encoder = new Encoder(config, terminateCode: false);
            var output = encoder.Encode(input);

            var viterbi = Viterbi.CreateWithHammingDistance(config);
            var restored = viterbi.Solve(output);

            Console.WriteLine("Configuration: " + config);
            Console.WriteLine();
            Console.WriteLine("Input:    " + input.Format());
            Console.WriteLine("Encoded:  " + output.Format());
            Console.WriteLine("Restored: " + restored.Message.Format());
            Console.Write($"Score:    {restored.BestEndScore:F2}");
            //if (restored.TerminationStateScore.HasValue)
                Console.Write($" / {restored.TerminationStateScore:F2}");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}