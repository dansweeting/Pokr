using System.Collections.Generic;
using Pokr.Domain;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.Builders
{
    public class CardsBuilder
    {
        private readonly List<Card> _cards = new List<Card>();

        public CardsBuilder WithPair(int value)
        {
            _cards.AddRange(new [] { value.Of(Suit.Spades), value.Of(Suit.Diamonds) });
            return this;
        }

        public CardsBuilder WithCard(Card card)
        {
            _cards.Add(card);
            return this;
        }

        public IEnumerable<Card> Build()
        {
            return _cards;
        }

        public CardsBuilder WithThreeOfAKind(int value)
        {
            _cards.AddRange( new [] {value.Of(Suit.Clubs), value.Of(Suit.Diamonds), value.Of(Suit.Hearts)});
            return this;
        }
    }
}