using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    internal class ThreeOfAKind : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            var nofAKind = new NofAKind();

            var three = nofAKind.Find(hand.Cards, 3);
            if (three != null)
            {
                var nextThree = nofAKind.Find(hand.Cards.Except(three), 3);

                return nextThree ?? three;
            }

            return null;
        }
    }
}