using System;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain
{
    [TestFixture]
    public class PokerHandEvaluatorTests
    {
        private PokerHandEvaluator _evaluator;

        [SetUp]
        public void BeforeEachTest()
        {
            _evaluator = new PokerHandEvaluator();
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

            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.FullHouse));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(5));
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


            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.StraightFlush));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(5));
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

            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.FourOfAKind));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(4));
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

            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.ThreeOfAKind));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(3));
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

            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.Flush));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ShouldMatchStraight()
        {
            var cards = new[] {
                                  2.Of(Suit.Hearts), 
                                  3.Of(Suit.Spades), 
                                  4.Of(Suit.Clubs), 
                                  5.Of(Suit.Diamonds), 
                                  6.Of(Suit.Hearts),
                                  Picture.Jack.Of(Suit.Clubs),
                                  Picture.King.Of(Suit.Spades)};

            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.Straight));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ShouldMatchTwoPair()
        {
            var cards = new[] {
                                  2.Of(Suit.Hearts), 
                                  4.Of(Suit.Spades), 
                                  6.Of(Suit.Clubs), 
                                  8.Of(Suit.Diamonds), 
                                  8.Of(Suit.Hearts),
                                  Picture.King.Of(Suit.Clubs),
                                  Picture.King.Of(Suit.Spades)};

            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.TwoPair));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(4));
        }

        [Test]
        public void ShouldMatchOnePair()
        {
            var cards = new[] {
                                  2.Of(Suit.Hearts), 
                                  4.Of(Suit.Spades), 
                                  6.Of(Suit.Clubs), 
                                  8.Of(Suit.Diamonds), 
                                  8.Of(Suit.Hearts),
                                  Picture.King.Of(Suit.Clubs),
                                  Picture.Queen.Of(Suit.Spades)};

            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.OnePair));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ShouldMatchHighCard()
        {
            var cards = new[] {
                                  2.Of(Suit.Hearts), 
                                  4.Of(Suit.Spades), 
                                  6.Of(Suit.Clubs), 
                                  8.Of(Suit.Diamonds), 
                                  9.Of(Suit.Hearts),
                                  Picture.King.Of(Suit.Clubs),
                                  Picture.Queen.Of(Suit.Spades)};

            PokerHandScore pokerHandScore = _evaluator.Evaluate(new Hand(cards));

            Assert.That(pokerHandScore.Rank, Is.EqualTo(Rank.HighCard));
            Assert.That(pokerHandScore.Cards.Count(), Is.EqualTo(1));
        }
    }
}