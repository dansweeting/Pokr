using System.Collections.Generic;

namespace Pokr.Domain
{
    public class PokerHandScore
    {
        public IEnumerable<Card> Cards { get; private set; }
        public Rank Rank { get; private set; }

        public PokerHandScore(IEnumerable<Card> cards, Rank rank)
        {
            Cards = cards;
            Rank = rank;
        }
    }
}