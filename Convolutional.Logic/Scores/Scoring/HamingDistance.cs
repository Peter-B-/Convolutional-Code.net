using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic.Scores.Scoring
{
    public static class HammingDistance
    {
        public static double Calculate(IEnumerable<bool> first, IEnumerable<bool> second)
        {
            return
                // ReSharper disable once InvokeAsExtensionMethod
                Enumerable.Zip(first, second, (a, b) => a != b)
                    .Count(different => different);
        }

        public static double Calculate(IEnumerable<bool> first, IEnumerable<double> second)
        {
            return
                // ReSharper disable once InvokeAsExtensionMethod
                Enumerable.Zip(first, second, (a, b) => a != b > 0.5)
                    .Count(different => different);
        }
    }
}