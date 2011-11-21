using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pokr.Domain;
using Pokr.Domain.HoldEm;
using Pokr.Domain.PatternMatchers;

namespace Pokr.Tests.Unit.Domain.PatternMatchers
{
    [TestFixture]
    public class StraightFlushPatternTests
    {
        [Test]
        public void ShouldMatchStraightFlush()
        {
            IEnumerable<Card> result = new StraightFlush().Match(new[]
                                                                    {
                                                                        Picture.Ace.Of(Suit.Clubs),   //
                                                                        Picture.King.Of(Suit.Clubs),  //
                                                                        Picture.Queen.Of(Suit.Clubs), //
                                                                        Picture.Jack.Of(Suit.Clubs),  //
                                                                        10.Of(Suit.Clubs),            //
                                                                        Picture.Ace.Of(Suit.Spades),
                                                                        2.Of(Suit.Hearts)
                                                                    });

            Assert.That( result.Count(), Is.EqualTo(5), "Should have matched this straight flush.");
        }

        [Test]
        public void ShouldReject()
        {
            IEnumerable<Card> result = new StraightFlush().Match(new[]
                                                                    {
                                                                        Picture.Ace.Of(Suit.Hearts),
                                                                        Picture.King.Of(Suit.Clubs),
                                                                        Picture.Queen.Of(Suit.Clubs),
                                                                        Picture.Jack.Of(Suit.Clubs),
                                                                        10.Of(Suit.Clubs),
                                                                        Picture.Ace.Of(Suit.Spades),
                                                                        2.Of(Suit.Hearts)
                                                                    });

            Assert.That(result, Is.Null, "Should not have matched.");
        }

        [Test]
        public void ShouldMatchHighestCards()
        {
            var cards = new[]
                            {
                                Picture.Ace.Of(Suit.Clubs),     //
                                Picture.King.Of(Suit.Clubs),    //
                                Picture.Queen.Of(Suit.Clubs),   //
                                Picture.Jack.Of(Suit.Clubs),    //
                                10.Of(Suit.Clubs),              //
                                9.Of(Suit.Clubs),
                                2.Of(Suit.Hearts)
                            };

            IEnumerable<Card> result = new StraightFlush().Match(cards);

            Assert.That( result, Is.EquivalentTo( cards.OrderByDescending( x => x.Rank).Take(5)), 
                "Should have matched the highest straight flush.");
        }

        [Test]
        public void ShouldMatchWhenHighestStraightIsNotAFlush()
        {
            var cards = new[]
                            {
                                Picture.Ace.Of(Suit.Hearts),
                                Picture.King.Of(Suit.Clubs),    //
                                Picture.Queen.Of(Suit.Clubs),   //
                                Picture.Jack.Of(Suit.Clubs),    //
                                10.Of(Suit.Clubs),              //
                                9.Of(Suit.Clubs),               //
                                2.Of(Suit.Hearts)
                            };

            IEnumerable<Card> result = new StraightFlush().Match(cards);

            Assert.That(result.Count(), Is.EqualTo(5), "Should have matched this straight flush.");
        }

        [Test]
        public void ShouldMatchWhenHighestFlushIsNotAStraight()
        {
            var cards = new[]
                            {
                                Picture.Ace.Of(Suit.Clubs),
                                Picture.Queen.Of(Suit.Clubs),   //
                                Picture.Jack.Of(Suit.Clubs),    //
                                10.Of(Suit.Clubs),              //
                                9.Of(Suit.Clubs),               //
                                8.Of(Suit.Clubs),               //
                                5.Of(Suit.Hearts)
                            };

            IEnumerable<Card> result = new StraightFlush().Match(cards);

            Assert.That(result.Count(), Is.EqualTo(5), "Should have matched this straight flush.");
        }
    }
}