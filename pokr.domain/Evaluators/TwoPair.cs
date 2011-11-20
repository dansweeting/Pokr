using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public class TwoPair : IHandEvaluator
    {
        public PokerHandEvaluation Evaluate(Hand hand)
        {
            var nofAKind = new NofAKind();
            var firstPair = nofAKind.Find(hand.Cards, 2);
            if (firstPair == null)
            {
                return new PokerHandEvaluation(false,null);
            }
            
            var secondPair = nofAKind.Find(hand.Cards.Except(firstPair), 2);
            if (secondPair == null)
            {
                return new PokerHandEvaluation(false,null);
            }

            var kicker =
                hand.Cards
                    .Except(firstPair.Union(secondPair))
                    .OrderByDescending(x => x.Value)
                    .Take(1);

            return new PokerHandEvaluation(true, firstPair.Union(secondPair.Union(kicker)));
        }
    }
}