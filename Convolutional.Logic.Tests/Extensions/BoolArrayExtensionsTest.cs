using System.Linq;
using Convolutional.Logic.Extensions;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests.Extensions
{
    public class BoolArrayExtensionsTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        [InlineData(15)]
        [InlineData(354987)]
        public void GetBoolsGetInt(int no)
        {
            var bools = no.GetBools().ToList();
            var res = bools.GetInt();

            res.ShouldBe(no);
        }
    }
}