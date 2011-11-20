using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public class OnePair : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            return new NofAKind().Find(hand.Cards, 2);
        }
    }
}