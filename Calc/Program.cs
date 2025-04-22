using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using Calc.Models;
using Calc.Services;
using Calc.Extensions;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check if a file path was provided when running the app
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Provide a file path to either a JSON or XML input file.");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.Error.WriteLine($"File not found: {args[0]}");
                return;
            }

            // Read the contents of the provided file (either JSON or XML)
            string input = File.ReadAllText(args[0]).Trim();
            MathsWrapper wrapper;

            // Determine whether the input is XML or JSON based on the first character
            if (input.StartsWith("<"))
            {
                // XML path (unchanged)
                var ser = new XmlSerializer(typeof(MathsWrapper));
                using var reader = new StringReader(input);
                wrapper = (MathsWrapper)ser.Deserialize(reader)!;
            }
            else
            {
                // JSON path: extract the inner "Maths" object before deserializing
                var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                using var doc = JsonDocument.Parse(input);
                // get the { "Maths": { ... } } element
                var mathsElement = doc.RootElement.GetProperty("Maths");

                // now deserialize just that sub-object into thewrapper
                wrapper = JsonSerializer.Deserialize<MathsWrapper>(
                    mathsElement.GetRawText(),
                    opts
                )!;
            }       

            // Create the calculator and hook up operation handlers
            var maths = new Maths();
            OperationExtensions.Init(maths);

            double result;

            try
            {
                // Run the calculation using the top-level Operation from the input
                result = maths.Calculate(wrapper.Operation);
            }
            catch (Exception ex)
            {
                // If anything fails (e.g. invalid operation or division by zero), print an error
                Console.Error.WriteLine($"Calculation failed: {ex.Message}");
                return;
            }

            // Print the final result to the console
            Console.WriteLine(result);
        }
    }
}
