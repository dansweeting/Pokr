using System;
using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    public class StraightFlush : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch)
        {
            Func<IEnumerable<Card>, IEnumerable<Card>> strategyA = TestStraightFirst;
            Func<IEnumerable<Card>, IEnumerable<Card>> strategyB = TestFlushFirst;

            return strategyA(cardsToMatch) ?? strategyB(cardsToMatch);
        }

        public IEnumerable<Card> TestStraightFirst(IEnumerable<Card> cards )
        {
            IEnumerable<Card> match = new Straight().Match(cards);

            return match != null ? new Flush().Match(cards) : null;
        }

        public IEnumerable<Card> TestFlushFirst(IEnumerable<Card> cards)
        {
            IEnumerable<Card> match = new Flush().Match(cards);

            return match != null ? new Straight().Match(cards) : null;
        }
    }
}