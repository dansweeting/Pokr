using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pokr.Domain.HoldEm
{
    public class Hand
    {
        private readonly List<Card> _cards;

        public Hand()
            : this(Enumerable.Empty<Card>())
        {}

        public Hand(IEnumerable<Card> cards )
        {
            _cards = new List<Card>(cards);
        }

        public ReadOnlyCollection<Card> Cards
        {
            get { return _cards.AsReadOnly(); }
        }

        public void Add(Card card)
        {
            if (_cards.Count == 7)
                throw new InvalidOperationException();

            _cards.Add(card);
        }
    }
}