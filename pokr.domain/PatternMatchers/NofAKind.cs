using System.Collections.Generic;
using System.Linq;

namespace Pokr.Domain.PatternMatchers
{
    internal class NofAKind
    {
        public IEnumerable<Card> Find(IEnumerable<Card> cards, int n)
        {
            var groupsOfN = 
                cards.GroupBy(x => x.Rank)
                    .Where(k => k.Count() == n)
                    .ToList();

            if (groupsOfN.Any())
            {
                int cardValue = (from match in groupsOfN.First()
                                 select match.Rank).First();

                return from card in cards
                       where card.Rank == cardValue
                       select card;
            }

            return null;
        }
    }
}