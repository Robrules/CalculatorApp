using System.Collections.Generic;
using Calc.Models;
using Calc.Services;
using Calc.Extensions;
using Xunit;

namespace Calc.Tests
{
    public class MathsTests
    {
        private readonly Maths _maths;

        public MathsTests()
        {
            // Use default constructor to wire up all handlers
            _maths = new Maths();
            OperationExtensions.Init(_maths);
        }

        [Fact]
        public void Plus_AddsValuesCorrectly()
        {
            var op = new Operation
            {
                ID = Operator.Plus,
                Value = new List<double> { 1, 2, 3 }
            };

            double result = _maths.Calculate(op);
            Assert.Equal(6, result);
        }

        [Fact]
        public void Subtraction_SubtractsValuesCorrectly()
        {
            var op = new Operation
            {
                ID = Operator.Subtraction,
                Value = new List<double> { 10, 3, 2 }
            };

            double result = _maths.Calculate(op);
            Assert.Equal(5, result);
        }

        [Fact]
        public void Multiplication_MultipliesValuesCorrectly()
        {
            var op = new Operation
            {
                ID = Operator.Multiplication,
                Value = new List<double> { 2, 3, 4 }
            };

            double result = _maths.Calculate(op);
            Assert.Equal(24, result);
        }

        [Fact]
        public void Division_DividesValuesCorrectly()
        {
            var op = new Operation
            {
                ID = Operator.Division,
                Value = new List<double> { 20, 4 }
            };

            double result = _maths.Calculate(op);
            Assert.Equal(5, result);
        }

        [Fact]
        public void Nested_AddPlusMultiplication_ReturnsCombinedResult()
        {
            var op = new Operation
            {
                ID = Operator.Plus,
                Value = new List<double> { 2, 3 },
                Nested = new Operation
                {
                    ID = Operator.Multiplication,
                    Value = new List<double> { 4, 5 }
                }
            };

            double result = _maths.Calculate(op);
            Assert.Equal(25, result);
        }

        [Fact]
        public void EmptyValueList_ReturnsZero()
        {
            var op = new Operation { ID = Operator.Plus, Value = new List<double>() };
            var result = _maths.Calculate(op);
            Assert.Equal(0, result);
        }

        [Fact]
        public void SingleItem_Addition_ReturnsSameValue()
        {
            var op = new Operation { ID = Operator.Plus, Value = new List<double> { 7 } };
            var result = _maths.Calculate(op);
            Assert.Equal(7, result);
        }

        [Fact]
        public void DivisionByZero_Throws()
        {
            var op = new Operation { ID = Operator.Division, Value = new List<double> { 10, 0 } };
            Assert.Throws<DivideByZeroException>(() => _maths.Calculate(op));
        }

        [Fact]
        public void UnknownOperator_Throws()
        {
            var op = new Operation { ID = (Operator)999, Value = new List<double> { 1, 2 } };
            Assert.Throws<InvalidOperationException>(() => _maths.Calculate(op));
        }


    }
}
