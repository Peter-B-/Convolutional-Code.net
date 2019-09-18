using System;
using System.Linq;
using Convolutional.Logic;
using Convolutional.Logic.Extensions;

namespace ConvolutionalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = CodeConfig.Default4;
            var encoder = new Encoder(config, true);


            var transitions = config.EnumerateTransitions();
            foreach (var trans in transitions)
                Console.WriteLine(trans);


            var input = 0xFFFF.GetBools(19);
            //var input = rand.Next(0, 0x10000).GetBools(16);
            var res = encoder.Encode(input);

            Console.WriteLine($"Input: {input.Count()} bits");
            Console.WriteLine(input.Format());
            Console.WriteLine();
            Console.WriteLine($"Output: {res.Count} bits");
            Console.WriteLine(res.Format());


            PrintSequence(encoder);
        }

        private static void PrintSequence(Encoder encoder)
        {
            for (int i = 0; i < 0x0100; i++)
            {
                var input = i.GetBools(19);
                //var input = rand.Next(0, 0x10000).GetBools(16);
                var res = encoder.Encode(input);
                Console.WriteLine(res.Format('1', '_'));
            }
        }
    }
}
