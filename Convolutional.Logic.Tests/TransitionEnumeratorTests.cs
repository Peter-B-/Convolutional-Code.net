using System.Collections.Generic;
using System.Linq;
using Convolutional.Logic.Extensions;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests
{
    public class TransitionEnumeratorTests
    {
        private static void AssertTransition(IReadOnlyList<Transition> transitions, string initial, bool input,
            string expectedState, string expectedOutput)
        {
            var transition = transitions.Single(t => t.InitialState.Equals(State.Parse(initial)) && t.Input == input);
            transition.Output.Format().ShouldBe(expectedOutput);
            transition.NewState.ShouldBe(State.Parse(expectedState));
        }

        [Fact]
        public void TestDefault3()
        {
            var config = CodeConfig.Default3;

            var transitions = config.EnumerateTransitions() as IReadOnlyList<Transition>;

            AssertTransition(transitions, "00", false, "00", "00");
            AssertTransition(transitions, "00", true, "10", "11");
            AssertTransition(transitions, "01", false, "00", "11");
            AssertTransition(transitions, "01", true, "10", "00");
            AssertTransition(transitions, "10", false, "01", "10");
            AssertTransition(transitions, "10", true, "11", "01");
            AssertTransition(transitions, "11", false, "01", "01");
            AssertTransition(transitions, "11", true, "11", "10");
        }
    }
}