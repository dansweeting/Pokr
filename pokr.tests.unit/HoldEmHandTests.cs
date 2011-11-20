using System;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;
using The = Pokr.Domain.Picture;

namespace Pokr.Tests.Unit
{
    [TestFixture]
    public class HoldEmHandTests
    {
        [Test]
        public void AddShouldAdd()
        {
            var hand = new Hand();

            hand.Add(The.King.Of(Suit.Spades));
            hand.Add(The.Queen.Of(Suit.Hearts));
            hand.Add( 1.Of(Suit.Diamonds));

            Assert.That(hand.Cards.Contains(The.King.Of(Suit.Spades)), Is.True);
            Assert.That(hand.Cards.Contains(The.Queen.Of(Suit.Hearts)), Is.True);
        }

        [Test]
        public void HandCannotContainMoreThan7Cards()
        {
            var hand = new Hand();

            hand.Add(1.Of(Suit.Clubs));
            hand.Add(2.Of(Suit.Clubs));
            hand.Add(3.Of(Suit.Clubs));
            hand.Add(4.Of(Suit.Clubs));
            hand.Add(5.Of(Suit.Clubs));
            hand.Add(6.Of(Suit.Clubs));
            hand.Add(7.Of(Suit.Clubs));

            Assert.Throws<InvalidOperationException>(() => hand.Add(10.Of(Suit.Hearts)),
                                                     "Should not be possible to have more than 7 cards in a holdem hand.");
        }
    }
}