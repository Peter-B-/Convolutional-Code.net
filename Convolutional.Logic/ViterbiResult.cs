using System.Collections.Generic;

namespace Convolutional.Logic
{
    public class ViterbiResult
    {
        public ViterbiResult(IReadOnlyList<bool> message, double bestEndScore, double? terminationStateScore)
        {
            Message = message;
            BestEndScore = bestEndScore;
            TerminationStateScore = terminationStateScore;
        }

        public IReadOnlyList<bool> Message { get; }
        public double BestEndScore { get;  }
        public double? TerminationStateScore { get;  }
    }
}