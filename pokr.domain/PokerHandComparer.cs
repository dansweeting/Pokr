using System;
using System.Collections.Generic;
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
                
            }

            return scoreX.Rank < scoreY.Rank ? -1 : 1;
        }
    }
}
