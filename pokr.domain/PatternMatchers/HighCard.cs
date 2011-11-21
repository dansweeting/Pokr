using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class HighCard : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch)
        {
            return cardsToMatch.OrderByDescending(x => x.Rank).Take(1);
        }
    }
}