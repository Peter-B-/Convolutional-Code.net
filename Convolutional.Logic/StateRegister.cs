using System;
using System.Collections.Generic;
using System.Linq;
using Convolutional.Logic.Extensions;

namespace Convolutional.Logic
{
    public class StateRegister
    {
        private StateRegister(IEnumerable<bool> values)
        {
            if (values is IReadOnlyList<bool> asList)
                Registers = asList;
            else
                Registers = values.ToList();
        }

        public IReadOnlyList<bool> Registers { get; }
        public IEnumerable<bool> States => Registers.Take(Registers.Count - 1);

        public static StateRegister CreateInitial(int noOfRegisters)
        {
            return new StateRegister(Enumerable.Repeat(false, noOfRegisters));
        }

        public  bool Mod2Add(IEnumerable<bool> mask)
        {
            var sum =
                Registers.Zip(mask, (a, b) => a && b)
                    .Count(b => b);

            return sum % 2 == 1;
        }

        public StateRegister Shift(bool input)
        {
            return new StateRegister(
                Registers
                    .AsEnumerable()
                    .Prepend(input)
                    .Take(Registers.Count)
            );
        }

        public override string ToString() => Registers.Format();
    }
}