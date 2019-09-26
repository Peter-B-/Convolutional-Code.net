using System;
using System.Linq;
using Convolutional.Logic;
using Convolutional.Logic.Extensions;
using Convolutional.Logic.Scores;

namespace ConvolutionalTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //DemoDiscreteHamming();
            DemoContinuousSymmetricScore();
        }

        private static void DemoDiscreteHamming()
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

        private static void DemoContinuousSymmetricScore()
        {
            var config = CodeConfig.Size7_6d_4f;

            var input = 15003.GetBools(14)
                .Concat(Enumerable.Repeat(false, 6));

            var encoder = new Encoder(config, terminateCode: false);
            var encoded = encoder.Encode(input);
            var transmitted =
                encoded
                    .Select(e => e ? 255.0 : 0);

            var decoder = new Viterbi<double>(
                config.EnumerateTransitions(),
                SymmetricScore.Range_0_255.CalculateScore,
                new ViterbiConfig()
                {
                    InitialState = State.Zero(config.NoOfStateRegisters),
                    ScoreMethod = ScoreMethod.Maximize,
                    TerminationState = State.Zero(config.NoOfStateRegisters)
                });
            var restored = decoder.Solve(transmitted);


            Console.WriteLine("Configuration: " + config);
            Console.WriteLine();
            Console.WriteLine("Input:    " + input.Format());
            Console.WriteLine("Encoded:  " + encoded.Format());
            Console.WriteLine("Restored: " + restored.Message.Format());
            Console.Write($"Score:    {restored.BestEndScore:F2}");
            //if (restored.TerminationStateScore.HasValue)
            Console.Write($" / {restored.TerminationStateScore:F2}");
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}