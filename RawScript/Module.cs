using System.Collections.Generic;
using System.Linq;

namespace RawScript
{
    public class Module
    {
        private readonly Dictionary<string, ExecutableTypeDef> executables;
        private readonly Dictionary<string, object> variables;

        protected Module()
        {
            executables = new Dictionary<string, ExecutableTypeDef>();
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

        protected void SetVariable(string variableName, object variable)
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
        
        public ExecutableTypeDef GetExecutable(string functionName)
        {
            return executables[functionName];
        }

        protected void SetExecutable(string functionName, ExecutableTypeDef function)
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