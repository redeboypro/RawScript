using System;
using System.Collections.Generic;
using System.Text;
using RawScript;

namespace RawToolkit
{
    [Instance("convert")]
    public class RawConverter : Struct
    {
        public RawConverter(Engine engine) : base(engine)
        {
            var tokens = new List<char>();
            Add("append", (variables, parameters) =>
            {
                tokens.Add((char) Convert.ToInt32(parameters[0]));
            });
            Add("clear", (variables, parameters) =>
            {
                tokens.Clear();
            });
            Add("parseTokens", (variables, parameters) => engine.Evaluator.Evaluate(new string(tokens.ToArray())));
            
            
            Add("parseSym", (variables, parameters) => int.Parse(((char)Convert.ToInt32(parameters[0])).ToString()));
            Add("parseStr", (variables, parameters) =>
            {
                var tokenBuilder = new StringBuilder();
                foreach (var parameter in parameters)
                {
                    tokenBuilder.Append((char) Convert.ToInt32(parameter));
                }
                return engine.Evaluator.Evaluate(tokenBuilder.ToString());
            });
            Add("toNumber", (variables, parameters) => Convert.ToInt32(parameters[0]));
            Add("toBoolean", (variables, parameters) =>  Convert.ToBoolean(Convert.ToInt32(parameters[0])));
        }
    }
}