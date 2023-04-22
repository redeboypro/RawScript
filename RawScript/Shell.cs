using System.Collections.Generic;

namespace RawScript
{
    public enum StatementType
    {
        If,
        While,
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
        
        public const string VariableDeclaration = "var";
        public const string OperationDeclaration = "let";
        
        public const string If = "if";
        public const string While = "while";
        
        public const string Equalize = "=";
        public const string Multiply = "*";
        public const string Divide = "/";
        public const string Add = "+";
        public const string Subtract = "-";

        public static bool IsOperator(string source, out OperationType operation)
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
        
        public static string JoinTokens(this IEnumerable<string> tokens)
        {
            return string.Join(TokenSeparator.ToString(), tokens);
        }
    }
}