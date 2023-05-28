using System;
using System.Linq;
using RawScript;

namespace RawToolkit
{
    [Instance("array")]
    public class RawArray : Struct
    {
        private object[] array;
        
        public RawArray(Engine engine) : base(engine)
        {
            Add("size", (variables, parameters) =>
            {
                array = new object[Convert.ToInt32(parameters[0])];
            });

            Add("set", (variables, parameters) =>
            {
                array[Convert.ToInt32(parameters[0])] = parameters[1];
            });

            Add("get", (variables, parameters) => array[Convert.ToInt32(parameters[0])]);
            Add("size", (variables, parameters) => array.Length);
            Add("min", (variables, parameters) => array.Min());
            Add("max", (variables, parameters) => array.Max());
        }
    }
}