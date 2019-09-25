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

        [Theory]
        [InlineData(1, 1, "1")]
        [InlineData(1, 8, "00000001")]
        [InlineData(8, 4, "1000")]
        [InlineData(10, 4, "1010")]
        [InlineData(15, 4, "1111")]
        [InlineData(354987, 20, "01010110101010101011")]
        public void GetBools(int no, int count, string expected)
        {
            var bools = no.GetBools(count).ToList();


            bools.Format().ShouldBe(expected);
        }

    }
}