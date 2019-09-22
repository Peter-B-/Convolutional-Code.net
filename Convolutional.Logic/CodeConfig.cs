using System;
using System.Collections.Generic;
using System.Linq;
using Convolutional.Logic.Extensions;

namespace Convolutional.Logic
{
    public class CodeConfig
    {
        public static CodeConfig Default3 = new CodeConfig("111".ParseBools(), "101".ParseBools());
        public static CodeConfig Default4 = new CodeConfig("1111".ParseBools(), "1011".ParseBools());

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
    }
}