using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;
using Pokr.Domain.PatternMatchers;

namespace Pokr.Tests.Unit.Domain.PatternMatchers
{
    [TestFixture]
    public class OnePairPatternTests
    {
        [Test]
        public void ShouldDetect()
        {
            IEnumerable<Card> result = new OnePair().Match(new Hand(new[] {
                                                                                Picture.Ace.Of(Suit.Spades),
                                                                                Picture.Queen.Of(Suit.Hearts),
                                                                                Picture.Jack.Of(Suit.Clubs),
                                                                                8.Of(Suit.Diamonds),
                                                                                8.Of(Suit.Spades),
                                                                                2.Of(Suit.Hearts),
                                                                                3.Of(Suit.Clubs)
                                                                            }));

            Assert.That(result.Count(), Is.EqualTo(2), "Should have found a match");

            Assert.That(result.All(x => x.Value == 8));
        }

        [Test]
        public void ShouldReject()
        {
            IEnumerable<Card> result = new OnePair().Match(new Hand(new[] {
                                                                                        5.Of(Suit.Spades),
                                                                                        Picture.Queen.Of(Suit.Hearts),
                                                                                        Picture.Ace.Of(Suit.Diamonds),
                                                                                        Picture.King.Of(Suit.Spades),
                                                                                        Picture.Jack.Of(Suit.Clubs),
                                                                                        2.Of(Suit.Hearts),
                                                                                        3.Of(Suit.Clubs)
                                                                                    }));

            Assert.That(result, Is.Null, "Should have correctly rejected.");
        }
    }
}