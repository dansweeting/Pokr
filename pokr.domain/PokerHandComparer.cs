using System;
using System.Collections.Generic;
using System.Linq;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain
{
    public class PokerHandComparer : IComparer<Hand>
    {
        private readonly IPokerHandEvaluator _evaluator;

        public PokerHandComparer(IPokerHandEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public int Compare(Hand x, Hand y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();

            PokerHandScore scoreX = _evaluator.Evaluate(x);
            PokerHandScore scoreY = _evaluator.Evaluate(y);

            if (scoreX.Rank == scoreY.Rank)
            {
                int comparisonNoKickers = CompareEqualPokerHandScores(scoreX.Cards, scoreY.Cards);

                return comparisonNoKickers != 0 ?
                    comparisonNoKickers :
                    CompareEqualPokerHandScores(x.Cards.Except(scoreX.Cards), y.Cards.Except(scoreY.Cards));
            }

            return scoreX.Rank - scoreY.Rank;
        }
        
        private static int CompareEqualPokerHandScores(IEnumerable<Card> x, IEnumerable<Card> y)
        {
            IEnumerable<Card> xCards = x;
            IEnumerable<Card> yCards = y;
            while (xCards.Any())
            {
                var highestX = xCards.Max(c => c.Rank);
                var highestY = yCards.Max(c => c.Rank);

                if (highestX == highestY)
                {
                    xCards = xCards.Where(c => c.Rank != highestX);
                    yCards = yCards.Where(c => c.Rank != highestX);
                    continue;
                }

                return highestX - highestY;
            }
            return 0;
        }
    }
}
