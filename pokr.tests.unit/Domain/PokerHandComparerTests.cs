using System;
using AutoMoq.Helpers;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;

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
    }
}