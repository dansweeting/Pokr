using Pokr.Domain.HoldEm;

namespace Pokr.Domain
{
    public interface IHandEvaluator
    {
        PokerHandEvaluation Evaluate(Hand hand);
    }
}