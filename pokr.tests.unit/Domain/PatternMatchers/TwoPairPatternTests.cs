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
    public class TwoPairPatternTests
    {
        [Test]
        public void ShouldDetect()
        {
            IEnumerable<Card> result = new TwoPair().Match(new Hand(new[] {
                                                                                Picture.Ace.Of(Suit.Spades),
                                                                                Picture.Ace.Of(Suit.Hearts),
                                                                                Picture.Jack.Of(Suit.Clubs),
                                                                                8.Of(Suit.Diamonds),
                                                                                8.Of(Suit.Spades),
                                                                                2.Of(Suit.Hearts),
                                                                                3.Of(Suit.Clubs)
                                                                            }));

            Assert.That(result.Count(), Is.EqualTo(4), "Should have correctly identified the hand.");

            result = new TwoPair().Match(new Hand(result));
            Assert.That(result.Count(), Is.EqualTo(4), "Should have correctly identified the hand from the winning subset.");
        }

        [Test]
        public void ShouldDetectCorrectKicker()
        {
            IEnumerable<Card> result = new TwoPair().Match(new Hand(new[] {
                                                                                Picture.Ace.Of(Suit.Spades),
                                                                                Picture.Ace.Of(Suit.Hearts),
                                                                                Picture.Jack.Of(Suit.Clubs),
                                                                                8.Of(Suit.Diamonds),
                                                                                8.Of(Suit.Spades),
                                                                                2.Of(Suit.Hearts),
                                                                                3.Of(Suit.Clubs)
                                                                              }));

            Assert.That(result.Count(), Is.EqualTo(4), "Should have correctly identified the hand.");
        }

        [Test]
        public void ShouldReject()
        {
            IEnumerable<Card> result = new TwoPair().Match(new Hand(new[] {
                                                                                        Picture.Ace.Of(Suit.Spades),
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