namespace Pokr.Domain
{
    public struct Card
    {
        private readonly Suit _suit;
        private readonly int _rank;

        public Card(Suit suit, int rank)
        {
            _suit = suit;
            _rank = rank;
        }

        public int Rank
        {
            get { return _rank; }
        }

        public Suit Suit
        {
            get { return _suit; }
        }

        public override string ToString()
        {
            if (Rank == 1 || Rank > 10)
            {
                var value = ((Picture) Rank).ToString();
                return string.Format("{0} of {1}", value, Suit);
            }
            return string.Format("{0} of {1}", Rank, Suit);
        }
    }
}