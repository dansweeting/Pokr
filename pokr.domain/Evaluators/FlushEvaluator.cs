using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public class FlushEvaluator : IHandEvaluator
    {
        public static PokerHandEvaluation FailedToMatch = new PokerHandEvaluation(false,null);

        public PokerHandEvaluation Evaluate(Hand hand)
        {
            var groupings = hand.Cards.GroupBy(x => x.Suit).Where( k => k.Count() >= 5);

            if (groupings.Any())
            {
                return new PokerHandEvaluation(true, (from card in hand.Cards
                                                      where card.Suit == groupings.First().Key
                                                      orderby card.Value descending
                                                      select card).Take(5));
            }

            return FailedToMatch;
        }
    }
}