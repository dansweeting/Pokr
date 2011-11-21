using System.Linq;
using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class FullHouse : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch)
        {
            var nofAKind = new NofAKind();
            var threeOfAKind = nofAKind.Find(cardsToMatch, 3);

            if (threeOfAKind != null)
            {
                var restOfTheCards = cardsToMatch.Except(threeOfAKind);

                IEnumerable<Card> currentPair, highestPair = null;

                while ((currentPair = nofAKind.Find(restOfTheCards, 2)) != null)
                {
                    if (highestPair == null 
                        || currentPair.First().Rank > highestPair.First().Rank)
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