using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class Flush : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            var groupings = hand.Cards.GroupBy(x => x.Suit).Where( k => k.Count() >= 5).ToList();

            if (groupings.Any())
            {
                return (from card in hand.Cards
                        where card.Suit == groupings.First().Key
                        orderby card.Value descending
                        select card).Take(5);
            }

            return null;
        }
    }
}