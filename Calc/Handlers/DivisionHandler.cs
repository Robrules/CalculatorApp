using System;
using System.Linq;
using Calc.Models;
using Calc.Extensions;

namespace Calc.Handlers
{
    public class DivisionHandler : OperationHandlerBase
    {
        public override bool CanHandle(Operator op) =>
            op == Operator.Division;

        public override double Evaluate(Operation operation)
        {
            // if any divisor is zero, throw
            for (int i = 1; i < operation.Value.Count; i++)
            {
                if (operation.Value[i] == 0)
                    throw new DivideByZeroException($"Cannot divide by zero at position {i}");
            }

            // divide in sequence: [20, 2, 2] => ((20 / 2) / 2) = 5
            //C# doesnt have a built in division method for this, so we make out own
            var result = operation.Value.Aggregate((a, b) => a / b);

            // check nested result too
            if (operation.Nested != null)
            {
                double nested = operation.Nested.EvaluateSelf();
                if (nested == 0)
                    throw new DivideByZeroException("Cannot divide by zero (nested operation result)");
                result /= nested;
            }

            return result;
        }
    }
}
