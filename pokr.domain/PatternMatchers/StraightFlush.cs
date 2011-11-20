using System;
using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    public class StraightFlush : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            Func<IEnumerable<Card>, IEnumerable<Card>> strategyA = TestStraightFirst;
            Func<IEnumerable<Card>, IEnumerable<Card>> strategyB = TestFlushFirst;

            return strategyA(hand.Cards) ?? strategyB(hand.Cards);
        }

        public IEnumerable<Card> TestStraightFirst(IEnumerable<Card> cards )
        {
            IEnumerable<Card> match = new Straight().Match(new Hand(cards));

            return match != null ? new Flush().Match(new Hand(match)) : null;
        }

        public IEnumerable<Card> TestFlushFirst(IEnumerable<Card> cards)
        {
            IEnumerable<Card> match = new Flush().Match(new Hand(cards));

            return match != null ? new Straight().Match(new Hand(match)) : null;
        }
    }
}