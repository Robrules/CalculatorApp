using Calc.Models;

namespace Calc.Handlers
{
    // simple base to show inheritance for now...
    public abstract class OperationHandlerBase : IOperationHandler
    {
        public abstract bool CanHandle(Operator op);
        public abstract double Evaluate(Operation operation);
    }
}
