using System.Collections.Generic;
using System.Linq;
using Convolutional.Logic.Extensions;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests
{
    public class StateEnumeratorTests
    {
        [Theory]
        [MemberData(nameof(CountData))]
        public void CheckCount(CodeConfig config, int expected)
        {
            var states = config.EnumerateStates();

            states.Count().ShouldBe(expected);
        }

        [Theory]
        [MemberData(nameof(StateData))]
        public void CheckStates(CodeConfig config, IEnumerable<string> expected)
        {
            var states = config.EnumerateStates();

            states
                .Select(s => s.State.Values.Format())
                .ShouldBe(expected, true);
        }

        public static IEnumerable<object[]> CountData()
        {
            yield return new object[] {CodeConfig.Size3_7_5, 4};
            yield return new object[] {CodeConfig.Size4_15_11, 8};
        }

        public static IEnumerable<object[]> StateData()
        {
            yield return new object[] {CodeConfig.Size3_7_5, new[] {"00", "01", "10", "11"}};
            yield return new object[]
                {CodeConfig.Size4_15_11, new[] {"000", "001", "010", "011", "100", "101", "110", "111"}};
        }
    }
}