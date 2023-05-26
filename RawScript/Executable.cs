using System.Collections.Generic;

namespace RawScript
{
    public readonly struct Executable : IInvokable
    {
        private readonly Dictionary<string, object> variables;
        private readonly ExecutableTypeDef operationFunc;

        public Executable(Dictionary<string, object> variables, ExecutableTypeDef operationFunc)
        {
            this.variables = variables;
            this.operationFunc = operationFunc;
        }
        
        public void Invoke()
        {
            operationFunc.Invoke(variables);
        }
        
        public void Dispose()
        {
            /*
             * Not Implemented
             */
        }
    }
}