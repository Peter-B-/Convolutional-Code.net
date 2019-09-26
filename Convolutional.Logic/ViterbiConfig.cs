namespace Convolutional.Logic
{
    public class ViterbiConfig
    {
        public State InitialState { get; set; }
        public State? TerminationState { get; set; }
        public ScoreMethod ScoreMethod { get; set; }
    }
}