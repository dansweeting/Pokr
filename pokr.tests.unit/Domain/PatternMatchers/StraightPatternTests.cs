using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;
using Pokr.Domain.PatternMatchers;

namespace Pokr.Tests.Unit.Domain.PatternMatchers
{
    [TestFixture]
    public class StraightPatternTests
    {
        [Test]
        public void ShouldDetectAStraight()
        {
            var cards = new[] {
                                  2.Of(Suit.Spades),
                                  3.Of(Suit.Hearts),
                                  4.Of(Suit.Spades),
                                  5.Of(Suit.Spades),
                                  6.Of(Suit.Clubs),
                                  Picture.Ace.Of(Suit.Hearts),
                                  Picture.King.Of(Suit.Clubs)
                              };

            IEnumerable<Card> result =  new Straight().Match(cards);

            Assert.That( result, Is.EquivalentTo(cards.OrderBy( x=> x.Rank).Take(5)), "Should have matched the consecutive cards.");
        }

        [Test]
        public void ShouldDetectTheHighestStraight()
        {
            var cards = new[] {
                                  3.Of(Suit.Hearts),
                                  4.Of(Suit.Spades),
                                  5.Of(Suit.Clubs),
                                  6.Of(Suit.Diamonds),
                                  7.Of(Suit.Spades),
                                  8.Of(Suit.Clubs),
                                  9.Of(Suit.Hearts)
                              };

            IEnumerable<Card> result = new Straight().Match(cards);

            Assert.That( result, Is.EquivalentTo(cards.OrderByDescending( x=>x.Rank).Take(5)), "Should have matched the highest straight.");
        }

        [Test]
        public void ShouldRejectANonStraight()
        {
            IEnumerable<Card> result = new Straight().Match(new[] {
                                                                2.Of(Suit.Hearts),
                                                                2.Of(Suit.Spades),
                                                                5.Of(Suit.Clubs),
                                                                6.Of(Suit.Diamonds),
                                                                8.Of(Suit.Clubs),
                                                                9.Of(Suit.Hearts),
                                                                10.Of(Suit.Hearts)
                                                            });

            Assert.That(result, Is.Null, "Should not have matched anything.");

        }

        [Test]
        public void ShouldDetectAceAsALowCard()
        {
            IEnumerable<Card> result = new Straight().Match(new[] {
                                                                Picture.Ace.Of(Suit.Hearts),
                                                                2.Of(Suit.Spades),
                                                                3.Of(Suit.Clubs),
                                                                4.Of(Suit.Diamonds),
                                                                5.Of(Suit.Clubs),
                                                                9.Of(Suit.Hearts),
                                                                10.Of(Suit.Hearts)
                                                            });

            Assert.That(result.Count(), Is.EqualTo(5), "Should detect the ace as a low card when applicable.");
        }
    }
}