using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.PatternMatchers
{
    [TestFixture]
    public class FlushPatternTests
    {
        [Test]
        public void ShouldDetectAFlush()
        {
            IEnumerable<Card> result =  new Flush().Match(new Hand(new[]
                                                                                {
                                                                                    Picture.King.Of(Suit.Diamonds),
                                                                                    2.Of(Suit.Spades),
                                                                                    3.Of(Suit.Spades),
                                                                                    5.Of(Suit.Spades),
                                                                                    7.Of(Suit.Spades),
                                                                                    9.Of(Suit.Spades),
                                                                                    10.Of(Suit.Clubs)
                                                                                }));

            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.That(result.All( x => x.Suit == Suit.Spades), Is.True, "Should contain the flush only.");
        }

        [Test]
        public void ShouldDetectTheHighestFlush()
        {
            var cards = new[]
                            {
                                Picture.Ace.Of(Suit.Spades),
                                2.Of(Suit.Spades),
                                3.Of(Suit.Spades),
                                5.Of(Suit.Spades),
                                7.Of(Suit.Spades),
                                9.Of(Suit.Spades),
                                10.Of(Suit.Spades)
                            };

            IEnumerable<Card> result = new Flush().Match(new Hand(cards));

            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.That(result.All(x => x.Suit == Suit.Spades), Is.True, "Should contain the flush only.");

            Assert.That(result, Is.EquivalentTo(cards.OrderByDescending(x => x.Value).Take(5)), 
                "Should have picked the highest spade cards.");
        }

        [Test]
        public void ShouldRejectANonFlush()
        {
            IEnumerable<Card> result = new Flush().Match(new Hand(new[]
                                                                                {
                                                                                    Picture.King.Of(Suit.Diamonds),
                                                                                    2.Of(Suit.Spades),
                                                                                    3.Of(Suit.Spades),
                                                                                    4.Of(Suit.Hearts),
                                                                                    5.Of(Suit.Spades),
                                                                                    6.Of(Suit.Spades),
                                                                                    10.Of(Suit.Clubs)
                                                                                }));

            Assert.That(result, Is.Null, "Should not have matching cards since the flush was not detected.");

        }
        
    }
}