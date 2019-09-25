using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic.Scoring
{
    public class SymetricScore
    {
        private readonly double steepness;
        private readonly double center;

        public SymetricScore(double steepness = 6.0, double center = 0.0)
        {
            this.steepness = steepness;
            this.center = center;
        }

        public double CalculateScore(IEnumerable<bool> states, IEnumerable<double> measurements)
        {
            var scores =
                measurements
                    .Select(m => -1.0 + 2.0 / (1.0 + Math.Exp(-steepness * (m - center))));

            return
                // ReSharper disable once InvokeAsExtensionMethod
                Enumerable.Zip(scores, states,
                               (score, state) => state ? score : - score
                    )
                    .Average();
        }
    }
}