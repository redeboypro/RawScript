using System.Collections.Generic;
using System.IO;

namespace RawScript
{
    public class Engine
    {
        private readonly Dictionary<string, IInvokable> functions;
        private Dictionary<string, object> variables;

        public Engine()
        {
            variables = new Dictionary<string, object>();
            functions = new Dictionary<string, IInvokable>();
            Instance = this;
        }
        
        public static Engine Instance { get; private set; }
        
        public object GetVariable(string name)
        {
            return variables[name];
        }
        
        public void SetVariable(string variableName, object variable)
        {
            if (variables.ContainsKey(variableName))
            {
                variables[variableName] = variable;
                return;
            }
            
            variables.Add(variableName, variable);
        }

        public void Invoke(string functionName)
        {
            functions[functionName].Invoke();
        }

        public void AddExecutable(string functionName, OperationFunction function)
        {
            functions.Add(functionName, new Operation(ref variables, function));
        }

        public void LoadFromFile(string fileName)
        {
            var functionName = new FileInfo(fileName).Name;
            var functionSource = File.ReadAllText(fileName);
            LoadFromSource(functionName, functionSource);
        }
        
        public void LoadFromSource(string functionName, string functionSource)
        {
            var function = new Function(variables, functionSource);
            functions.Add(functionName, function);
        }
        
        public void LoadModule(Module module)
        {
            foreach (var variable in module.GetVariableNames())
            {
                SetVariable(variable, module.GetVariable(variable));
            }
            
            foreach (var executable in module.GetExecutableNames())
            {
                AddExecutable(executable, module.GetExecutable(executable));
            }
        }
    }
}