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
            

            //Console.WriteLine(state1.Equals(state2));
            //Console.WriteLine(state1.GetHashCode());


            Learner learner = new Learner();
            learner.Learn();

            learner.Play(2000);
            learner.PlayRandom(2000);

        }
    }
}