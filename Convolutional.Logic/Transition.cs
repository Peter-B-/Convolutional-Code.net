using System.Collections.Generic;
using System.Linq;
using Convolutional.Logic.Extensions;

namespace Convolutional.Logic
{
    public class Transition
    {
        public Transition(IEnumerable<bool> initialState, bool input, IEnumerable<bool> output,
            IEnumerable<bool> newState)
        {
            InitialState = new State(initialState);
            Input = input;
            Output = output.ToList();
            NewState = new State(newState);
        }

        public State InitialState { get; }
        public IReadOnlyList<bool> Output { get; }
        public bool Input { get; }
        public State NewState { get; }

        public override string ToString()
        {
            return $"{InitialState}|{(Input ? '1' : '0')} => {NewState}|{Output.Format()}";
        }
    }
}