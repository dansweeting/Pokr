using System;
using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;
using Pokr.Domain.PatternMatchers;

namespace Pokr.Domain
{
    public class PokerHandEvaluator
    {
        private static readonly Dictionary<Rank, IHandPatternMatcher> PatternLookup =
            new Dictionary<Rank, IHandPatternMatcher>
                {
                    {Rank.StraightFlush,    new StraightFlush()},
                    {Rank.FourOfAKind,      new FourOfAKind()},
                    {Rank.FullHouse,        new FullHouse()},
                    {Rank.Flush,            new Flush()},
                    {Rank.Straight,         new Straight()},
                    {Rank.ThreeOfAKind,     new ThreeOfAKind()},
                    {Rank.TwoPair,          new TwoPair()},
                    {Rank.OnePair,          new OnePair()},
                    {Rank.HighCard,         new HighCard()}
                };

        public PokerHandScore Evaluate(Hand hand)
        {
            foreach (Rank rank in PatternLookup.Keys.OrderByDescending( x => x))
            {
                var match = PatternLookup[rank].Match(hand);
                if (match != null)
                {
                    return new PokerHandScore(match, rank);
                }
            }
            throw new InvalidOperationException("No hand was matched!");
        }
    }
}