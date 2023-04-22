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
            Instance = this;
        }

        public object this[int index] => data[index];

        public static Output Instance { get; private set; }

        public void Print(object input)
        {
            data.Add(input);
        }
    }
}