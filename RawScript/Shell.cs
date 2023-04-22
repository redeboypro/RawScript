using System.Collections.Generic;

namespace RawScript
{
    public enum StatementType
    {
        If,
        While,
        None
    }
    
    public enum FunctionType
    {
        Print,
        Use,
        None
    }

    public enum DeclarationType
    {
        Variable,
        Operation,
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
    
    public delegate void OperationFunction(Dictionary<string, object> variables);
    
    public static class Shell
    {
        public const char TokenSeparator = ' ';
        public const string DeclarationSeparator = ";";

        public const char BracketsOpening = '(';
        public const char BracketsClosing = ')';

        public const string BeginStatement = "{";
        public const string EndStatement = "}";
        
        private const string Use = "use";
        private const string Print = "print";
        
        private const string VariableDeclaration = "var";
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
        
        public static bool IsStatement(this string source, out StatementType statementType)
        {
            statementType = StatementType.None;
            
            switch (source)
            {
                case If :
                    statementType = StatementType.If;
                    return true;
                case While :
                    statementType = StatementType.While;
                    return true;
            }

            return false;
        }
        
        public static bool IsDeclarationToken(this string source, out DeclarationType declarationType)
        {
            declarationType = DeclarationType.None;
            
            switch (source)
            {
                case VariableDeclaration :
                    declarationType = DeclarationType.Variable;
                    return true;
                case OperationDeclaration :
                    declarationType = DeclarationType.Operation;
                    return true;
            }

            return false;
        }
        
        public static bool IsFunction(this string source, out FunctionType functionType)
        {
            functionType = FunctionType.None;
            
            switch (source)
            {
                case Print :
                    functionType = FunctionType.Print;
                    return true;
                case Use :
                    functionType = FunctionType.Use;
                    return true;
            }

            return false;
        }
        
        public static string JoinTokens(this IEnumerable<string> tokens)
        {
            return string.Join(TokenSeparator.ToString(), tokens);
        }
    }
}