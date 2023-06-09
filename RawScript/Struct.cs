﻿using System.Collections.Generic;
using System.Linq;

namespace RawScript
{
    public abstract class Struct
    {
        private readonly List<ExecutableData> executables;
        private readonly List<FunctionData> functions;
        
        protected readonly Engine Engine;

        protected Struct(Engine engine)
        {
            Engine = engine;
            functions = new List<FunctionData>();
            executables = new List<ExecutableData>();
        }

        public int ExecutablesCount => executables.Count;
        
        public int FunctionsCount => functions.Count;

        public ExecutableData GetExecutable(int index)
        {
            return executables[index];
        }
        
        public FunctionData GetFunction(int index)
        {
            return functions[index];
        }

        protected void Add(string name, ParamExecutableTypeDef invokable)
        {
            if (ContainsExecutable(name))
            {
                return;
            }
            executables.Add(new ExecutableData(name, invokable));
        }
        
        protected void Add(string name, ParamFunctionTypeDef invokable)
        {
            if (ContainsFunction(name))
            {
                return;
            }
            functions.Add(new FunctionData(name, invokable));
        }

        private bool ContainsExecutable(string name)
        {
            return executables.Any(ex => ex.Name == name);
        }
        
        private bool ContainsFunction(string name)
        {
            return functions.Any(fun => fun.Name == name);
        }

        public readonly struct ExecutableData
        {
            public ExecutableData(string name, ParamExecutableTypeDef invokable)
            {
                Name = name;
                Invokable = invokable;
            }
            
            public string Name { get; }

            public ParamExecutableTypeDef Invokable { get; }
        }
        
        public readonly struct FunctionData
        {
            public FunctionData(string name, ParamFunctionTypeDef invokable)
            {
                Name = name;
                Invokable = invokable;
            }
            
            public string Name { get; }

            public ParamFunctionTypeDef Invokable { get; }
        }
    }
}