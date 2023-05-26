using System;
using System.Collections.Generic;

namespace RawScript
{
    public delegate void PrintFunction(object input);
    
    public class Output
    {
        private readonly List<object> data;
        
        public Output()
        {
            data = new List<object>();
        }

        public object this[int index] => data[index];

        public int Length => data.Count;

        public void Print(object input)
        {
            data.Add(input);
        }
    }
}