using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    public class OnePair : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            return new NofAKind().Find(hand.Cards, 2);
        }
    }
}