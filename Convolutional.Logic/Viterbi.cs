using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic
{
    public class Viterbi
    {
        private readonly Func<IEnumerable<bool>, IEnumerable<bool>, double> calcScore;
        private readonly IReadOnlyDictionary<State, IReadOnlyList<Transition>> transitionDict;
        private readonly IReadOnlyList<Transition> transitions;

        public Viterbi(IEnumerable<Transition> transitions,
            Func<IEnumerable<bool>, IEnumerable<bool>, double> calcScore)
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
            var results = FindBestPath(states);
            return results;
        }

        private static List<bool> FindBestPath(IReadOnlyList<TrellisState> states)
        {
            var results = new List<bool>(states.Count);

            var state = states[states.Count - 1].StateInfos
                .MinBy(si => si.Score)
                .First();
            results.Add(state.Input);

            for (var i = states.Count - 2; i >= 0; i--)
            {
                state = states[i].StateInfos.Single(si => si.State.Equals(state.PreviousState));
                results.Add(state.Input);
            }

            results.Reverse();
            return results;
        }

        private IReadOnlyList<TrellisState> TrellisDecode(IEnumerable<bool> observations)
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

        public static Viterbi CreateWithHammingDistance(CodeConfig codeConfig)
        {
            var transitions = codeConfig.EnumerateTransitions();
            var viterbi = new Viterbi(transitions, HammingDistance.Calculate);

            return viterbi;
        }
    }
}