using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.HandEvaluators
{
    [TestFixture]
    public class PairEvaluatorTests
    {
        [Test]
        public void ShouldDetect()
        {
            PokerHandEvaluation result = new OnePair().Evaluate(new Hand(new[] {
                                                                                Picture.Ace.Of(Suit.Spades),
                                                                                Picture.Queen.Of(Suit.Hearts),
                                                                                Picture.Jack.Of(Suit.Clubs),
                                                                                8.Of(Suit.Diamonds),
                                                                                8.Of(Suit.Spades),
                                                                                2.Of(Suit.Hearts),
                                                                                3.Of(Suit.Clubs)
                                                                            }));

            Assert.That(result.Success, Is.True, "Should have correctly identified the hand.");

            result = new OnePair().Evaluate(new Hand(result.WinningCards));
            Assert.That(result.Success, Is.True, "Should have correctly identified the hand from the winning subset.");
        }

        [Test]
        public void ShouldDetectWinningCards()
        {
            PokerHandEvaluation result = new OnePair().Evaluate(new Hand(new[] {
                                                                                Picture.Ace.Of(Suit.Spades),
                                                                                Picture.Queen.Of(Suit.Hearts),
                                                                                Picture.Jack.Of(Suit.Clubs),
                                                                                8.Of(Suit.Diamonds),
                                                                                8.Of(Suit.Spades),
                                                                                2.Of(Suit.Hearts),
                                                                                3.Of(Suit.Clubs)
                                                                            }));

            Assert.That(result.Success, Is.True, "Should have correctly identified the hand.");

            var expectedWinningCards = new[] {
                                               Picture.Ace.Of(Suit.Spades),
                                               Picture.Queen.Of(Suit.Hearts),
                                               Picture.Jack.Of(Suit.Clubs),
                                               8.Of(Suit.Diamonds),
                                               8.Of(Suit.Spades)
                                           };

            Assert.That( result.WinningCards, Is.EquivalentTo(expectedWinningCards) 
                , "Winning cards should contain the highest 5 from the original 7.");
        }

        [Test]
        public void ShouldReject()
        {
            PokerHandEvaluation result = new OnePair().Evaluate(new Hand(new[] {
                                                                                        5.Of(Suit.Spades),
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

    public class OnePair : IHandEvaluator
    {
        public PokerHandEvaluation Evaluate(Hand hand)
        {
            IEnumerable<Card> pair = new NofAKind().Find(hand.Cards, 2);
            if (pair != null)
            {
                var topCards = hand.Cards.Except(pair).OrderByDescending(x => x.Value).Take(3);
                return new PokerHandEvaluation(true,pair.Union(topCards));
            }
            return new PokerHandEvaluation(false,null);
        }
    }
}