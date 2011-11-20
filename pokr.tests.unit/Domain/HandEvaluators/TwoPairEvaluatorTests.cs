using System;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.HandEvaluators
{
    [TestFixture]
    public class TwoPairEvaluatorTests
    {
        [Test]
        public void ShouldDetect()
        {
            PokerHandEvaluation result = new TwoPair().Evaluate(new Hand(new[] {
                                                                                Picture.Ace.Of(Suit.Spades),
                                                                                Picture.Ace.Of(Suit.Hearts),
                                                                                Picture.Jack.Of(Suit.Clubs),
                                                                                8.Of(Suit.Diamonds),
                                                                                8.Of(Suit.Spades),
                                                                                2.Of(Suit.Hearts),
                                                                                3.Of(Suit.Clubs)
                                                                            }));

            Assert.That(result.Success, Is.True, "Should have correctly identified the hand.");

            result = new TwoPair().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have correctly identified the hand from the winning subset.");
        }

        [Test]
        public void ShouldDetectCorrectKicker()
        {
            PokerHandEvaluation result = new TwoPair().Evaluate(new Hand(new[] {
                                                                                Picture.Ace.Of(Suit.Spades),
                                                                                Picture.Ace.Of(Suit.Hearts),
                                                                                Picture.Jack.Of(Suit.Clubs),
                                                                                8.Of(Suit.Diamonds),
                                                                                8.Of(Suit.Spades),
                                                                                2.Of(Suit.Hearts),
                                                                                3.Of(Suit.Clubs)
                                                                              }));

            Assert.That(result.Success, Is.True, "Should have correctly identified the hand.");
            Assert.That(result.WinningCards.Contains(Picture.Jack.Of(Suit.Clubs)), Is.True, "Should have selected correct kicker card.");
        }

        [Test]
        public void ShouldReject()
        {
            PokerHandEvaluation result = new TwoPair().Evaluate(new Hand(new[] {
                                                                                        Picture.Ace.Of(Suit.Spades),
                                                                                        Picture.Queen.Of(Suit.Hearts),
                                                                                        Picture.Ace.Of(Suit.Diamonds),
                                                                                        Picture.King.Of(Suit.Spades),
                                                                                        Picture.Jack.Of(Suit.Clubs),
                                                                                        2.Of(Suit.Hearts),
                                                                                        3.Of(Suit.Clubs)
                                                                                    }));

            Assert.That(result.Success, Is.False, "Should have correctly rejected the hand.");
            Assert.That(result.WinningCards, Is.Null, "Should have correctly rejected.");
        }
    }
}