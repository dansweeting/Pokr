using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.PatternMatchers
{
    public interface IHandPatternMatcher
    {
        IEnumerable<Card> Match(IEnumerable<Card> cardsToMatch);
    }
}