using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic
{
    public static class TransitionEnumerator
    {
        public static IEnumerable<Transition> EnumerateTransitions(this CodeConfig config)
        {
            return
                config.EnumerateStates()
                    .SelectMany(state =>
                        new[] {false, true}
                            .Select(input =>
                                CalculateTransition(state, input, config))
                    )
                    .ToList();
        }

        private static Transition CalculateTransition(StateRegister state, bool input, CodeConfig config)
        {
            var newState = state.Shift(input);
            var output = newState.GetOutput(config).ToList();

            return new Transition(state.State.Values, input, output, newState.State.Values);
        }
    }
}