using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlackJackRL.Constants;

namespace BlackJackRL.Game
{
    internal class State
    {
        public Values DealerCard { get; set; }

        public int PlayerSum { get; set; }

        public bool UsableAce { get; set; }

        public State()
        {
            DealerCard = 0;
            PlayerSum = 0;
            UsableAce = false;
        }

        public bool IsTerminal()
        {
            return PlayerSum >= 21;
        }

        public override string ToString()
        {
            return string.Format("DealerCard: {0}, PlayerSum: {1}, UsableAce: {2}", DealerCard, PlayerSum, UsableAce);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(State)) return false;
            State state = (State)obj;
            return state.DealerCard == DealerCard && state.PlayerSum == PlayerSum && state.UsableAce == UsableAce;
        }

        public override int GetHashCode()
        {
            return (int)DealerCard + PlayerSum + (UsableAce ? 1 : 0);
        }

    }
}
