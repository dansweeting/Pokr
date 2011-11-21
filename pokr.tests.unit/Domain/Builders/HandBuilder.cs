using System.Collections.Generic;
using Pokr.Domain;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.Builders
{
    public class HandBuilder
    {
        private readonly List<Card> _cards = new List<Card>();

        public HandBuilder WithPair(int value)
        {
            _cards.AddRange(new [] { value.Of(Suit.Spades), value.Of(Suit.Diamonds) });
            return this;
        }

        public HandBuilder WithCard(Card card)
        {
            _cards.Add(card);
            return this;
        }

        public Hand Build()
        {
            return new Hand(_cards);
        }

        public HandBuilder WithThreeOfAKind(int value)
        {
            _cards.AddRange( new [] {value.Of(Suit.Clubs), value.Of(Suit.Diamonds), value.Of(Suit.Hearts)});
            return this;
        }
    }
}