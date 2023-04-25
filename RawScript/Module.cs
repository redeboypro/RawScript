using System.Collections.Generic;

namespace RawScript
{
    public class Module
    {
        private readonly Dictionary<string, OperationFunction> executables;
        private readonly Dictionary<string, object> variables;
        private List<string> variableNames;
        private List<string> executableNames;

        public Module()
        {
            executables = new Dictionary<string, OperationFunction>();
            variables = new Dictionary<string, object>();
            variableNames = new List<string>();
            executableNames = new List<string>();
        }

        public string[] GetVariableNames()
        {
            return variableNames.ToArray();
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
            variableNames.Add(variableName);
        }
        
        public string[] GetExecutableNames()
        {
            return executableNames.ToArray();
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
            executableNames.Add(functionName);
        }
    }
}