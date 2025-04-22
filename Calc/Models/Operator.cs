
using System.Text.Json.Serialization;

namespace Calc.Models;

// list of allowed math types
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Operator 
{
    Plus,
    Subtraction,
    Multiplication,
    Division
}