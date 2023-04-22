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
            var terminal = new Output();
            var statement = new Statement(locals,@"
var a;
let a = 2;
print a * 23;
");
            statement.Invoke();
            Console.WriteLine(terminal[0]);
        }
    }
}