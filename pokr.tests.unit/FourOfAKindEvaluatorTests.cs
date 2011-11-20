using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;
using System.Linq;

namespace Pokr.Tests.Unit
{
    [TestFixture]
    public class FourOfAKindEvaluatorTests
    {
        [Test]
        public void ShouldDetectFourOfAKind()
        {
            PokerHandEvaluation result = new FourOfAKindEvaluator().Evaluate(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             2.Of(Suit.Clubs),
                                                                                             2.Of(Suit.Diamonds),
                                                                                             8.Of(Suit.Clubs),
                                                                                             9.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Success, Is.True, "Should have detected 4 of a kind.");

            Assert.That(result.WinningCards.Count(), Is.EqualTo(5),
                        "Should only have 5 cards in the winning cards collection.");

            result = new FourOfAKindEvaluator().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have detected four of a kind in the winning subset of cards.");


        }

        [Test]
        public void ShouldDetectCorrectKicker()
        {
            PokerHandEvaluation result = new FourOfAKindEvaluator().Evaluate(new Hand(new[] {
                                                                                             10.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Spades),
                                                                                             10.Of(Suit.Clubs),
                                                                                             10.Of(Suit.Diamonds),
                                                                                             8.Of(Suit.Clubs),
                                                                                             2.Of(Suit.Hearts),
                                                                                             5.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Success, Is.True, "Should have detected a full house");
            Assert.That(result.WinningCards.Contains(8.Of(Suit.Clubs)), Is.True, "Should have the correct kicker in the winning 5.");

            result = new FourOfAKindEvaluator().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have detected four of a kind in the winning subset of cards.");
            Assert.That(result.WinningCards.Contains(8.Of(Suit.Clubs)), Is.True, "Should have the correct kicker in the winning 5.");

        }

        [Test]
        public void ShouldRejectIfNotFourOfAKind()
        {
            PokerHandEvaluation result = new FourOfAKindEvaluator().Evaluate(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             5.Of(Suit.Clubs),
                                                                                             6.Of(Suit.Diamonds),
                                                                                             8.Of(Suit.Clubs),
                                                                                             9.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Success, Is.False, "Should not have found a straight in this hand.");
            Assert.That(result.WinningCards, Is.Null, "Should not have winning hands");
        }
    }
}