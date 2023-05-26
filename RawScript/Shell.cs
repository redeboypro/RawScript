using System;
using System.Collections.Generic;

namespace RawScript
{
    public enum BlockType
    {
        If,
        While,
        None
    }
    
    public enum ExecutionType
    {
        Print,
        Execute,
        None
    }

    public enum DeclarationType
    {
        Variable,
        Operation,
        Local,
        Instance,
        None
    }
    
    public enum OperationType { 
        Equalize,
        Multiply,
        Divide,
        Add,
        Subtract,
        None
    }
    
    public delegate object FunctionTypeDef(Dictionary<string, object> variables);
    
    public delegate void ExecutableTypeDef(Dictionary<string, object> variables);
    
    public delegate object ParamFunctionTypeDef(Dictionary<string, object> variables, object[] parameters);
    
    public delegate void ParamExecutableTypeDef(Dictionary<string, object> variables, object[] parameters);

    public delegate void _Init(string name);

    public static class Shell
    {
        public const string TypeNamePrefix = "TypeName";
        
        public const char TokenSeparator = ' ';
        public const string DeclarationSeparator = ";";
        public const string ParameterSeparator = ",";
        public const string RootSeparator = ".";

        public const char BracketsOpening = '(';
        public const char BracketsClosing = ')';

        public const string BeginStatement = "{";
        public const string EndStatement = "}";
        
        private const string Execute = "exec";
        private const string Print = "print";
        
        private const string VariableDeclaration = "var";
        private const string LocalDeclaration = "local";
        private const string OperationDeclaration = "let";

        private const string If = "if";
        private const string While = "while";

        private const string Equalize = "=";
        private const string Multiply = "*";
        private const string Divide = "/";
        private const string Add = "+";
        private const string Subtract = "-";

        public static bool IsOperator(this string source, out OperationType operation)
        {
            operation = OperationType.None;
            
            switch (source)
            {
               case Equalize:
                   operation = OperationType.Equalize;
                   return true;
               case Multiply:
                   operation = OperationType.Multiply;
                   return true;
               case Divide:
                   operation = OperationType.Divide;
                   return true;
               case Add:
                   operation = OperationType.Add;
                   return true;
               case Subtract:
                   operation = OperationType.Subtract;
                   return true;
            }

            return false;
        }
        
        public static bool IsBlock(this string source, out BlockType statementType)
        {
            statementType = BlockType.None;
            
            switch (source)
            {
                case If :
                    statementType = BlockType.If;
                    return true;
                case While :
                    statementType = BlockType.While;
                    return true;
            }

            return false;
        }
        
        public static bool IsDeclarationToken(this string source, Engine engine, out DeclarationType declarationType)
        {
            declarationType = DeclarationType.None;
            
            if (engine.ContainsInstanceType(source))
            {
                declarationType = DeclarationType.Instance;
                return true;
            }

            switch (source)
            {
                case VariableDeclaration :
                    declarationType = DeclarationType.Variable;
                    return true;
                case OperationDeclaration :
                    declarationType = DeclarationType.Operation;
                    return true;
                case LocalDeclaration :
                    declarationType = DeclarationType.Local;
                    return true;
            }

            return false;
        }
        
        public static bool IsFunction(this string source, out ExecutionType functionType)
        {
            functionType = ExecutionType.None;
            
            switch (source)
            {
                case Print :
                    functionType = ExecutionType.Print;
                    return true;
                case Execute :
                    functionType = ExecutionType.Execute;
                    return true;
            }

            return false;
        }
        
        public static string JoinTokens(this IEnumerable<string> tokens)
        {
            return string.Join(TokenSeparator.ToString(), tokens);
        }
        
        public static string JoinLink(this string from, string to)
        {
            return from + RootSeparator + to;
        }
    }
}