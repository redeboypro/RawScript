using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RawScript
{
    public class Engine : IDisposable
    {
        private readonly Dictionary<string, ParamFunctionTypeDef> internalFunctions;
        private readonly Dictionary<string, ParamExecutableTypeDef> internalExecutables;
        private readonly Dictionary<string, _Init> structsInits;
        private readonly List<string> instancesNames;
        
        private Dictionary<string, IInvokable> invokables;

        public Engine()
        {
            internalFunctions = new Dictionary<string, ParamFunctionTypeDef>();
            internalExecutables = new Dictionary<string, ParamExecutableTypeDef>();
            structsInits = new Dictionary<string, _Init>();
            instancesNames = new List<string>();
            invokables = new Dictionary<string, IInvokable>();
            Evaluator = new Evaluator();
            Terminal = new Output();
            Variables = new Dictionary<string, object>();
        }
        
        public Evaluator Evaluator { get; }
        
        public Output Terminal { get; }

        public Dictionary<string, object> Variables { get; private set; }

        public object GetVariable(string name)
        {
            return Variables[name];
        }
        
        public void SetVariable(string variableName, object variable)
        {
            if (Variables.ContainsKey(variableName))
            {
                Variables[variableName] = variable;
                return;
            }
            
            Variables.Add(variableName, variable);
        }
        
        public void SetExecutable(string functionName, ExecutableTypeDef function)
        {
            var operation = new Executable(Variables, function);
            
            if (invokables.ContainsKey(functionName))
            {
                invokables[functionName] = operation;
                return;
            }
            
            invokables.Add(functionName, operation);
        }
        
        public void Invoke(string functionName)
        {
            invokables[functionName].Invoke();
        }
        
        public object InvokeInternalFunction(string functionName, object[] parameters)
        {
            return internalFunctions[functionName].Invoke(Variables, parameters);
        }
        
        public void InvokeInternalExecutable(string functionName, object[] parameters)
        {
            internalExecutables[functionName].Invoke(Variables, parameters);
        }

        public bool ContainsInternalExecutable(string name)
        {
            return internalExecutables.ContainsKey(name);
        }
        
        public bool ContainsInternalFunction(string name)
        {
            return internalFunctions.ContainsKey(name);
        }

        public bool ContainsInstance(string name)
        {
            return instancesNames.Contains(name);
        }
        
        public bool ContainsInstanceType(string name)
        {
            return structsInits.ContainsKey(name);
        }

        public void CreateInstanceOfStruct(string name, string type)
        {
            instancesNames.Add(name);
            structsInits[type].Invoke(name);
        }

        public void LoadFromFile(string fileName)
        {
            var functionName = new FileInfo(fileName).Name;
            var functionSource = File.ReadAllText(fileName);
            LoadFromSource(functionName, functionSource);
        }
        
        public void LoadFromSource(string functionName, string functionSource)
        {
            var function = new Invokable(Invokable.None, this, functionSource);
            invokables.Add(functionName, function);
        }

        public void LoadAssemlyFromFile(string assemblyName)
        {
            var assembly = Assembly.LoadFrom(assemblyName);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var baseType = type.BaseType;
                var instanceAttribute = type.GetCustomAttribute<Instance>();
                
                if (baseType is null || instanceAttribute is null)
                {
                    continue;
                }

                if (baseType.IsAssignableFrom(typeof(Struct)))
                {
                    var typeName = instanceAttribute.Name;

                    structsInits.Add(typeName, name =>
                    {
                        var instance = (Struct) Activator.CreateInstance(type, this);

                        var executablesCount = instance.ExecutablesCount;
                        
                        for (var i = 0; i < executablesCount; i++)
                        {
                            var executableData = instance.GetExecutable(i);
                            internalExecutables.Add(name.JoinLink(executableData.Name), executableData.Invokable);
                        }
                        
                        var functionsCount = instance.FunctionsCount;
                        
                        for (var i = 0; i < functionsCount; i++)
                        {
                            var functionData = instance.GetFunction(i);
                            internalFunctions.Add(name.JoinLink(functionData.Name), functionData.Invokable);
                        }
                    });
                }
            }
        }

        public void Dispose()
        {
            for (var index = 0; index < invokables.Count; index++)
            {
                invokables.ElementAt(index).Value.Dispose();
            }

            invokables = null;
            Variables = null;
        }
    }
}