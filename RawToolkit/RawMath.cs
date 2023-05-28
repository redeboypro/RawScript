using System;
using RawScript;

namespace RawToolkit
{
    [Instance("math")]
    public class RawMath : Struct
    {
        public RawMath(Engine engine) : base(engine)
        {
            Add("pi", (variables, parameters) => (float) Math.PI);
            Add("e", (variables, parameters) => (float) Math.E);
            Add("sin", (variables, parameters) => (float) Math.Sin((float) parameters[0]));
            Add("cos", (variables, parameters) => (float) Math.Sin((float) parameters[0]));
            Add("tan", (variables, parameters) => (float) Math.Tan((float) parameters[0]));
            Add("asin", (variables, parameters) => (float) Math.Asin((float) parameters[0]));
            Add("acos", (variables, parameters) => (float) Math.Acos((float) parameters[0]));
            Add("atan", (variables, parameters) => (float) Math.Atan((float) parameters[0]));
            Add("atan2", (variables, parameters) => (float) Math.Atan2((float) parameters[0], (float) parameters[1]));
            Add("abs", (variables, parameters) => Math.Abs((float) parameters[0]));
            Add("sign", (variables, parameters) => (float) Math.Sign((float) parameters[0]));
            Add("exp", (variables, parameters) => (float) Math.Exp((float) parameters[0]));
            Add("pow", (variables, parameters) => (float) Math.Pow((float) parameters[0], (float) parameters[1]));
            Add("sqrt", (variables, parameters) => (float) Math.Sqrt((float) parameters[0]));
            Add("log", (variables, parameters) => (float) Math.Log((float) parameters[0]));
            Add("log_p", (variables, parameters) => (float) Math.Log((float) parameters[0], (float) parameters[1]));
            Add("log10", (variables, parameters) => (float) Math.Log10((float) parameters[0]));
            Add("min", (variables, parameters) => Math.Min((float) parameters[0], (float) parameters[1]));
            Add("max", (variables, parameters) => Math.Max((float) parameters[0], (float) parameters[1]));
            Add("ceiling", (variables, parameters) => Math.Ceiling((float) parameters[0]));
            Add("round", (variables, parameters) => Math.Round((float) parameters[0]));
            Add("round_p", (variables, parameters) => Math.Round((float) parameters[0], Convert.ToInt32(parameters[1])));
            Add("floor", (variables, parameters) => Math.Floor((float) parameters[0]));
        }
    }
}