using System.Linq;
using NUnit.Framework;
using Pokr.Domain;

namespace Pokr.Tests.Unit.Domain
{
    [TestFixture]
    public class DeckTests
    {
        [Test]
        public void DeckShouldContainAllTheCards()
        {
            var deck = new Deck();

            var suits = new[] {Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades};

            foreach (var suit in suits)
            {
                foreach (int x in Enumerable.Range(2, 13))
                {
                    Assert.That(deck.Contains(x.Of(suit)), Is.True);
                }
            }
        }

        [Test]
        public void DealShouldRemoveCardFromDeck()
        {
            var deck = new Deck();

            Card? card = deck.Deal();

            Assert.That( deck.Contains(card.Value), Is.False);
        }

        [Test]
        public void DealShouldReturnNullWhenNoCardsLeftInDeck()
        {
            var deck = new Deck();

            for (int i = 0; i < 52; i++)
            {
                Card? dealt = deck.Deal();
                Assert.That(dealt.HasValue, Is.True);
            }

            var card = deck.Deal();

            Assert.That(card, Is.Null);
        }

        [Test]
        public void ShuffleShouldRandomizeTheCardOrder()
        {
            AssertDecksHaveDifferentOrder(new Deck().Shuffle(), new Deck());
        }

        [Test]
        public void TwoShuffledDecksShouldNotHaveTheSameOrder()
        {
            AssertDecksHaveDifferentOrder(new Deck().Shuffle(), new Deck().Shuffle());
        }

        private static void AssertDecksHaveDifferentOrder(Deck first, Deck second)
        {
            int mismatches = 0;
            for (int i = 0; i < 52; i++)
            {
                var cardA = first.Deal().Value;
                var cardB = second.Deal().Value;

                if (!cardA.Equals(cardB))
                    mismatches++;
            }

            Assert.That(mismatches, Is.AtLeast(2));
        }
    }
}
