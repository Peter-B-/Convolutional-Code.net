using System;
using System.Collections.Generic;
using Convolutional.Logic.Extensions;
using System.Linq;

namespace Convolutional.Logic
{
    public class CodeConfig
    {
        // Code configurations for rate 1/2 codes found on page http://www.eccpage.com/ under
        // "14. Viterbi decoding" in http://www.eccpage.com/viterbi-3.0.1.tar
        public static CodeConfig Size3_7_5 = Generate(3, 0x7, 0x5);
        public static CodeConfig Size4_f_b = Generate(4, 0xf, 0xb);
        public static CodeConfig Size5_17_19 = Generate(5, 0x17, 0x19);
        public static CodeConfig Size5_13_1b = Generate(5, 0x13, 0x1b);
        public static CodeConfig Size6_2f_35 = Generate(6, 0x2f, 0x35);
        public static CodeConfig Size7_6d_4f = Generate(7, 0x6d, 0x4f);
        public static CodeConfig Size8_9f_5e = Generate(8, 0x9f, 0xe5);
        public static CodeConfig Size9_1af_11d = Generate(9, 0x1af, 0x11d);
        public static CodeConfig Size15_45dd_69e3 = Generate(15, 0x45dd, 0x69e3);

        public CodeConfig(IEnumerable<bool> generatorTop, IEnumerable<bool> generatorBottom)
        {
            GeneratorBottom = generatorBottom.ToArray();
            GeneratorTop = generatorTop.ToArray();

            if (GeneratorBottom.Length != GeneratorTop.Length)
                throw new ArgumentException("Generator polynomials must have the same number of elements.");
        }

        public bool[] GeneratorBottom { get; }
        public bool[] GeneratorTop { get; }

        public int NoOfRegisters => GeneratorBottom.Length;
        public int NoOfStateRegisters => GeneratorBottom.Length - 1;

        public static CodeConfig Generate(int noOfRegisters, int polyTop, int polyBottom)
        {
            return new CodeConfig(polyTop.GetBools(noOfRegisters), polyBottom.GetBools(noOfRegisters));
        }

        public override string ToString()
        {
            return "Top:    " + GeneratorTop.Format() + ", Bottom: " + GeneratorBottom.Format();
        }
    }
}