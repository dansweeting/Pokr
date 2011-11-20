using System;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public class ThreeOfAKind : IHandEvaluator
    {
        public PokerHandEvaluation Evaluate(Hand hand)
        {
            var nofAKind = new NofAKind();

            var three = nofAKind.Find(hand.Cards, 3);
            if (three != null)
            {
                var next2HighestCards = hand.Cards
                    .Except(three)
                    .OrderByDescending(x => x.Value)
                    .Take(2);

                return new PokerHandEvaluation(true, three.Union(next2HighestCards));
            }

            return new PokerHandEvaluation(false, null);
        }
    }
}