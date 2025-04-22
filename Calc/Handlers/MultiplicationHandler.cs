using System.Linq;
using Calc.Models;
using Calc.Extensions;

namespace Calc.Handlers
{
    // handles all Multiplication ops
    public class MultiplicationHandler : OperationHandlerBase
    {
        public override bool CanHandle(Operator op) =>
            op == Operator.Multiplication;

        public override double Evaluate(Operation operation)
        {
            // multiply all values in the list: [2, 3, 4] => 2 × 3 × 4 = 24
            //C# doesnt have a built in multiplication method for this, so we make out own
            var result = operation.Value
                                  .Aggregate((a, b) => a * b);

            // if there's a nested operation, subtract its result
            if (operation.Nested != null)
                result -= operation.Nested.EvaluateSelf();

            return result;
        }
    }
}
