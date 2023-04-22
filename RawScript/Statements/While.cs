using System.Collections.Generic;

namespace RawScript.Statements
{
    public class While : Function, IInvokable
    {
        private readonly Condition condition;
        private readonly Dictionary<string, object> variables;

        public While(ref Dictionary<string, object> variables, Condition condition, string source) :
            base(variables, source)
        {
            this.variables = variables;
            this.condition = condition;
        }
        
        public override void Invoke()
        {
            while (condition.Invoke(variables))
            {
                foreach (var invokable in invokables)
                {
                    invokable.Invoke();
                }
            }
        }
    }
}