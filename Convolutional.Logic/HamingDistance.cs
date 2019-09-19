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
    }
}