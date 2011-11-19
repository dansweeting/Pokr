using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokr.Domain
{
    public class Deck
    {
        private static readonly Random Random = new Random();
        private readonly Queue<Card> _cards;
 
        public Deck()
        {
            _cards = new Queue<Card>(from suit in new[] {Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades}
                                     from card in suit.All()
                                     select card);
        }

        internal Deck(IEnumerable<Card> cards )
        {
            _cards = new Queue<Card>(cards);
        }

        public bool Contains(Card card)
        {
            return _cards.Contains(card);
        }

        public Card? Deal()
        {
            if (_cards.Count >= 1)
                return _cards.Dequeue();

            return null;
        }

        public Deck Shuffle()
        {
            var cards = new List<Card>();

            Card? card;
            while ( (card = Deal())!= null )
            {
                cards.Add(card.Value);
            }

            var shuffledList = new List<Card>();

            while (cards.Count > 0)
            {
                int indexToRemove = Random.Next(cards.Count);

                shuffledList.Add(cards[indexToRemove]);
                cards.RemoveAt(indexToRemove);
            }

            return new Deck(shuffledList);
        }
    }
}