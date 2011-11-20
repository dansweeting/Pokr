using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit
{
    [TestFixture]
    public class StraightEvaluatorTests
    {
        [Test]
        public void ShouldDetectAStraight()
        {
            PokerHandEvaluation result =  new Straight().Evaluate(new Hand(new[] {
                                                                                              2.Of(Suit.Spades),
                                                                                              3.Of(Suit.Hearts),
                                                                                              4.Of(Suit.Spades),
                                                                                              5.Of(Suit.Spades),
                                                                                              6.Of(Suit.Clubs),
                                                                                              Picture.Ace.Of(Suit.Hearts),
                                                                                              Picture.King.Of(Suit.Clubs)
                                                                                          }));

            Assert.That( result.Success , Is.True, "Should have correctly identified the hand.");
            Assert.That( result.HighestCard, Is.EqualTo(6.Of(Suit.Clubs)), "Should have correctly picked the highest card.");

            result = new Straight().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have correctly identified the hand from the winning subset.");
            
        }

        [Test]
        public void ShouldDetectTheHighestStraight()
        {
            PokerHandEvaluation result = new Straight().Evaluate(new Hand(new[] {
                                                                                             3.Of(Suit.Hearts),
                                                                                             4.Of(Suit.Spades),
                                                                                             5.Of(Suit.Clubs),
                                                                                             6.Of(Suit.Diamonds),
                                                                                             7.Of(Suit.Spades),
                                                                                             8.Of(Suit.Clubs),
                                                                                             9.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Success, Is.True, "Should have correctly identified the hand.");
            Assert.That(result.HighestCard, Is.EqualTo(9.Of(Suit.Hearts)), "Should have correctly picked the highest card.");

            result = new Straight().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have correctly identified the hand from the winning subset.");
        }

        [Test]
        public void ShouldRejectANonStraight()
        {
            PokerHandEvaluation result = new Straight().Evaluate(new Hand(new[] {
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