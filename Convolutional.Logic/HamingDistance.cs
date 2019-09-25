using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic
{
    public class HammingDistance
    {
        public static double Calculate(IEnumerable<bool> first, IEnumerable<bool> second)
        {
            return
                first.Zip(second, (a, b) => a != b)
                    .Count(different => different);
        }

        public static double Calculate(IEnumerable<bool> first, IEnumerable<double> second)
        {
            return
                first.Zip(second, (a, b) => a != (b > 0.5))
                    .Count(different => different);
        }

    }
}