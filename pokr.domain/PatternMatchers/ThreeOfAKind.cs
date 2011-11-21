using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class ThreeOfAKind : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch)
        {
            var nofAKind = new NofAKind();

            var three = nofAKind.Find(cardsToMatch, 3);
            if (three != null)
            {
                var nextThree = nofAKind.Find(cardsToMatch.Except(three), 3);

                return nextThree ?? three;
            }

            return null;
        }
    }
}