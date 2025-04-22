using System.Linq;
using Calc.Models;
using Calc.Extensions;

namespace Calc.Handlers
{
    // handles all Plus ops
    public class PlusHandler : OperationHandlerBase
    {
        public override bool CanHandle(Operator op) =>
            op == Operator.Plus;

        public override double Evaluate(Operation operation)
        {
            // sum up the plain values
            var result = operation.Value.Sum();

            // if there's a nested operation, recurse into it
            if (operation.Nested != null)
                result += operation.Nested.EvaluateSelf();

            return result;
        }
    }
}
