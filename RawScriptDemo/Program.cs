﻿using System;
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
            
            engine.SetVariable("math_input", 0.0f);
            engine.SetVariable("math_result", 0.0f);
            engine.AddExecutable("math_sin", inputVariables =>
            {
                
            });
            engine.AddExecutable("math_cos", inputVariables =>
            {
                
            });
            
            engine.Invoke(File2Path);
            
            for (var i = 0; i < terminal.Length; i++)
            {
                Console.WriteLine(terminal[i]);
            }
        }
    }
}