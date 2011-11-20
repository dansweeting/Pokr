using System;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.HandEvaluators
{
    [TestFixture]
    public class PokerHandEvaluatorTests
    {
        [SetUp]
        public void BeforeEachTest()
        {
            //evaluator = new PokerHandEvaluator();
        }

        [Test]
        public void ShouldMatchFullHouse()
        {
            var cards = new [] {
                                   2.Of(Suit.Spades), 
                                   2.Of(Suit.Clubs), 
                                   2.Of(Suit.Hearts), 
                                   3.Of(Suit.Hearts), 
                                   3.Of(Suit.Diamonds),
                                   10.Of(Suit.Hearts),
                                   9.Of(Suit.Spades)};

            //HandPatternMatch handPatternMatch = evaluator.Evaluate(new Hand(cards));

            //Assert.That(handPatternMatch.Score, Is.EqualTo(HandScore.FullHouse));
        }

        [Test]
        public void ShouldMatchStraightFlush()
        {
            var cards = new[] {
                                  2.Of(Suit.Hearts), 
                                  3.Of(Suit.Hearts), 
                                  4.Of(Suit.Hearts), 
                                  5.Of(Suit.Hearts), 
                                  6.Of(Suit.Hearts),
                                  10.Of(Suit.Hearts),
                                  9.Of(Suit.Spades)};

            //HandPatternMatch handPatternMatch = evaluator.Evaluate(new Hand(cards));

            //Assert.That(handPatternMatch.Score, Is.EqualTo(HandScore.StraightFlush));
        }

        [Test]
        public void ShouldMatchFourOfAKind()
        {
            var cards = new[] {
                                  10.Of(Suit.Hearts), 
                                  10.Of(Suit.Spades), 
                                  10.Of(Suit.Clubs), 
                                  10.Of(Suit.Diamonds), 
                                  6.Of(Suit.Hearts),
                                  Picture.Ace.Of(Suit.Hearts),
                                  9.Of(Suit.Spades)};

            //HandPatternMatch handPatternMatch = evaluator.Evaluate(new Hand(cards));

            //Assert.That(handPatternMatch.Score, Is.EqualTo(HandScore.FourOfAKind));
        }

        [Test]
        public void ShouldMatchThreeOfAKind()
        {
            var cards = new[] {
                                  5.Of(Suit.Clubs), 
                                  5.Of(Suit.Hearts), 
                                  5.Of(Suit.Spades), 
                                  7.Of(Suit.Clubs), 
                                  6.Of(Suit.Diamonds),
                                  10.Of(Suit.Hearts),
                                  9.Of(Suit.Spades)};

            //HandPatternMatch handPatternMatch = evaluator.Evaluate(new Hand(cards));

            //Assert.That(handPatternMatch.Score, Is.EqualTo(HandScore.ThreeOfAKind));
        }

        [Test]
        public void ShouldMatchFlush()
        {
            var cards = new[] {
                                  2.Of(Suit.Hearts), 
                                  4.Of(Suit.Hearts), 
                                  6.Of(Suit.Hearts), 
                                  8.Of(Suit.Hearts), 
                                  10.Of(Suit.Diamonds),
                                  Picture.Jack.Of(Suit.Hearts),
                                  Picture.King.Of(Suit.Spades)};

            //HandPatternMatch handPatternMatch = evaluator.Evaluate(new Hand(cards));

            //Assert.That(handPatternMatch.Score, Is.EqualTo(HandScore.Flush));
        }

        [Test]
        public void ShouldMatchStraight()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ShouldMatchTwoPair()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ShouldMatchOnePair()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ShouldMatchHighCard()
        {
            throw new NotImplementedException();
        }
    }
}