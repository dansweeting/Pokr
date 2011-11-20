using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    internal class FullHouse : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
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

                return highestPair != null ? threeOfAKind.Union(highestPair) : null;
            }

            return null;
        }
    }
}