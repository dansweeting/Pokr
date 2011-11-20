using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.Evaluators;
using Pokr.Domain.HoldEm;

namespace Pokr.Tests.Unit.Domain.PatternMatchers
{
    [TestFixture]
    public class FullHousePatternTests
    {
        [Test]
        public void ShouldMatchAFullHouse()
        {
            IEnumerable<Card> result = new FullHouse().Match(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             2.Of(Suit.Clubs),
                                                                                             Picture.Ace.Of(Suit.Diamonds),
                                                                                             Picture.Ace.Of(Suit.Hearts),
                                                                                             9.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Count(), Is.EqualTo(5),
                        "Should only have 5 cards in the winning cards collection.");

            result = new FullHouse().Match(new Hand(result));
            Assert.That(result, Is.Not.Null, "Should have detected a match in the winning subset of cards.");
        }

        [Test]
        public void ShouldMatchAFullHouseWithTheHighestPair()
        {
            var cards = new[] {
                                  2.Of(Suit.Hearts),
                                  2.Of(Suit.Spades),
                                  10.Of(Suit.Clubs),
                                  10.Of(Suit.Spades),
                                  10.Of(Suit.Hearts),
                                  Picture.Ace.Of(Suit.Diamonds),
                                  Picture.Ace.Of(Suit.Hearts)
                              };

            IEnumerable<Card> result = new FullHouse().Match(new Hand(cards));

            Assert.That(result.Count(), Is.EqualTo(5), "Should have matched.");
            Assert.That(result.Count(x => x.Value == (int)Picture.Ace), Is.EqualTo(2), "Should have matched the highest full house.");
            Assert.That(result.Count(x => x.Value == 10), Is.EqualTo(3), "Should have matched the highest full house.");
        }

        [Test]
        public void ShouldRejectANonFullHouse()
        {
            IEnumerable<Card> result = new FullHouse().Match(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             2.Of(Suit.Clubs),
                                                                                             Picture.Ace.Of(Suit.Diamonds),
                                                                                             Picture.Queen.Of(Suit.Hearts),
                                                                                             9.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result, Is.Null, "Should not have detected a full house");
        }
    }
}