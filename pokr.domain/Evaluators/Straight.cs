using System.Collections.Generic;
using Pokr.Domain.HoldEm;
using System.Linq;

namespace Pokr.Domain.Evaluators
{
    internal class Straight : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(Hand hand)
        {
            IList<Card> highestStraight = null;
            var cards = hand.Cards.ToArray();

            for (int i = 0; i < hand.Cards.Count; i++)
            {
                var currentStraight = new List<Card> {cards[i]};

                for (int k = 1; k < 5; k++)
                {
                    int increment = k;
                    var next = cards.Where(x => x.Value == currentStraight[0].Value + increment);
                    if (!next.Any())
                    {
                        break;
                    }
                    currentStraight.Add(next.First());
                }

                if (currentStraight.Count == 5)
                {
                    var maxCardValue = currentStraight.Max(x => x.Value);

                    if (highestStraight == null ||
                        maxCardValue > highestStraight.Max(x => x.Value))
                    {
                        highestStraight = currentStraight;
                    }
                }
            }

            if (highestStraight != null && highestStraight.Count == 5)
            {
                return highestStraight;
            }

            return null;
        }
    }
}