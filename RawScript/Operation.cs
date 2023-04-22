using System.Collections.Generic;

namespace RawScript
{
    public readonly struct Operation : IInvokable
    {
        private readonly Dictionary<string, object> variables;
        private readonly OperationFunction operationFunc;

        public Operation(ref Dictionary<string, object> variables, OperationFunction operationFunc)
        {
            this.variables = variables;
            this.operationFunc = operationFunc;
        }
        
        public void Invoke()
        {
            operationFunc.Invoke(variables);
        }
    }
}