using System.Collections.Generic;
using Pokr.Domain.HoldEm;
using System.Linq;

namespace Pokr.Domain.PatternMatchers
{
    internal class Straight : IHandPatternMatcher
    {
        public IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch)
        {
            //Ace can be used in a straight at the start (Ace-5) or end (10-Ace).

            if (!cardsToMatch.Any(x => x.Rank == (int)Picture.Ace))
                return MatchStraight(cardsToMatch);

            var replaced = ReplaceTheAce(cardsToMatch);

            return MatchStraight(cardsToMatch) ?? MatchStraight(replaced);

        }

        private static IEnumerable<Card> ReplaceTheAce(IEnumerable<Card> cardsToMatch)
        {
            var cards = cardsToMatch.ToList();
            var ace = cards.First(x => x.Rank == (int) Picture.Ace);

            cards.Remove(ace);
            cards.Add(new Card(ace.Suit, 1));

            return cards;
        }

        private static IEnumerable<Card> MatchStraight(IEnumerable<Card> cardsToMatch)
        {
            IList<Card> highestStraight = null;
            var cards = cardsToMatch.ToArray();

            for (int i = 0; i < cardsToMatch.Count(); i++)
            {
                var currentStraight = new List<Card> {cards[i]};

                for (int k = 1; k < 5; k++)
                {
                    int increment = k;
                    var next = cards.Where(x => x.Rank == currentStraight[0].Rank + increment);
                    if (!next.Any())
                    {
                        break;
                    }
                    currentStraight.Add(next.First());
                }

                if (currentStraight.Count == 5)
                {
                    var maxCardValue = currentStraight.Max(x => x.Rank);

                    if (highestStraight == null ||
                        maxCardValue > highestStraight.Max(x => x.Rank))
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