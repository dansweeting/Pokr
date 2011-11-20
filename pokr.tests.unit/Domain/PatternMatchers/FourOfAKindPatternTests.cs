using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;
using Pokr.Domain.PatternMatchers;

namespace Pokr.Tests.Unit.Domain.PatternMatchers
{
    [TestFixture]
    public class FourOfAKindPatternTests
    {
        [Test]
        public void ShouldDetectFourOfAKind()
        {
            IEnumerable<Card> result = new FourOfAKind().Match(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             2.Of(Suit.Clubs),
                                                                                             2.Of(Suit.Diamonds),
                                                                                             8.Of(Suit.Clubs),
                                                                                             9.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Hearts)
                                                                                         }));

            Assert.That(result.Count(), Is.EqualTo(4),
                        "Should only have 4 cards in the winning cards collection.");

            Assert.That(result.All( x => x.Value == 2), Is.True, "Should have matched the 2's");

        }

        [Test]
        public void ShouldRejectIfNotFourOfAKind()
        {
            IEnumerable<Card> result = new FourOfAKind().Match(new Hand(new[] {
                                                                                             2.Of(Suit.Hearts),
                                                                                             2.Of(Suit.Spades),
                                                                                             5.Of(Suit.Clubs),
                                                                                             6.Of(Suit.Diamonds),
                                                                                             8.Of(Suit.Clubs),
                                                                                             9.Of(Suit.Hearts),
                                                                                             10.Of(Suit.Hearts)
                                                                                         }));
            Assert.That(result, Is.Null, "Should not have matched anything.");
        }
    }
}