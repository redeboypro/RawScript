using System;
using System.Collections.Generic;
using RawScript;

namespace RawToolkit
{
    [Instance("input")]
    public class RawInput : Struct
    {
        public RawInput(Engine engine) : base(engine)
        {
            var tokens = new List<int>();
            Add("keyIsAvailable", (variables, parameters) => Console.KeyAvailable);
            Add("readKey", (variables, parameters) => (int) Console.ReadKey().KeyChar);
            Add("readNumber", (variables, parameters) => (float) engine.Evaluator.Evaluate(Console.ReadLine()));
            Add("readTokens", (variables, parameters) =>
            {
                var line = Console.ReadLine();
                
                if (line is null)
                {
                    return;
                }
                
                foreach (var token in line)
                {
                    tokens.Add(token);
                }
            });
            
            Add("clear", (variables, parameters) =>
            {
                tokens.Clear();
            });
            Add("getToken", (variables, parameters) => tokens[Convert.ToInt32(parameters[0])]);
        }
    }
}