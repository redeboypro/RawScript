using System;
using RawScript;

namespace RawToolkit
{
    [Instance("dirCmd")]
    public class RawFastOutput : Struct
    {
        public RawFastOutput(Engine engine) : base(engine)
        {
            Add("printf", (variables, parameters) =>
            {
                Console.WriteLine(parameters[0].ToInvariantString());
            });
            Add("println", (variables, parameters) =>
            {
                Console.WriteLine(parameters.ParametersToString());
            });
            Add("clear", (variables, parameters) =>
            {
                Console.Clear();
            });
        }
    }
}