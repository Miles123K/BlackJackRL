using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlackJackRL.Constants;
using BlackJackRL.Game;

namespace BlackJackRL
{
    internal class Learner
    {
        private readonly Dictionary<State, Dictionary<Actions, double>> _qValues;
        private readonly Dictionary<State, Dictionary<Actions, List<double>>> _returns;
        private Dictionary<State, Actions> _policy;
        private double _epsilon = 0.2;


        private const int MAX_EPOCHS = 100000;

        public Learner()
        {
            _qValues = new Dictionary<State, Dictionary<Actions, double>>();
            _returns = new Dictionary<State, Dictionary<Actions, List<double>>>();
            _policy = new Dictionary<State, Actions>();
        }

        public void Learn()
        {
            for (int i = 0; i < MAX_EPOCHS; i++)
            {
                BlackJack game = new BlackJack();
                List<State> states = new List<State>();
                List<Actions> actions = new List<Actions>();
                List<double> rewards = new List<double>();

                while (!game.IsTerminal())
                {
                    State state = game.getState();
                    var random = new Random();
                    Actions action = GetAction(state);
                    if (random.NextDouble() < _epsilon) action = (Actions)random.Next(0, 2);
                    double reward = game.Play(action);
                    states.Add(state);
                    actions.Add(action);
                    rewards.Add(reward);
                }

                for (int j = 0; j < states.Count; j++)
                {
                    State state = states[j];
                    Actions action = actions[j];
                    double reward = rewards[j];

                    if (!_returns.ContainsKey(state))
                    {
                        _returns.Add(state, new Dictionary<Actions, List<double>>());
                        _returns[state].Add(Actions.Hit, new List<double>());
                        _returns[state].Add(Actions.Stand, new List<double>());
                    }

                    _returns[state][action].Add(reward);

                    double average = _returns[state][action].Average();

                    if (!_qValues.ContainsKey(state))
                    {
                        _qValues.Add(state, new Dictionary<Actions, double>());
                        _qValues[state].Add(Actions.Hit, 0);
                        _qValues[state].Add(Actions.Stand, 0);
                    }

                    _qValues[state][action] = average;

                    if (!_policy.ContainsKey(state))
                    {
                        _policy.Add(state, Actions.Hit);
                    }

                    _policy[state] = _qValues[state].MaxBy(x => x.Value).Key;
                }
            }
        }

        private Actions GetAction(State state)
        {
            if (!_policy.ContainsKey(state))
            {
                _policy.Add(state, Actions.Hit);
            }
            return _policy[state];
        }

        public string GetPolicy()
        {
            StringBuilder sb = new StringBuilder();
            _policy.AsEnumerable().OrderBy(x => x.Key.PlayerSum).ThenBy(x => x.Key.UsableAce).ThenBy(x => x.Key.DealerCard).ToList().ForEach(x => sb.AppendLine(x.Key.ToString() + " " + x.Value.ToString()));
            return sb.ToString();
        }

        public double Play()
        {
            BlackJack game = new BlackJack();
            double reward = 0;
            while (!game.IsTerminal())
            {
                State state = game.getState();
                Actions action = GetAction(state);
                reward = game.Play(action);
            }
            return reward;

        }
        
        public double Play(int epochs)
        {
            Console.WriteLine("Playing {0} epochs", epochs);
            Console.WriteLine("Learned policy after {0} epochs", MAX_EPOCHS);
            double totalReward = 0;
            for (int i = 0; i < epochs; i++)
            {
                totalReward += Play();
            }
            
            Console.WriteLine("Average reward: " + totalReward / epochs);
            return totalReward / epochs;
        }

        public double PlayRandom()
        {
            BlackJack game = new BlackJack();
            double reward = 0;
            while (!game.IsTerminal())
            {
                State state = game.getState();
                var random = new Random();
                Actions action = (Actions)random.Next(0, 2);
                reward = game.Play(action);
            }
            return reward;
        }

        public double PlayRandom(int epochs)
        {
            Console.WriteLine("Playing {0} epochs", epochs);
            Console.WriteLine("Random policy");
            double totalReward = 0;
            for (int i = 0; i < epochs; i++)
            {
                totalReward += PlayRandom();
            }
            Console.WriteLine("Average reward: " + totalReward / epochs);
            return totalReward / epochs;
        }

    }
}
