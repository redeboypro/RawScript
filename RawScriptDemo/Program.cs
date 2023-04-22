using System;
using System.Collections.Generic;
using RawScript;

namespace RawScriptDemo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var locals = new Dictionary<string, object>();
            var evaluator = new Evaluator();
            var statement = new Statement(locals,@"

");
            statement.Invoke();
            Console.WriteLine(locals["A"]);
        }
    }
}