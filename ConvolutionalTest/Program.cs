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
            var encoder = new Encoder();

            var rand = new Random();

            var input = 0x0000.GetBools(19);
            //var input = rand.Next(0, 0x10000).GetBools(16);
            var res = encoder.Encode(input);

            Console.WriteLine($"Input: {input.Count()} bits");
            Console.WriteLine(input.Format());
            Console.WriteLine();
            Console.WriteLine($"Output: {res.Count} bits");
            Console.WriteLine(res.Format());
        }
    }
}
