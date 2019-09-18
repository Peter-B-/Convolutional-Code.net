using System;
using Shouldly;
using Xunit;

namespace Convolutional.Logic.Tests
{
    public class EncoderTests
    {
        private Encoder GetEncoder(bool terminateCode) => new Encoder(CodeConfig.Default3, terminateCode);

        [Fact]
        public void NoTermination()
        {
            var input =new [] {true, false, true, true, false};
            var res = GetEncoder(false).Encode(input);

            res.ShouldBe(new []
            {
                true, true,
                true, false,
                false, false, 
                false, true,
                false, true,
            });
        }

        [Fact]
        public void WithTermination()
        {
            var input = new[] { true, false, true, true, false };
            var res = GetEncoder(true).Encode(input);

            res.ShouldBe(new[]
            {
                true, true,
                true, false,
                false, false,
                false, true,
                false, true,
                true, true
            });
        }

    }
}
