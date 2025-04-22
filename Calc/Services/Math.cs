using System;
using System.Collections.Generic;
using Calc.Handlers;
using Calc.Models;

namespace Calc.Services
{
    public class Maths
    {
        private readonly IEnumerable<IOperationHandler> _handlers;

        // default: wires up all four handlers so we can just do `new Maths()`
        public Maths()
            : this(new IOperationHandler[]
            {
                new PlusHandler(),
                new SubtractionHandler(),
                new MultiplicationHandler(),
                new DivisionHandler()
            })
        { }

        // main constructor: can pass in custom handlers (useful for tests or dependency injection)
        public Maths(IEnumerable<IOperationHandler> handlers) =>
            _handlers = handlers;

        public double Calculate(Operation operation)
        {
            try
            {
                // Look for the handler that says it can process this operator
                IOperationHandler handler = null;
                foreach (var h in _handlers)
                {
                    if (h.CanHandle(operation.ID))
                    {
                        handler = h;
                        break;
                    }
                }

                if (handler == null)
                    // No matching handler found
                    throw new InvalidOperationException(
                        $"No handler registered for operator '{operation.ID}'");

                // Delegate the calculation to the chosen handler
                return handler.Evaluate(operation);
            }
            catch (InvalidOperationException)
            {
                // If we explicitly threw for "no handler", just pass it along
                throw;
            }
            catch (DivideByZeroException)
            {
                // Let division-by-zero errors bubble up directly
                throw;
            }
            catch (Exception ex)
            {
                // Wrap any other unexpected exception with more context
                throw new ApplicationException(
                    $"Calculation failed for operator '{operation.ID}': {ex.Message}", ex);
            }
        }
    }
}

