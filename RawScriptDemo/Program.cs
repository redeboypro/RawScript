using System;
using System.Collections.Generic;
using System.IO;
using RawScript;

namespace RawScriptDemo
{
    internal class Program
    {
        public const string File1Path = "test1.rs";
        public const string File2Path = "test2.rs";
        
        public static void Main(string[] args)
        {
            var evaluator = new Evaluator();
            var terminal = new Output();
            var engine = new Engine();
            
            engine.LoadFromFile(File1Path);
            engine.LoadFromFile(File2Path);
            
            engine.GetFunction(File2Path).Invoke();
            
            for (var i = 0; i < terminal.Length; i++)
            {
                Console.WriteLine(terminal[i]);
            }
        }
    }
}