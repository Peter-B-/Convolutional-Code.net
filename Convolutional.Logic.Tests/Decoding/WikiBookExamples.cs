using System.Collections.Generic;
using Convolutional.Logic.Extensions;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Convolutional.Logic.Tests.Decoding
{
    /// <summary>
    /// This class contains unit tests for the decoding examples provided in
    /// https://en.wikibooks.org/wiki/A_Basic_Convolutional_Coding_Example
    /// </summary>
    public class WikiBook
    {
        private readonly ITestOutputHelper output;

        public WikiBook(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void WorkbookExamples(TestCase testCase)
        {
            var decoder = Viterbi.CreateWithHammingDistance(testCase.Config);
            var res = decoder.Solve(testCase.Input.ParseBools());


            var expected = testCase.Expected.ParseBools();
            output.WriteLine("Result:   "  +res.Message.Format());
            output.WriteLine("Expected: "  +expected.Format());

            res.Message.ShouldBe(expected);
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            // Figure 5: Example no errors
            yield return new object[]{new TestCase(){ Config = Size3_7_6, Input = "11 11 01 00 01 10 00", Expected = "1 0 1 1 0 0 0"}};

            // Figure 6: 2 errors correctable
            yield return new object[]{new TestCase(){ Config = Size3_7_6, Input = "11 01 01 00 00 10 00", Expected = "1 0 1 1 0 0 0"}};
            
            // Figure 7: 2 errors not correctable
            yield return new object[] { new TestCase() { Config = Size3_7_6, Input = "11 01 01 00 11 10 00", Expected = "1 1 0 1 0 0 0" } };

            // (7,5) example, no errors
            yield return new object[] { new TestCase() { Config = CodeConfig.Size3_7_5, Input = "11 10 00 01 01 11 00", Expected = "1 0 1 1 0 0 0" } };

            // (7,5) example, with correctable errors
            yield return new object[] { new TestCase() { Config = CodeConfig.Size3_7_5, Input = "11 00 00 01 11 11 00", Expected = "1 0 1 1 0 0 0" } };
        }

        private static CodeConfig Size3_7_6 => CodeConfig.Generate(3, 0x7,0x6);

        public class TestCase
        {
            public CodeConfig Config { get; set; }
            public string Input { get; set; }
            public string Expected { get; set; }
        }
    }
}