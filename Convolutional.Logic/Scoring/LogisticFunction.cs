using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic.Scoring
{
    public class LogisticFunction
    {
        private readonly double steepness;
        private readonly double center;

        public LogisticFunction(double steepness = 12.0, double center = 0.5)
        {
            this.steepness = steepness;
            this.center = center;
        }

        public double CalculateScore(IEnumerable<bool> states, IEnumerable<double> measurements)
        {
            var scores =
                measurements
                    .Select(m => 1.0 / (1.0 + Math.Exp(- steepness * (m - center))));

            return
                // ReSharper disable once InvokeAsExtensionMethod
                Enumerable.Zip(scores, states,
                               (score, state) => state ? score : 1 - score
                    )
                    .Sum();
        }
    }
}