using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests
{
    public class EqualsTest
    {
        [Theory]
        [MemberData(nameof(EqualStates))]
        public void IsEqual(State a, State b)
        {
            a.Equals(b).ShouldBeTrue();
            a.GetHashCode().ShouldBe(b.GetHashCode());
        }


        [Theory]
        [MemberData(nameof(UnequalStates))]
        public void IsUnequal(State a, State b)
        {
            a.Equals(b).ShouldBeFalse();
        }

        public static IEnumerable<object[]> EqualStates()
        {
            yield return new object[] {State.Parse("101"), State.Parse("101")};
            yield return new object[] {State.Parse("110011"), State.Parse("110011")};
            yield return new object[] {State.Parse("1"), State.Parse("1")};
            yield return new object[] {State.Parse("0101"), State.Parse("0101")};
        }

        public static IEnumerable<object[]> UnequalStates()
        {
            yield return new object[] {State.Parse("101"), State.Parse("111")};
            yield return new object[] {State.Parse("110010"), State.Parse("010011")};
            yield return new object[] {State.Parse("1"), State.Parse("0")};

            yield return new object[] {State.Parse("1010"), State.Parse("010")};
            yield return new object[] {State.Parse("1010"), State.Parse("101")};
        }
    }
}