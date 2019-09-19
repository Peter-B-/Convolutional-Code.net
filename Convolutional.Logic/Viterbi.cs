using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Convolutional.Logic.Extensions;

namespace Convolutional.Logic
{
    public class Viterbi
    {
        private readonly Func<IEnumerable<bool>, IEnumerable<bool>, double> calcScore;
        private readonly IReadOnlyDictionary<State, IReadOnlyList<Transition>> transitionDict;
        private readonly IReadOnlyList<Transition> transitions;

        public Viterbi(IEnumerable<Transition> transitions, Func<IEnumerable<bool>, IEnumerable<bool>, double> calcScore)
        {
            this.calcScore = calcScore;
            this.transitions = transitions as IReadOnlyList<Transition> ?? transitions.ToList();

            var dict =
                this.transitions.GroupBy(t => t.InitialState)
                    .ToDictionary(
                        gr => gr.Key,
                        gr => gr.ToList() as IReadOnlyList<Transition>);
            transitionDict = dict;
        }

        private State ZeroState => State.Zero(transitions.First().InitialState);

        public IReadOnlyList<bool> Solve(IEnumerable<bool> observations)
        {
            var states = TrellisDecode(observations);

            var results = new List<bool>(states.Count);

            var state = states[states.Count - 1].StateInfos
                .MinBy(si => si.Score)
                .First();
            results.Add(state.Input);

            for (var i = states.Count-2; i >= 0; i--)
            {
                state = states[i].StateInfos.Single(si => si.State.Equals(state.PreviousState));
                results.Add(state.Input);
            }

            results.Reverse();
            return results;
        }

        public IReadOnlyList<TrellisState> TrellisDecode(IEnumerable<bool> observations)
        {
            var obsList = observations.Buffer(2).ToList();
            var results = new List<TrellisState>(obsList.Count);

            var lastState = new TrellisState(0, new List<TrellisStateInfo>
            {
                new TrellisStateInfo(ZeroState, ZeroState, 0, false)
            });

            foreach (var observation in obsList)
            {
                var newStates = lastState.StateInfos
                    .SelectMany(si => GetNextPossibleStates(si, observation))
                    .GroupBy(si => si.State)
                    .Select(gr => gr.MinBy(si => si.Score).First())
                    .ToList();

                lastState = new TrellisState(lastState.Generation + 1, newStates);
                results.Add(lastState);
            }

            return results;
        }

        private IEnumerable<TrellisStateInfo> GetNextPossibleStates(TrellisStateInfo lastState, IList<bool> observation)
        {
            var transitions =
                transitionDict[lastState.State]
                    .Select(t =>
                    {
                        var score = lastState.Score + calcScore(t.Output, observation);
                        return new TrellisStateInfo(t.InitialState, t.NewState, score, t.Input);
                    });
            return transitions;
        }
    }

    public class TrellisState
    {
        public TrellisState(int generation, IReadOnlyList<TrellisStateInfo> stateInfos)
        {
            Generation = generation;
            StateInfos = stateInfos;
        }

        public int Generation { get; }
        public IReadOnlyList<TrellisStateInfo> StateInfos { get; }

        public override string ToString()
        {
            return string.Join(
                Environment.NewLine,
                StateInfos.Select(s => s.ToString())
                    .Prepend($"Gen {Generation}:")
            );

        }
    }

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
            return $"{PreviousState}|{(Input?"1":"0")} => {State} ({Score})";
        }
    }
}