using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlackJackRL.Constants;

namespace BlackJackRL.Game
{
    internal class BlackJack
    {
        public State State { get; set; }
        public int DealerSum { get; set; }
        public bool UsableDealerAce { get; set; }
        public bool PlayerStands { get; set; }

        public BlackJack()
        {
            State = new State();
            var random = new Random();
            State.PlayerSum = random.Next(1, 21);
            State.UsableAce = random.Next(0, 2) == 1;
            int dealerCard1 = random.Next(1, 11);
            int dealerCard2 = random.Next(1, 11);
            DealerSum = dealerCard1 + dealerCard2;
            UsableDealerAce = dealerCard1 == 1 || dealerCard2 == 1;
            State.DealerCard = (Values)dealerCard1;
        }

        internal bool IsTerminal()
        {
            return PlayerStands || State.PlayerSum >= 21 || DealerSum >= 21;
        }

        internal double Play(Actions action)
        {
            if (action == Actions.Hit)
            {
                var random = new Random();
                int card = random.Next(1, 11);
                State.PlayerSum += card;
                if (card == 1 && !State.UsableAce) State.UsableAce = true;
                if (State.PlayerSum > 21)
                {
                    if (State.UsableAce)
                    {
                        State.PlayerSum -= 10;
                        State.UsableAce = false;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            else
            {
                PlayerStands = true;
                while (DealerSum < 17)
                {
                    var random = new Random();
                    int card = random.Next(1, 11);
                    DealerSum += card;
                    if (card == 1 && !UsableDealerAce) UsableDealerAce = true;
                    if (DealerSum > 21)
                    {
                        if (UsableDealerAce)
                        {
                            DealerSum -= 10;
                            UsableDealerAce = false;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }
            if (PlayerStands)
            {
                if (State.PlayerSum > DealerSum) return 1;
                if (State.PlayerSum == DealerSum) return 0;
                return -1;
            }
            return 0;
        }

        internal State getState()
        {
            return new State
            {
                DealerCard = State.DealerCard,
                PlayerSum = State.PlayerSum,
                UsableAce = State.UsableAce
            };
        }
    }
}
