using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic
{
    public class TrellisState
    {
        public TrellisState(int generation, IReadOnlyList<TrellisStateInfo> stateInfos)
        {
            Generation = generation;
            StateInfos = stateInfos;
        }

        public int Generation { get; }
        public IReadOnlyList<TrellisStateInfo> StateInfos { get; }

        public override string ToString()
        {
            return string.Join(
                Environment.NewLine,
                StateInfos.Select(s => s.ToString())
                    .Prepend($"Gen {Generation}:")
            );
        }
    }
}