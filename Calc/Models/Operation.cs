// File: Calc/Models/Operation.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text.Json.Serialization;

namespace Calc.Models
{
    // Stores a single math operation (e.g. "add 2 and 3")
    public class Operation
    {
        [XmlAttribute("ID")]
        [JsonPropertyName("@ID")]
        public Operator ID { get; set; }

        [XmlElement("Value")]
        [JsonPropertyName("Value")]
        public List<double> Value { get; set; } = new();

        [XmlElement("Operation")]
        [JsonPropertyName("Operation")]
        public Operation? Nested { get; set; }
    }

    // Wraps the top‑level <Maths>…</Maths> or { "Maths": … }
    [XmlRoot("Maths")]
    public class MathsWrapper
    {
        [XmlElement("Operation")]
        [JsonPropertyName("Operation")]
        public Operation Operation { get; set; } = default!;
    }
}