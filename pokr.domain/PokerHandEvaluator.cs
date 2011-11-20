using System;
using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain
{
    public class PokerHandEvaluator
    {
        private static Dictionary<Rank, IHandPatternMatcher> patternLookup =
            new Dictionary<Rank, IHandPatternMatcher>
                {
                    {Rank.Flush, new Flush()},
                    {Rank.FourOfAKind, new FourOfAKind()},
                    {Rank.FullHouse, new FullHouse()},
                    {Rank.OnePair, new OnePair()},
                    {Rank.Straight, new Straight()},
                    {Rank.StraightFlush, new StraightFlush()},
                    {Rank.ThreeOfAKind, new ThreeOfAKind()},
                    {Rank.TwoPair, new TwoPair()},
                    {Rank.HighCard, new HighCard()}
                };

        private static IList<Rank> rankDescending =
            Enum.GetValues(typeof (Rank))
                .Cast<Rank>()
                .OrderByDescending(x => x)
                .ToList();

        public PokerHandScore Evaluate(Hand hand)
        {
            foreach (Rank rank in rankDescending)
            {
                var match = patternLookup[rank].Match(hand);
                if (match != null)
                {
                    return new PokerHandScore(match, rank);
                }
            }
            throw new InvalidOperationException("No hand was matched!");
        }
    }
}