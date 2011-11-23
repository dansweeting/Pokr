using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public CardsBuilder WithFullHouse(int tripletRank, int pairRank)
        {
            _cards.AddRange(new [] 
                    {tripletRank.Of(Suit.Hearts), tripletRank.Of(Suit.Diamonds), tripletRank.Of(Suit.Spades),
                     pairRank.Of(Suit.Spades), pairRank.Of(Suit.Hearts) }
                );

            return this;
        }

        public CardsBuilder WithStraight(int max)
        {
            var rand = new Random();

            for (int i = max; i > max-5 ; i--)
            {
                _cards.Add(new Card( (Suit)rand.Next(4), i ));                
            }

            return this;
        }

        public CardsBuilder WithFlush(Suit suit, int max)
        {
            var rand = new Random();

            _cards.Add(new Card(suit,max));

            for (int i = 0; i < 4; i++)
            {
                _cards.Add(new Card(suit, rand.Next(max)));    
            }
            
            return this;
        }
    }
}