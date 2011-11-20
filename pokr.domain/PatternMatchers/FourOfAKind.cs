using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class FourOfAKind : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            return new NofAKind().Find(hand.Cards, 4);
        }
    }
}