using Pokr.Domain.HoldEm;

namespace Pokr.Domain
{
    public interface IPokerHandEvaluator
    {
        PokerHandScore Evaluate(Hand hand);
    }
}