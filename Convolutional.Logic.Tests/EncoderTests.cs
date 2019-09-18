using System;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests
{
    public class EncoderTests
    {
        private Encoder Encoder => new Encoder();

        [Fact]
        public void TestExample()
        {
            var input =new [] {true, false, true, true, false};
            var res = Encoder.Encode(input);

            res.ShouldBe(new []
            {
                true, true,
                true, false,
                false, false, 
                false, true,
                false, true,
            });
        }

    }
}
