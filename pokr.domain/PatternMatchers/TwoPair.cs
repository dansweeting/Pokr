using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class TwoPair : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch)
        {
            var nofAKind = new NofAKind();
            var firstPair = nofAKind.Find(cardsToMatch, 2);
            if (firstPair == null)
            {
                return null;
            }

            var secondPair = nofAKind.Find(cardsToMatch.Except(firstPair), 2);
            if (secondPair == null)
            {
                return null;
            }

            return firstPair.Union(secondPair);
        }
    }
}