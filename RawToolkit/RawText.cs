using System;
using System.Collections.Generic;
using System.Text;
using RawScript;

namespace RawToolkit
{
    [Instance("txt")]
    public class RawText : Struct
    {
        public RawText(Engine engine) : base(engine)
        {
            var resultStr = new StringBuilder();
            
            Add("append", (variables, parameters) =>
            {
                resultStr.Append(parameters.ParametersToString());
            });
            
            Add("print", (variables, parameters) =>
            {
                engine.Terminal.Print(resultStr.ToString());
            });
            
            Add("clear", (variables, parameters) =>
            {
                resultStr.Clear();
            });
        }
    }
}