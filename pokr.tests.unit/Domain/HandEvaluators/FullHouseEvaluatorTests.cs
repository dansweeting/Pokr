using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.HandEvaluators
{
    [TestFixture]
    public class FullHouseEvaluatorTests
    {
        [Test]
        public void ShouldMatchAFullHouse()
        {
            PokerHandEvaluation result = new FullHouse().Evaluate(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             2.Of(Suit.Clubs),
                                                                                             Picture.Ace.Of(Suit.Diamonds),
                                                                                             Picture.Ace.Of(Suit.Hearts),
                                                                                             9.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Success, Is.True, "Should have detected a full house");

            Assert.That(result.WinningCards.Count(), Is.EqualTo(5),
                        "Should only have 5 cards in the winning cards collection.");

            result = new FullHouse().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have detected four of a kind in the winning subset of cards.");
        }

        [Test]
        public void ShouldMatchAFullHouseWithTheHighestPair()
        {
            PokerHandEvaluation result = new FullHouse().Evaluate(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             2.Of(Suit.Clubs),
                                                                                             10.Of(Suit.Spades),
                                                                                             10.Of(Suit.Hearts),
                                                                                             Picture.Ace.Of(Suit.Diamonds),
                                                                                             Picture.Ace.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Success, Is.True, "Should have detected a full house");

            Assert.That(result.WinningCards.Count(), Is.EqualTo(5),
                        "Should only have 5 cards in the winning cards collection.");

            result = new FullHouse().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have detected four of a kind in the winning subset of cards.");

            Assert.That(result.HighestCard.Value, Is.EqualTo((int)Picture.Ace), "Should have picked the aces as the highest pair.");
        }

        [Test]
        public void ShouldRejectANonFullHouse()
        {
            PokerHandEvaluation result = new FullHouse().Evaluate(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             2.Of(Suit.Clubs),
                                                                                             Picture.Ace.Of(Suit.Diamonds),
                                                                                             Picture.Queen.Of(Suit.Hearts),
                                                                                             9.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Success, Is.False, "Should not have detected a full house");

            Assert.That(result.WinningCards, Is.Null, "Should not have any winning cards");
        }
    }
}