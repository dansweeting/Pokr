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
                var remainX = x.Cards.Except(scoreX.Cards).OrderByDescending(c => c.Rank).ToArray();
                var remainY = y.Cards.Except(scoreY.Cards).OrderByDescending(c => c.Rank).ToArray();

                for( int i = 0; i < remainX.Length; i++)
                {
                    if (remainX[i].Rank == remainY[i].Rank)
                        continue;

                    return remainX[i].Rank - remainY[i].Rank;
                }

                throw new NotImplementedException();
            }

            return scoreX.Rank - scoreY.Rank;
        }
    }
}
