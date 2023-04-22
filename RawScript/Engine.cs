using System.Collections.Generic;
using System.IO;

namespace RawScript
{
    public class Engine
    {
        private readonly Dictionary<string, object> variables;
        private readonly Dictionary<string, Function> functions;

        public Engine()
        {
            variables = new Dictionary<string, object>();
            functions = new Dictionary<string, Function>();
            Instance = this;
        }
        
        public static Engine Instance { get; private set; }
        
        public object GetVariable(string name)
        {
            return variables[name];
        }

        public Function GetFunction(string name)
        {
            return functions[name];
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
    }
}