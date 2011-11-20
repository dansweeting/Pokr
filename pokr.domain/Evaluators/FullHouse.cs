using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public class FullHouse : IHandEvaluator
    {
        public PokerHandEvaluation Evaluate(Hand hand)
        {
            var nofAKind = new NofAKind();
            var threeOfAKind = nofAKind.Find(hand.Cards, 3);

            if (threeOfAKind != null)
            {
                var restOfTheCards = hand.Cards.Except(threeOfAKind);

                IEnumerable<Card> currentPair, highestPair = null;

                while ((currentPair = nofAKind.Find(restOfTheCards, 2)) != null)
                {
                    if (highestPair == null 
                        || currentPair.First().Value > highestPair.First().Value)
                    {
                        highestPair = currentPair;
                        restOfTheCards = restOfTheCards.Except(currentPair);
                    }   
                }
                if (highestPair != null)
                {
                    return new PokerHandEvaluation(true, threeOfAKind.Union(highestPair));
                }

            }

            return new PokerHandEvaluation(false, null);
        }
    }
}