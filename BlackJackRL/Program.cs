using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlackJackRL.Constants;
using BlackJackRL.Game;

namespace BlackJackRL
{
    class Program
    {
        static void Main(string[] args)
        {

            PolicyAgent learner = new PolicyAgent();
            learner.Learn();
            RandomAgent randomAgent = new RandomAgent();

            randomAgent.PlayRandom(10000);
            learner.Play(2000);

        
        }
    }
}