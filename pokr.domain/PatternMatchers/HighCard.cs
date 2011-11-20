using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class HighCard : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            return hand.Cards.OrderByDescending(x => x.Value).Take(1);
        }
    }
}