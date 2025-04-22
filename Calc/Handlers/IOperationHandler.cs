using Calc.Models;

namespace Calc.Handlers
{
    // every math handler must say which Operator it supports
    // and how to Evaluate an Operation of that type
    public interface IOperationHandler
    {
        bool CanHandle(Operator op);
        double Evaluate(Operation operation);
    }
}
