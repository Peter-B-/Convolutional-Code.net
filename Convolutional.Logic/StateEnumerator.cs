using System.Collections.Generic;
using Convolutional.Logic.Extensions;

namespace Convolutional.Logic
{
    public static class StateEnumerator
    {
        public static IEnumerable<StateRegister> EnumerateStates(this CodeConfig config)
        {
            for (var i = 0; i < (1<< config.NoOfStateRegisters); i++)
                yield return StateRegister.Create(i.GetBools(config.NoOfRegisters));
        }
    }
}