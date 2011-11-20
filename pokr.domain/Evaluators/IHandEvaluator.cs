using Pokr.Domain.HoldEm;

namespace Pokr.Domain.Evaluators
{
    public interface IHandEvaluator
    {
        PokerHandEvaluation Evaluate(Hand hand);
    }
}