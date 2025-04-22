// File: Calc/Extensions/OperationExtensions.cs
using System;
using Calc.Models;
using Calc.Services;

namespace Calc.Extensions
{
    public static class OperationExtensions
    {
        // Will be set once at startup via Init()
        private static Maths? _maths;

        public static void Init(Maths maths)
        {
            _maths = maths;
        }

        public static double EvaluateSelf(this Operation op)
        {
            if (_maths == null)
                throw new InvalidOperationException(
                    "Maths service not initialized. Call OperationExtensions.Init() before evaluating.");

            return _maths.Calculate(op);
        }
    }
}
