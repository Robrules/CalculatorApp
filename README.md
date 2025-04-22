# Recursive Calculator (Console App)

This is a C# console calculator that supports nested arithmetic operations defined in XML or JSON.

## Features Implemented

- Operators: Plus, Subtraction, Multiplication, Division
- Nested operations: supports recursion (e.g., 2 + 3 + (4 * 5))
- XML and JSON input support
- Deserialisation into typed models
- Exception handling for divide-by-zero and invalid operators
- Flexible structure for input format changes
- Interface and inheritance-based design
- Unit tests using xUnit

## Architecture Overview

- Calc/Models: Contains Operation, Operator, and MathsWrapper classes for XML/JSON deserialization
- Calc/Handlers: Implements each math operation (PlusHandler, SubtractionHandler, etc.) inheriting from OperationHandlerBase and IOperationHandler
- Calc/Services: Holds the Maths class that wires up handlers and performs calculations
- Calc/Extensions: Provides OperationExtensions with the EvaluateSelf() helper for nested recursion
- Calc/Program.cs: Console entry point that reads an input file (XML/JSON), deserializes into MathsWrapper, and invokes the calculator
- Calc.Tests/MathsTests.cs: xUnit test suite covering core operations, nesting and edge cases

## How to Run

### Prerequisites

- .NET 8.0 SDK

### Run the calculator with sample files. There are 2 sample files below which you can run

dotnet run --project Calc sample.xml
or
dotnet run --project Calc sample.json

## Run Tests

dotnet test calc.tests

Covers standard operations, nesting, and error conditions.
