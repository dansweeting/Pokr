using System.Collections.Generic;
using System.Linq;

namespace Pokr.Domain
{
    public static class CardExtensions
    {
        public static Card Of(this int value, Suit suit)
        {
            return new Card(suit, value);
        }

        public static Card Of(this Picture picture, Suit suit)
        {
            return ((int)picture).Of(suit);
        }

        public static IEnumerable<Card> All(this Suit suit)
        {
            foreach( int x in Enumerable.Range(2,9))
            {
                yield return x.Of(suit);
            }
            yield return Picture.Jack.Of(suit);
            yield return Picture.Queen.Of(suit);
            yield return Picture.King.Of(suit);
            yield return Picture.Ace.Of(suit);
        } 
    }
}