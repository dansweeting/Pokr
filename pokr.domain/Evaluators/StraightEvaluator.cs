using System;
using System.Collections.Generic;
using Pokr.Domain.HoldEm;
using System.Linq;

namespace Pokr.Domain.Evaluators
{
    public class StraightEvaluator : IHandEvaluator
    {
        public PokerHandEvaluation Evaluate(Hand hand)
        {
            var cards = (from c in hand.Cards
                        orderby c.Value descending 
                        select c).ToArray();

            IList<Card> highestRun = null;
            int highestCard = 0;

            foreach (Card card in cards)
            {
                IList<Card> neighbours = FindNeigbours(card, cards).ToList();

                int highCard = neighbours.Max(x => x.Value);
                if ( highCard > highestCard || 
                    ( highestRun != null && neighbours.Count > highestRun.Count))
                {
                    highestCard = highCard;
                    highestRun = neighbours;
                }
            }

            if ( highestRun != null && highestRun.Count >= 5)
            {
                var highestFive = (from card in highestRun
                                   orderby card.Value descending
                                   select card).Take(5);

                return new PokerHandEvaluation(true, highestFive);
            }

            return FlushEvaluator.FailedToMatch;
        }

        private IEnumerable<Card> FindNeigbours(Card card, IEnumerable<Card> cards )
        {
            var neighbours = from c in cards
                             let diff = c.Value - card.Value
                             where diff >= 0 && diff < 5
                             select c;

            neighbours = RemoveDuplicateValues(neighbours);

            return neighbours;
        }

        private IEnumerable<Card> RemoveDuplicateValues(IEnumerable<Card> item)
        {
            var cardValues = new HashSet<int>();
            foreach (var neighbour in item)
            {
                if (!cardValues.Contains(neighbour.Value))
                {
                    cardValues.Add(neighbour.Value);
                    yield return neighbour;
                }
            }
        }
    }
}