using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public class FourOfAKind : IHandEvaluator
    {
        public PokerHandEvaluation Evaluate(Hand hand)
        {
            IEnumerable<Card> fourOfAKind = new NofAKind().Find(hand.Cards, 4);

            if (fourOfAKind != null)
            {
                var kicker = (from card in hand.Cards
                             where card.Value != fourOfAKind.First().Value
                             orderby card.Value descending
                             select card).Take(1);

                return new PokerHandEvaluation(true, fourOfAKind.Union(kicker));
            }

            return new PokerHandEvaluation(false, null);
        }
    }
}