using System.Collections.Generic;
using System.Linq;

namespace RawScript
{
    public class Module
    {
        private readonly Dictionary<string, OperationFunction> executables;
        private readonly Dictionary<string, object> variables;

        public Module()
        {
            executables = new Dictionary<string, OperationFunction>();
            variables = new Dictionary<string, object>();
        }

        public IEnumerable<string> GetVariableNames()
        {
            return variables.Keys.ToArray();
        }
        
        public object GetVariable(string variableName)
        {
            return variables[variableName];
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
        
        public IEnumerable<string> GetExecutableNames()
        {
            return executables.Keys.ToArray();
        }
        
        public OperationFunction GetExecutable(string functionName)
        {
            return executables[functionName];
        }

        public void SetExecutable(string functionName, OperationFunction function)
        {
            if (executables.ContainsKey(functionName))
            {
                executables[functionName] = function;
                return;
            }
            
            executables.Add(functionName, function);
        }
    }
}