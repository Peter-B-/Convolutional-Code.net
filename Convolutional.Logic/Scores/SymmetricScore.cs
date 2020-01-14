using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic.Scores
{
    public class SymmetricScore
    {
        private readonly double steepness;
        private readonly double center;
        private readonly bool invert;

        private SymmetricScore(double steepness , double center, bool invert = false )
        {
            this.steepness = steepness;
            this.center = center;
            this.invert = invert;
        }

        public double CalculateScore(IEnumerable<bool> states, IEnumerable<double> measurements)
        {
            var scores =
                measurements
                    .Select(m => -1.0 + 2.0 / (1.0 + Math.Exp(-steepness * (m - center))));

            return
                // ReSharper disable once InvokeAsExtensionMethod
                Enumerable.Zip(scores, states,
                               (score, state) => (state^invert) ? score : - score
                    )
                    .Average();
        }

        /// <summary>
        /// A symmetric scoring function that is well suited for inputs from -1.0 (false) to 1.0 (true).
        /// </summary>
        public static SymmetricScore MinusOneToOne => new SymmetricScore(6.0, 0);

        /// <summary>
        /// A symmetric scoring function that is well suited for inputs from 0.0 (false) to 255.0 (true).
        /// Use this, if true is encoded as white.
        /// </summary>
        public static SymmetricScore Range_0_255 => new SymmetricScore(0.04, 128);

        /// <summary>
        /// A symmetric scoring function that is well suited for inputs from 255.0 (false) to 0.0 (true).
        /// Use this, if true is encoded as black.
        /// </summary>
        public static SymmetricScore Range_255_0 => new SymmetricScore(0.04, 128, true);
    }
}