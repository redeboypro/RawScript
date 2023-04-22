using System;
using System.Collections.Generic;
using System.IO;
using RawScript;

namespace RawScriptDemo
{
    internal class Program
    {
        public const string FilePath = "test.rs";
        
        public static void Main(string[] args)
        {
            var locals = new Dictionary<string, object>();
            var evaluator = new Evaluator();
            var terminal = new Output();
            var statement = new Statement(locals, File.ReadAllText(FilePath));
            statement.Invoke();
            for (var line = 0; line < terminal.Length; line++)
            {
                Console.WriteLine(terminal[line]);
            }

            while (true);
        }
    }
}