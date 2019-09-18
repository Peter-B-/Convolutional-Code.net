using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic
{
    public class Encoder
    {
        private readonly CodeConfig config;
        private readonly bool terminateCode;

        public Encoder(CodeConfig config, bool terminateCode)
        {
            this.config = config;
            this.terminateCode = terminateCode;
        }

        public IReadOnlyList<bool> Encode(IEnumerable<bool> input)
        {
            var inputList = input as IReadOnlyList<bool> ?? input.ToList();

            var result = new List<bool>(GetResultLength(inputList));

            var stateReg = StateRegister.CreateInitial(config.NoOfRegisters);

            foreach (var i in inputList)
            {
                stateReg = stateReg.Shift(i);

                result.AddRange(stateReg.GetOutput(config));
            }


            if (terminateCode)
                while (stateReg.State.Values.Any(s => s))
                {
                    stateReg = stateReg.Shift(false);

                    result.AddRange(stateReg.GetOutput(config));
                }


            return result;
        }

        private int GetResultLength(IReadOnlyList<bool> inputList) =>
            inputList.Count + (terminateCode ? config.NoOfRegisters : 0) * 2;
    }
}