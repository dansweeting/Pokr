using System;
using System.Linq;
using AutoMoq.Helpers;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;
using Pokr.Tests.Unit.Domain.Builders;

namespace Pokr.Tests.Unit.Domain
{
    [TestFixture]
    public class PokerHandComparerTests : AutoMoqTestFixture<PokerHandComparer>
    {
        [SetUp]
        public void BeforeEachTest()
        {
            ResetSubject();
        }

        [Test]
        public void ShouldHandleNulls()
        {
            var hand = new Hand(new[] {2.Of(Suit.Hearts), 3.Of(Suit.Diamonds)});

            Assert.Throws<ArgumentNullException>(() => Subject.Compare(null, null));

            Assert.Throws<ArgumentNullException>(() => Subject.Compare(null, hand));

            Assert.Throws<ArgumentNullException>(() => Subject.Compare(hand, null));
        }

        [Test]
        public void ShouldCompareByUsingHandEvaluator()
        {
            var hand1 = new Hand();
            var hand2 = new Hand();

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(hand1))
                .Returns(new PokerHandScore(null, Rank.OnePair));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(hand2))
                .Returns(new PokerHandScore(null, Rank.TwoPair));

            Assert.That(Subject.Compare(hand1, hand2), Is.EqualTo(-1));
            
            Assert.That(Subject.Compare(hand2, hand1), Is.EqualTo(1));
        }

        [Test]
        public void ShouldCompareWithKickersWhenHandsHaveSameRank()
        {
            Hand losingHand = new Hand(new CardsBuilder()
                .WithPair(5)
                .WithCard(10.Of(Suit.Clubs))
                .WithCard(2.Of(Suit.Spades))
                .Build());

            Hand winningHand = new Hand(new CardsBuilder()
                .WithPair(5)
                .WithCard(10.Of(Suit.Spades))
                .WithCard(3.Of(Suit.Clubs))
                .Build());

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(losingHand))
                .Returns(new PokerHandScore(losingHand.Cards.Where(x => x.Rank == 5), Rank.OnePair));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(winningHand))
                .Returns(new PokerHandScore(winningHand.Cards.Where(x => x.Rank == 5), Rank.OnePair));

            Assert.That(Subject.Compare(losingHand, winningHand), Is.EqualTo(-1));
            Assert.That(Subject.Compare(winningHand, losingHand), Is.EqualTo(1));
        }

        [Test]
        public void ShouldHandleEqualHands()
        {
            Hand left = new Hand(new CardsBuilder()
                .WithThreeOfAKind(7)
                .WithCard(2.Of(Suit.Hearts))
                .WithCard(3.Of(Suit.Clubs))
                .Build());

            Hand right = new Hand(new CardsBuilder()
                .WithThreeOfAKind(7)
                .WithCard(2.Of(Suit.Clubs))
                .WithCard(3.Of(Suit.Diamonds))
                .Build());

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(left))
                .Returns(new PokerHandScore(left.Cards.Where(x => x.Rank == 7), Rank.ThreeOfAKind));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(right))
                .Returns(new PokerHandScore(right.Cards.Where(x => x.Rank == 7), Rank.ThreeOfAKind));

            Assert.That( Subject.Compare(left,right), Is.EqualTo(0), "Hands should be equal." );

        }

        [Test]
        public void ShouldHandleHandsOfEqualRankButDifferentCardValues()
        {
            Hand winner = new Hand(new CardsBuilder()
                .WithThreeOfAKind(5)
                .WithCard(10.Of(Suit.Spades))
                .WithCard(9.Of(Suit.Hearts))
                .Build());

            Hand loser = new Hand(new CardsBuilder()
                .WithThreeOfAKind(4)
                .WithCard(10.Of(Suit.Spades))
                .WithCard(9.Of(Suit.Hearts))
                .Build());

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(winner))
                .Returns(new PokerHandScore(winner.Cards.Where(x => x.Rank == 5), Rank.ThreeOfAKind));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(loser))
                .Returns(new PokerHandScore(loser.Cards.Where(x => x.Rank == 4), Rank.ThreeOfAKind));

            Assert.That(Subject.Compare(loser, winner), Is.EqualTo(-1), "Should have compared the correct loser.");
            Assert.That(Subject.Compare(winner, loser), Is.EqualTo( 1), "Should have compared the correct loser.");
        }

        [Test]
        public void ShouldHandleTwoPairWhenHighPairsDiffer()
        {
            Hand winner = new Hand(new CardsBuilder()
                .WithPair(10)
                .WithPair(8)
                .WithCard(3.Of(Suit.Hearts))
                .Build());

            Hand loser = new Hand(new CardsBuilder()
                .WithPair(9)
                .WithPair(8)
                .WithCard(3.Of(Suit.Hearts))
                .Build());

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(winner))
                .Returns(new PokerHandScore(winner.Cards.Where(x => x.Rank != 3), Rank.TwoPair));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(loser))
                .Returns(new PokerHandScore(loser.Cards.Where(x => x.Rank != 3), Rank.TwoPair));

            Assert.That(Subject.Compare(loser, winner), Is.EqualTo(-1), "Should have compared the correct loser.");
            Assert.That(Subject.Compare(winner, loser), Is.EqualTo(1), "Should have compared the correct loser.");
        }

        [Test]
        public void ShouldHandleTwoPairWhenLowPairsDiffer()
        {
            Hand winner = new Hand(new CardsBuilder()
                .WithPair(9)
                .WithPair(8)
                .WithCard(3.Of(Suit.Hearts))
                .Build());

            Hand loser = new Hand(new CardsBuilder()
                .WithPair(9)
                .WithPair(7)
                .WithCard(3.Of(Suit.Hearts))
                .Build());

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(winner))
                .Returns(new PokerHandScore(winner.Cards.Where(x => x.Rank != 3), Rank.TwoPair));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(loser))
                .Returns(new PokerHandScore(loser.Cards.Where(x => x.Rank != 3), Rank.TwoPair));

            Assert.That(Subject.Compare(loser, winner), Is.EqualTo(-1), "Should have compared the correct loser.");
            Assert.That(Subject.Compare(winner, loser), Is.EqualTo(1), "Should have compared the correct loser.");
        }

        [Test]
        public void ShouldHandleFullHouseWhenTripsDiffer()
        {
            Hand winner = new Hand(new CardsBuilder()
                .WithFullHouse(10,5)
                .Build());

            Hand loser = new Hand(new CardsBuilder()
                .WithFullHouse(9, 5)
                .Build());

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(winner))
                .Returns(new PokerHandScore(winner.Cards, Rank.FullHouse));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(loser))
                .Returns(new PokerHandScore(loser.Cards, Rank.FullHouse));

            Assert.That(Subject.Compare(loser, winner), Is.EqualTo(-1));
            Assert.That(Subject.Compare(winner, loser), Is.EqualTo( 1));
        }

        [Test]
        public void ShouldHandleFullHouseWhenPairsDiffer()
        {
            Hand winner = new Hand(new CardsBuilder()
                .WithFullHouse(10, 5)
                .Build());

            Hand loser = new Hand(new CardsBuilder()
                .WithFullHouse(10, 4)
                .Build());

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(winner))
                .Returns(new PokerHandScore(winner.Cards, Rank.FullHouse));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(loser))
                .Returns(new PokerHandScore(loser.Cards, Rank.FullHouse));

            Assert.That(Subject.Compare(loser, winner), Is.EqualTo(-1));
            Assert.That(Subject.Compare(winner, loser), Is.EqualTo(1));
        }

        [Test]
        public void ShouldHandleStraights()
        {
            Hand winner = new Hand(new CardsBuilder()
                .WithStraight(10)
                .Build());

            Hand loser = new Hand(new CardsBuilder()
                .WithStraight(9)
                .Build());

            Mocked<IPokerHandEvaluator>()
               .Setup(x => x.Evaluate(winner))
               .Returns(new PokerHandScore(winner.Cards, Rank.Straight));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(loser))
                .Returns(new PokerHandScore(loser.Cards, Rank.Straight));

            Assert.That(Subject.Compare(loser, winner), Is.EqualTo(-1));
            Assert.That(Subject.Compare(winner, loser), Is.EqualTo(1));

        }

        [Test]
        public void ShouldHandleFlushes()
        {
            Hand winner = new Hand(new CardsBuilder()
                .WithFlush(Suit.Diamonds, 10)
                .Build());

            Hand loser = new Hand(new CardsBuilder()
                .WithFlush(Suit.Clubs, 9)
                .Build());

            Mocked<IPokerHandEvaluator>()
               .Setup(x => x.Evaluate(winner))
               .Returns(new PokerHandScore(winner.Cards, Rank.Flush));

            Mocked<IPokerHandEvaluator>()
                .Setup(x => x.Evaluate(loser))
                .Returns(new PokerHandScore(loser.Cards, Rank.Flush));

            Assert.That(Subject.Compare(loser, winner), Is.EqualTo(-1));
            Assert.That(Subject.Compare(winner, loser), Is.EqualTo(1));
        }
    }
}