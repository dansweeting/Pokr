using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class FourOfAKind : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch)
        {
            return new NofAKind().Find(cardsToMatch, 4);
        }
    }
}