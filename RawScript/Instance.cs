using System;

namespace RawScript
{
    public class Instance : Attribute
    {
        public Instance(string name)
        {
            Name = name;
        }
        
        public Instance()
        {
            Name = "_INSTANCE";
        }

        public string Name { get; set; }
    }
}