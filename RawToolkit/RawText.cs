using System;
using System.Collections.Generic;
using RawScript;

namespace RawToolkit
{
    [Instance("txt")]
    public class RawText : Struct
    {
        public RawText(Engine engine) : base(engine)
        {
            var chars = new List<char>();
            
            Add("append", (variables, parameters) =>
            {
                chars.Add((char) Convert.ToInt32(parameters[0]));
            });
            
            Add("print", (variables, parameters) =>
            {
                engine.Terminal.Print(new string(chars.ToArray()));
            });
            
            Add("clear", (variables, parameters) =>
            {
                chars.Clear();
            });
        }
    }
}