using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlackJackRL.Constants;
using BlackJackRL.Game;

namespace BlackJackRL
{
    internal class RandomAgent
    {
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
