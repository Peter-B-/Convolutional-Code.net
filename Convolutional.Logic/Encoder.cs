using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic
{
    public class Encoder
    {
        public bool[] GeneratorBottom = {true, false, false, true};
        public bool[] GeneratorTop = {true, true, true, true};

        public IReadOnlyList<bool> Encode(IEnumerable<bool> input)
        {
            return DoEncode(input).ToList();
        }

        private IEnumerable<bool> DoEncode(IEnumerable<bool> input)
        {
            var stateReg = StateRegister.CreateInitial(GeneratorTop.Length);

            foreach (var i in input)
            {
                stateReg = stateReg.Shift(i);

                yield return stateReg.Mod2Add(GeneratorTop);
                yield return stateReg.Mod2Add(GeneratorBottom);
            }


            while (stateReg.States.Any(s => s))
            {
                stateReg = stateReg.Shift(false);

                yield return stateReg.Mod2Add(GeneratorTop);
                yield return stateReg.Mod2Add(GeneratorBottom);
            }

        }
    }
}