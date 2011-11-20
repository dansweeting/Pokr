using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    internal class TwoPair : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            var nofAKind = new NofAKind();
            var firstPair = nofAKind.Find(hand.Cards, 2);
            if (firstPair == null)
            {
                return null;
            }
            
            var secondPair = nofAKind.Find(hand.Cards.Except(firstPair), 2);
            if (secondPair == null)
            {
                return null;
            }

            return firstPair.Union(secondPair);
        }
    }
}