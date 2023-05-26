using System;
using System.Collections.Generic;
using RawScript;

namespace RawToolkit
{
    [Instance]
    public class RawMath : Module
    {
        private const string PI = "math_pi";
        private const string Epsilon = "math_epsilon";
        
        private const string Parameter = "math_parameter";
        private const string Result = "math_result";
        private const string PushParameter = "math_push_parameter";
        private const string Clear = "math_clear";
        
        private const string Sin = "math_sin";
        private const string Cos = "math_cos";
        private const string Tan = "math_tan";
        private const string Cot = "math_cot";
        
        private const string ASin = "math_asin";
        private const string ACos = "math_acos";
        private const string ATan = "math_atan";
        private const string ATan2 = "math_atan2";
        private const string ACot = "math_acot";
        
        private const string Abs = "math_abs";
        private const string Sign = "math_sign";
        
        private const string Pow = "math_pow";
        private const string Log = "math_log";
        private const string Log10 = "math_log10";
        
        private const string Ceil = "math_ceil";
        private const string Floor = "math_float";
        private const string Round = "math_round";
        
        private const string Min = "math_min";
        private const string Max = "math_max";

        public RawMath()
        {
            Parameters = new List<object>();
            
            SetVariable(Parameter, 0.0f);
            SetVariable(Result, 0.0f);
            SetVariable(PI, (float) Math.PI);
            SetVariable(Epsilon, float.Epsilon);
            
            SetExecutable(PushParameter, inputVariables =>
            {
                Parameters.Add(inputVariables[Parameter]);
            });
            SetExecutable(Clear, inputVariables =>
            {
                Parameters.Clear();
            });
            
            SetExecutable(Sin, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Sin((float) Parameters[0]);
            });
            SetExecutable(Cos, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Cos((float) Parameters[0]);
            });
            SetExecutable(Tan, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Tan((float) Parameters[0]);
            });
            SetExecutable(Cot, inputVariables =>
            {
                inputVariables[Result] = 1.0f / Math.Tan((float) Parameters[0]);
            });
            
            SetExecutable(ASin, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Asin((float) Parameters[0]);
            });
            SetExecutable(ACos, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Acos((float) Parameters[0]);
            });
            SetExecutable(ATan, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Atan((float) Parameters[0]);
            });
            SetExecutable(ATan2, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Atan2((float) Parameters[0], (float) Parameters[1]);
            });
            SetExecutable(ACot, inputVariables =>
            {
                inputVariables[Result] = (float) Math.PI / 2.0f - Math.Atan((float) Parameters[0]);
            });
            
            SetExecutable(Abs, inputVariables =>
            {
                inputVariables[Result] = Math.Abs((float) Parameters[0]);
            });
            SetExecutable(Sign, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Sign((float) Parameters[0]);
            });
            
            SetExecutable(Pow, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Pow((float) Parameters[0], (float) Parameters[1]);
            });
            SetExecutable(Log, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Log((float) Parameters[0], (float) Parameters[1]);
            });
            SetExecutable(Log10, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Log10((float) Parameters[0]);
            });
            
            SetExecutable(Ceil, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Ceiling((float) Parameters[0]);
            });
            SetExecutable(Floor, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Floor((float) Parameters[0]);
            });
            SetExecutable(Round, inputVariables =>
            {
                inputVariables[Result] = (float) Math.Round((float) Parameters[0]);
            });
            
            SetExecutable(Min, inputVariables =>
            {
                inputVariables[Result] = Math.Min((float) Parameters[0], (float) Parameters[1]);
            });
            SetExecutable(Max, inputVariables =>
            {
                inputVariables[Result] = Math.Max((float) Parameters[0], (float) Parameters[1]);
            });
        }
        
        public List<object> Parameters { get; }
    }
}