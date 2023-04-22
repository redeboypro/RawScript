using System.Collections.Generic;

namespace RawScript.Statements
{
    public class If : Function, IInvokable
    {
        private readonly Condition condition;
        private readonly Dictionary<string, object> variables;

        public If(ref Dictionary<string, object> variables, Condition condition, string source) :
            base(variables, source)
        {
            this.variables = variables;
            this.condition = condition;
        }
        
        public override void Invoke()
        {
            if (!condition.Invoke(variables))
            {
                return;
            }
            
            foreach (var invokable in invokables)
            {
                invokable.Invoke();
            }
        }
    }
}