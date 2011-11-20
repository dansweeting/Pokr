using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public class NofAKind
    {
        public IEnumerable<Card> Find(IEnumerable<Card> cards, int n)
        {
            var groupsOfN = 
                cards.GroupBy(x => x.Value)
                    .Where(k => k.Count() == n)
                    .ToList();

            if (groupsOfN.Any())
            {
                int cardValue = (from match in groupsOfN.First()
                                 select match.Value).First();

                return from card in cards
                       where card.Value == cardValue
                       select card;
            }

            return null;
        }
    }
}