namespace Convolutional.Logic
{
    public class TrellisStateInfo
    {
        public TrellisStateInfo(State previousState, State state, double score, bool input)
        {
            PreviousState = previousState;
            State = state;
            Score = score;
            Input = input;
        }

        public State State { get; }
        public State PreviousState { get; }
        public double Score { get; }
        public bool Input { get; }

        public override string ToString()
        {
            return $"{PreviousState}|{(Input ? "1" : "0")} => {State} ({Score})";
        }
    }
}