using System.Collections.Generic;
using System.Linq;

namespace Pokr.Domain
{
    public class PokerHandEvaluation
    {
        private readonly IEnumerable<Card> _winningCards;

        public PokerHandEvaluation(bool success, IEnumerable<Card> winningCards)
        {
            _winningCards = winningCards;
            Success = success;
        }

        public IEnumerable<Card> WinningCards
        {
            get { return _winningCards; }
        }

        public bool Success { get; private set; }

        public Card HighestCard
        {
            get
            {
                return WinningCards.OrderBy(x => x.Value).Last();
            }
        }
    }
}