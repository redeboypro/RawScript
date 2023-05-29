using System.Text;
using RawScript;

namespace RawToolkit
{
    [Instance("evaluator")]
    public class RawEvaluator : Struct
    {
        public RawEvaluator(Engine engine) : base(engine)
        {
            var expressionBuilder = new StringBuilder();
            Add("solve", (variables, parameters) => engine.Evaluator.Evaluate(expressionBuilder.ToString()));
            Add("append", (variables, parameters) =>
            {
                expressionBuilder.Append(parameters.ParametersToString());
            });
            Add("clear", (variables, parameters) =>
            {
                expressionBuilder.Clear();
            });
        }
    }
}