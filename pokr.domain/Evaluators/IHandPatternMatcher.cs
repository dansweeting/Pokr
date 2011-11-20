using System.Collections.Generic;
using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public interface IHandPatternMatcher
    {
        IEnumerable<Card> Match(Hand hand);
    }
}