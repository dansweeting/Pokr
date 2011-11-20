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
            Hand losingHand = new HandBuilder()
                .WithPair(5)
                .WithCard(10.Of(Suit.Clubs))
                .WithCard(2.Of(Suit.Spades))
                .Build();

            Hand winningHand = new HandBuilder()
                .WithPair(5)
                .WithCard(10.Of(Suit.Spades))
                .WithCard(3.Of(Suit.Clubs))
                .Build();

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
            throw new NotImplementedException();
        }

        [Test]
        public void ShouldHandleHandsOfEqualRankAndDifferentCardValues()
        {
            throw new NotImplementedException();
        }
    }
}