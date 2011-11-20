using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.HandEvaluators
{
    [TestFixture]
    public class FlushEvaluatorTests
    {
        [Test]
        public void ShouldDetectAFlush()
        {
            PokerHandEvaluation result =  new Flush().Evaluate(new Hand(new[]
                                                                                {
                                                                                    Picture.King.Of(Suit.Diamonds),
                                                                                    2.Of(Suit.Spades),
                                                                                    3.Of(Suit.Spades),
                                                                                    5.Of(Suit.Spades),
                                                                                    7.Of(Suit.Spades),
                                                                                    9.Of(Suit.Spades),
                                                                                    10.Of(Suit.Clubs)
                                                                                }));

            Assert.That(result.Success, Is.True, "Should have detected the correct hand score from the original hand.");
            Assert.That(result.HighestCard, Is.EqualTo(9.Of(Suit.Spades)), "Should have picked the correct highest card");

            result = new Flush().Evaluate(new Hand(result.WinningCards));
            
            Assert.That(result.Success, Is.True, "Should have detected the correct hand score from the winning subset of cards.");
            Assert.That(result.HighestCard, Is.EqualTo(9.Of(Suit.Spades)), "Should have picked the correct highest card");
        }

        [Test]
        public void ShouldDetectTheHighestFlush()
        {
            PokerHandEvaluation result = new Flush().Evaluate(new Hand(new[]
                                                                                {
                                                                                    Picture.Ace.Of(Suit.Spades),
                                                                                    2.Of(Suit.Spades),
                                                                                    3.Of(Suit.Spades),
                                                                                    5.Of(Suit.Spades),
                                                                                    7.Of(Suit.Spades),
                                                                                    9.Of(Suit.Spades),
                                                                                    10.Of(Suit.Spades)
                                                                                }));

            Assert.That(result.Success, Is.True, "Should have detected the correct hand score from the original hand.");
            Assert.That(result.HighestCard, Is.EqualTo(Picture.Ace.Of(Suit.Spades)), "Should have picked the correct highest card");

            result = new Flush().Evaluate(new Hand(result.WinningCards));

            Assert.That(result.Success, Is.True, "Should have detected the correct hand score from the winning subset of cards.");
            Assert.That(result.HighestCard, Is.EqualTo(Picture.Ace.Of(Suit.Spades)), "Should have picked the correct highest card");
        }

        [Test]
        public void ShouldRejectANonFlush()
        {
            PokerHandEvaluation result = new Flush().Evaluate(new Hand(new[]
                                                                                {
                                                                                    Picture.King.Of(Suit.Diamonds),
                                                                                    2.Of(Suit.Spades),
                                                                                    3.Of(Suit.Spades),
                                                                                    4.Of(Suit.Hearts),
                                                                                    5.Of(Suit.Spades),
                                                                                    6.Of(Suit.Spades),
                                                                                    10.Of(Suit.Clubs)
                                                                                }));

            Assert.That(result.Success, Is.EqualTo( false),"Should not have detected a straight flush in this hand.");
            Assert.That(result.WinningCards, Is.Null, "Should not have winning cards since the flush was not detected.");

        }
        
    }
}