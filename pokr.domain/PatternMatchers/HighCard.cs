using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class HighCard : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            return hand.Cards.OrderByDescending(x => x.Rank).Take(1);
        }
    }
}