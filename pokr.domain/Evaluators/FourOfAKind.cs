using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    internal class FourOfAKind : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            return new NofAKind().Find(hand.Cards, 4);
        }
    }
}