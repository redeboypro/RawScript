using System;
using System.Collections.Generic;
using System.Linq;
using RawScript;

namespace RawToolkit
{
    [Instance("list")]
    public class RawList : Struct
    {
        public RawList(Engine engine) : base(engine)
        {
            var list = new List<object>();
            
            Add("add", (variables, parameters) =>
            {
                list.Add(parameters[0]);
            });
            
            Add("set", (variables, parameters) =>
            {
                list[Convert.ToInt32(parameters[0])] = parameters[1];
            });
            
            Add("get", (variables, parameters) => list[Convert.ToInt32(parameters[0])]);
            Add("size", (variables, parameters) => list.Count);
            Add("min", (variables, parameters) => list.Min());
            Add("max", (variables, parameters) => list.Max());
        }
    }
}