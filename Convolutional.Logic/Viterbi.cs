using System;
using System.Collections.Generic;
using System.Linq;
using Convolutional.Logic.Extensions;
using Convolutional.Logic.Scores;

namespace Convolutional.Logic
{
    public class Viterbi<TInput>
    {
        private readonly Func<IEnumerable<bool>, IEnumerable<TInput>, double> calcScore;
        private readonly ViterbiConfig config;
        private readonly IReadOnlyDictionary<State, IReadOnlyList<Transition>> transitionDict;
        private readonly IReadOnlyList<Transition> transitions;

        public Viterbi(IEnumerable<Transition> transitions,
            Func<IEnumerable<bool>, IEnumerable<TInput>, double> calcScore, 
            ViterbiConfig config)
        {
            this.calcScore = calcScore;
            this.config = config;

            this.transitions = transitions as IReadOnlyList<Transition> ?? transitions.ToList();

            var dict =
                this.transitions.GroupBy(t => t.InitialState)
                    .ToDictionary(
                        gr => gr.Key,
                        gr => gr.ToList() as IReadOnlyList<Transition>);
            transitionDict = dict;
        }

        private State ZeroState => State.Zero(transitions.First().InitialState);

        public ViterbiResult Solve(IEnumerable<TInput> observations)
        {
            var states = TrellisDecode(observations);
            var results = FindBestPath(states);
            return results;
        }

        private ViterbiResult FindBestPath(IReadOnlyList<TrellisState> states)
        {
            var message = new List<bool>(states.Count);

            var state = states[states.Count - 1].StateInfos
                .GetBestBy(si => si.Score, config.ScoreMethod);
            var bestEndScore = state.Score;

            double? terminationStateScore = null;
            if (config.TerminationState.HasValue)
            { state =
                    states[states.Count - 1].StateInfos
                        .Single(si => si.State.Equals(config.TerminationState.Value));
                terminationStateScore = state.Score;
            }


            message.Add(state.Input);

            for (var i = states.Count - 2; i >= 0; i--)
            {
                state = states[i].StateInfos.Single(si => si.State.Equals(state.PreviousState));
                message.Add(state.Input);
            }

            message.Reverse();

            return new ViterbiResult(message, bestEndScore, terminationStateScore);
        }

        private IReadOnlyList<TrellisState> TrellisDecode(IEnumerable<TInput> observations)
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
                    .Select(gr => gr.GetBestBy( si => si.Score, config.ScoreMethod))
                    .ToList();

                lastState = new TrellisState(lastState.Generation + 1, newStates);
                results.Add(lastState);
            }

            return results;
        }

        private IEnumerable<TrellisStateInfo> GetNextPossibleStates(TrellisStateInfo lastState, IList<TInput> observation)
        {
            return
                transitionDict[lastState.State]
                    .Select(t =>
                    {
                        var score = lastState.Score + calcScore(t.Output, observation);
                        return new TrellisStateInfo(t.InitialState, t.NewState, score, t.Input);
                    });
        }

    }

    public static class Viterbi
    {
        public static Viterbi<bool> CreateWithHammingDistance(CodeConfig codeConfig, bool isTerminated = true)
        {
            var transitions = codeConfig.EnumerateTransitions();
            var viterbiConfig = new ViterbiConfig()
            {
                InitialState = State.Zero(codeConfig.NoOfStateRegisters),
                ScoreMethod = ScoreMethod.Minimize,
            };

            if (isTerminated)
                viterbiConfig.TerminationState = State.Zero(codeConfig.NoOfStateRegisters);

            var viterbi = new Viterbi<bool>(transitions, HammingDistance.Calculate, viterbiConfig);

            return viterbi;
        }

    }
}