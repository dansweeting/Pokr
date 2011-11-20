using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.HandEvaluators
{
    [TestFixture]
    public class ThreeOfAKindEvaluatorTests
    {
        [Test]
        public void ShouldDetect()
        {
            PokerHandEvaluation result = new ThreeOfAKind().Evaluate(new Hand(new[] {
                                                                                        Picture.Ace.Of(Suit.Spades),
                                                                                        Picture.Ace.Of(Suit.Hearts),
                                                                                        Picture.Ace.Of(Suit.Diamonds),
                                                                                        Picture.King.Of(Suit.Spades),
                                                                                        Picture.Jack.Of(Suit.Clubs),
                                                                                        2.Of(Suit.Hearts),
                                                                                        3.Of(Suit.Clubs)
                                                                                    }));

            Assert.That(result.Success, Is.True, "Should have correctly identified the hand.");
            Assert.That(result.HighestCard.Value, Is.EqualTo((int)Picture.Ace), "Should have correctly picked the highest card.");

            Assert.That(result.WinningCards.Contains(Picture.King.Of(Suit.Spades)), Is.True,
                        "Should have picked the two other highest cards.");

            Assert.That(result.WinningCards.Contains(Picture.Jack.Of(Suit.Clubs)), Is.True,
                        "Should have picked the two other highest cards.");

            result = new ThreeOfAKind().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have correctly identified the hand from the winning subset.");
        }

        [Test]
        public void ShouldReject()
        {
            PokerHandEvaluation result = new ThreeOfAKind().Evaluate(new Hand(new[] {
                                                                                        Picture.Ace.Of(Suit.Spades),
                                                                                        Picture.Queen.Of(Suit.Hearts),
                                                                                        Picture.Ace.Of(Suit.Diamonds),
                                                                                        Picture.King.Of(Suit.Spades),
                                                                                        Picture.Jack.Of(Suit.Clubs),
                                                                                        2.Of(Suit.Hearts),
                                                                                        3.Of(Suit.Clubs)
                                                                                    }));

            Assert.That(result.Success, Is.False, "Should have correctly identified the hand.");
            Assert.That(result.WinningCards, Is.Null);
        }
    }
}