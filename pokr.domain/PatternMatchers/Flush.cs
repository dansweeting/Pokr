using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class Flush : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch)
        {
            var groupings = cardsToMatch.GroupBy(x => x.Suit).Where(k => k.Count() >= 5).ToList();

            if (groupings.Any())
            {
                return (from card in cardsToMatch
                        where card.Suit == groupings.First().Key
                        orderby card.Rank descending
                        select card).Take(5);
            }

            return null;
        }
    }
}