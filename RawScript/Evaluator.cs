using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawScript
{
    public class Evaluator
    {
        private readonly Dictionary<int, int> MultiplicationTokens;
        private readonly Dictionary<int, int> DivisionTokens;
        private readonly Dictionary<int, int> DivisionRemainderTokens;
        private readonly Dictionary<int, int> AdditionTokens;
        private readonly Dictionary<int, int> SubtractionTokens;

        private readonly Dictionary<int, int> LargerNumberTokens;
        private readonly Dictionary<int, int> LessNumberTokens;
        private readonly Dictionary<int, int> EqualityTokens;
        
        private readonly Dictionary<int, int> AndTokens;
        private readonly Dictionary<int, int> OrTokens;
        private readonly Dictionary<int, int> XorTokens;

        public static Evaluator Instance;

        public Evaluator()
        {
            MultiplicationTokens = new Dictionary<int, int>();
            DivisionTokens = new Dictionary<int, int>();
            DivisionRemainderTokens = new Dictionary<int, int>();
            AdditionTokens = new Dictionary<int, int>();
            SubtractionTokens = new Dictionary<int, int>(); 
            
            LargerNumberTokens = new Dictionary<int, int>(); 
            LessNumberTokens = new Dictionary<int, int>(); 
            EqualityTokens = new Dictionary<int, int>();
            
            AndTokens = new Dictionary<int, int>(); 
            OrTokens = new Dictionary<int, int>(); 
            XorTokens = new Dictionary<int, int>();

            Instance = this;
        }

        public object Evaluate(string expression)
        {
            var bracketsCount = expression.Count(x => x == Shell.BracketsOpening);
            
            for (var i = 0; i < bracketsCount; i++)
            {
                expression = Trace(expression).ToString();
            }

            return Trace(expression);
        }

        private object Trace(string expression)
        {
            var tokens = Lexer.Separate(expression);
            expression = tokens.JoinTokens();
            
            var bracketCount = 0;
            var bracketBuilder = new StringBuilder();
            var expressionPosition = 0;
            
            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];

                if (tokens[i] == Shell.BracketsOpening.ToString())
                {
                    i++; bracketCount++;

                    var startPosition = expressionPosition;
                    var length = 3;

                    while (bracketCount > 0)
                    {
                        token = tokens[i];

                        if (char.TryParse(token, out var sym))
                        {
                            switch (sym)
                            {
                                case Shell.BracketsOpening:
                                    bracketCount++;
                                    break;
                                case Shell.BracketsClosing:
                                    bracketCount--;
                                    break;
                            }
                        }

                        if (bracketCount > 0)
                        {
                            bracketBuilder.Append(token + Shell.TokenSeparator);
                            length += token.Length + 1;
                        }

                        i++;
                    }

                    return expression.Remove(startPosition, length)
                        .Insert(startPosition, Trace(bracketBuilder.ToString()).ToString());
                }
                
                expressionPosition += token.Length + 1;
            }

            for (var i = 0; i < tokens.Length / 2; i++)
            {
                expression = UnitTrace(expression);
            }
            
            if (bool.TryParse(expression, out var resultAsBoolean))
            {
                return resultAsBoolean;
            }

            if (float.TryParse(expression, out var resultAsSingle))
            {
                return resultAsSingle;
            }
            
            return null;
        }

        private string UnitTrace(string expression)
        {
            var whitespaces = Lexer.Separate(expression);

            MultiplicationTokens.Clear();
            DivisionTokens.Clear();
            AdditionTokens.Clear();
            SubtractionTokens.Clear();
            DivisionRemainderTokens.Clear();
            LargerNumberTokens.Clear();
            LessNumberTokens.Clear();
            EqualityTokens.Clear();
            AndTokens.Clear();
            OrTokens.Clear();
            XorTokens.Clear();

            var currentTokenId = 0;

            for (var i = 0; i < expression.Length; i++)
            {
                var sym = expression[i];
                if (sym == Shell.TokenSeparator)
                {
                    currentTokenId++;
                    continue;
                }

                switch (sym)
                {
                    case '*':
                        MultiplicationTokens.Add(currentTokenId, i);
                        break;
                    case '/':
                        DivisionTokens.Add(currentTokenId, i);
                        break;
                    case '%':
                        DivisionRemainderTokens.Add(currentTokenId, i);
                        break;
                    case '+':
                        AdditionTokens.Add(currentTokenId, i);
                        break;
                    case '-':
                        SubtractionTokens.Add(currentTokenId, i);
                        break;
                    case '>':
                        LargerNumberTokens.Add(currentTokenId, i);
                        break;
                    case '<':
                        LessNumberTokens.Add(currentTokenId, i);
                        break;
                    case '=':
                        EqualityTokens.Add(currentTokenId, i);
                        break;
                    case '&':
                        AndTokens.Add(currentTokenId, i);
                        break;
                    case '|':
                        OrTokens.Add(currentTokenId, i);
                        break;
                    case '^':
                        XorTokens.Add(currentTokenId, i);
                        break;
                }
            }

            var divisionRemainderCheck = EvaluateLocally(Operation.DivisionRemainder, DivisionRemainderTokens, whitespaces, expression);
            if (divisionRemainderCheck != expression)
            {
                return divisionRemainderCheck;
            }

            var multiplicationCheck = EvaluateLocally(Operation.Multiplication, MultiplicationTokens, whitespaces, expression);
            if (multiplicationCheck != expression)
            {
                return multiplicationCheck;
            }

            var divisionCheck = EvaluateLocally(Operation.Division, DivisionTokens, whitespaces, expression);
            if (divisionCheck != expression)
            {
                return divisionCheck;
            }

            var additionCheck = EvaluateLocally(Operation.Addition, AdditionTokens, whitespaces, expression);
            if (additionCheck != expression)
            {
                return additionCheck;
            }

            var subtractionCheck = EvaluateLocally(Operation.Subtraction, SubtractionTokens, whitespaces, expression);
            if (subtractionCheck != expression)
            {
                return subtractionCheck;
            }
            
            var largerNumberCheck = EvaluateLocally(Operation.LargerThan, LargerNumberTokens, whitespaces, expression);
            if (largerNumberCheck != expression)
            {
                return largerNumberCheck;
            }
            
            var lessNumberCheck = EvaluateLocally(Operation.LessThan, LessNumberTokens, whitespaces, expression);
            if (lessNumberCheck != expression)
            {
                return lessNumberCheck;
            }
            
            var equalityCheck = EvaluateLocally(Operation.Equals, EqualityTokens, whitespaces, expression);
            if (equalityCheck != expression)
            {
                return equalityCheck;
            }
            
            var andCheck = EvaluateLocally(Operation.And, AndTokens, whitespaces, expression);
            if (andCheck != expression)
            {
                return andCheck;
            }
            
            var orCheck = EvaluateLocally(Operation.Or, OrTokens, whitespaces, expression);
            if (orCheck != expression)
            {
                return orCheck;
            }
            
            var xorCheck = EvaluateLocally(Operation.Xor, XorTokens, whitespaces, expression);
            return xorCheck != expression ? xorCheck : expression;
        }

        private static string EvaluateLocally(Operation operation, IReadOnlyDictionary<int, int> tokens, IReadOnlyList<string> whitespaces,
            string expression)
        {
            foreach (var token in tokens)
            {
                var a = whitespaces[token.Key - 1];
                var b = whitespaces[token.Key + 1];
                var length = a.Length + b.Length + 3;

                var tokenPosition = token.Value - a.Length - 1;

                var clearedExpression = expression.Remove(tokenPosition, length);

                if (float.TryParse(a, out var aTestA) && float.TryParse(b, out var bTestA))
                {
                    return GetLocalResult(operation, clearedExpression, tokenPosition, aTestA, bTestA);
                }
                
                if (bool.TryParse(a, out var aTestB) && bool.TryParse(b, out var bTestB))
                {
                    return GetLocalResult(operation, clearedExpression, tokenPosition, aTestB, bTestB);
                }
            }

            return expression;
        }

        private static string GetLocalResult(Operation operation, string expression, int tokenPosition, object a, object b)
        {
            return expression.Insert(tokenPosition, EvaluateOperation(operation, a, b).ToString());
        }
        
        private static object EvaluateOperation(Operation operation, object a, object b)
        {
            switch (operation)
            {
                /*
                 * Math operations
                 */
                case Operation.Multiplication:
                    return (float) a * (float) b;
                case Operation.Division:
                    return (float) a / (float) b;
                case Operation.DivisionRemainder:
                    return (float) a % (float) b;
                case Operation.Addition:
                    return (float) a + (float) b;
                case Operation.Subtraction:
                    return (float) a - (float) b;
                /*
                 * Comparison operations
                 */
                case Operation.LargerThan:
                    return (float) a > (float) b;
                case Operation.LessThan:
                    return (float) a < (float) b;
                case Operation.Equals:
                    return a.Equals(b);
                
                /*
                 * Logic operations
                 */
                case Operation.And:
                    return (bool) a && (bool) b;
                case Operation.Or:
                    return (bool) a || (bool) b;
                case Operation.Xor:
                    return (bool) a ^ (bool) b;

                default:
                    return 0;
            }
            
            throw new Exception("Unknown operation");
        }

        private enum Operation
        {
            Multiplication,
            Division,
            Addition,
            Subtraction,
            DivisionRemainder,
            And,
            Or,
            Xor,
            LargerThan,
            LessThan,
            Equals
        }
    }
}