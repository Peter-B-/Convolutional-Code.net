using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Convolutional.Logic.Extensions;

namespace Convolutional.Logic
{
    public struct State
    {
        public bool Equals(State other)
        {
            if (other.Values.Count != Values.Count) return false;
            for (var i = 0; i < Values.Count; i++)
                if (Values[i] != other.Values[i])
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is State other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Values.GetInt();
        }

        public State(IEnumerable<bool> values)
        {
            Debug.Assert(values != null, nameof(values) + " != null");
            Values = values as IReadOnlyList<bool> ?? values.ToList();
        }

        public IReadOnlyList<bool> Values { get; }

        public static State Parse(string input) => new State(input.ParseBools());
        public static State Zero(int noOfStates) => new State(Enumerable.Repeat(false, noOfStates));

        public override string ToString() => Values.Format();

        public static State Zero(State template) => Zero(template.Values.Count);
    }
}