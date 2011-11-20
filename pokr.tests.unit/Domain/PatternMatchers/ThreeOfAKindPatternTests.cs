using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.PatternMatchers
{
    [TestFixture]
    public class ThreeOfAKindPatternTests
    {
        [Test]
        public void ShouldDetect()
        {
            IEnumerable<Card> result = new ThreeOfAKind().Match(new Hand(new[] {
                                                                                        Picture.Ace.Of(Suit.Spades),
                                                                                        Picture.Ace.Of(Suit.Hearts),
                                                                                        Picture.Ace.Of(Suit.Diamonds),
                                                                                        Picture.King.Of(Suit.Spades),
                                                                                        Picture.Jack.Of(Suit.Clubs),
                                                                                        2.Of(Suit.Hearts),
                                                                                        3.Of(Suit.Clubs)
                                                                                    }));


            Assert.That(result.Count(), Is.EqualTo(3), "Should contain the 3 of a kind.");
            Assert.That(result.All(x => x.Value == (int)Picture.Ace), Is.True, "Should have matched the aces.");

        }

        [Test]
        public void ShouldDetectHighestThree()
        {
            IEnumerable<Card> result = new ThreeOfAKind().Match(new Hand(new[] {
                                                                                        Picture.Queen.Of(Suit.Spades),
                                                                                        Picture.Queen.Of(Suit.Hearts),
                                                                                        Picture.Queen.Of(Suit.Diamonds),
                                                                                        Picture.King.Of(Suit.Spades),
                                                                                        Picture.King.Of(Suit.Clubs),
                                                                                        Picture.King.Of(Suit.Hearts),
                                                                                        3.Of(Suit.Clubs)
                                                                                    }));

            Assert.That(result.Count(), Is.EqualTo(3), "Should contain the 3 of a kind.");

            Assert.That( result.All( x => x.Value == (int)Picture.King), Is.True, "Should have picked the higher triplet.");
        }

        [Test]
        public void ShouldReject()
        {
            IEnumerable<Card> result = new ThreeOfAKind().Match(new Hand(new[] {
                                                                                        Picture.Ace.Of(Suit.Spades),
                                                                                        Picture.Queen.Of(Suit.Hearts),
                                                                                        Picture.Ace.Of(Suit.Diamonds),
                                                                                        Picture.King.Of(Suit.Spades),
                                                                                        Picture.Jack.Of(Suit.Clubs),
                                                                                        2.Of(Suit.Hearts),
                                                                                        3.Of(Suit.Clubs)
                                                                                    }));

            Assert.That(result, Is.Null, "Should not have matched anything.");
        }
    }
}