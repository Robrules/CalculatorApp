using System.Linq;
using Calc.Models;
using Calc.Extensions;

namespace Calc.Handlers
{
    // handles all Subtraction ops
    public class SubtractionHandler : OperationHandlerBase
    {
        public override bool CanHandle(Operator op) =>
            op == Operator.Subtraction;

        public override double Evaluate(Operation operation)
        {
            // start by subtracting the list: e.g. [10, 3, 2] => 10 - 3 - 2 = 5
            //C# doesnt have a built in subtraction method for this, so we make out own
            var result = operation.Value
                                  .Aggregate((a, b) => a - b);

            // if there's a nested operation, subtract its result
            if (operation.Nested != null)
                result -= operation.Nested.EvaluateSelf();

            return result;
        }
    }
}
