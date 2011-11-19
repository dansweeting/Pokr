namespace Pokr.Domain
{
    public struct Card
    {
        private readonly Suit _suit;
        private readonly int _value;

        public Card(Suit suit, int value)
        {
            _suit = suit;
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }

        public Suit Suit
        {
            get { return _suit; }
        }

        public override string ToString()
        {
            if (Value == 1 || Value > 10)
            {
                var value = ((Picture) Value).ToString();
                return string.Format("{0} of {1}", value, Suit);
            }
            return string.Format("{0} of {1}", Value, Suit);
        }
    }
}