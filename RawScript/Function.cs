using System;
using System.Collections.Generic;
using RawScript.Statements;

namespace RawScript
{
    public delegate bool Condition(Dictionary<string, object> variables);
    
    public class Function
    {
        protected readonly List<IInvokable> invokables;

        public Function(Dictionary<string, object> variables, string source)
        {
            invokables = new List<IInvokable>();
            Trace(ref variables, source);
        }

        protected void Trace(ref Dictionary<string, object> variables, string source)
        {
            var tokens = Lexer.Separate(source);
            var bracketsCount = 0;

            var evaluator = Evaluator.Instance;
            var terminal = Output.Instance;
            var engine = Engine.Instance;

            for (var tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex++)
            {
                var token = tokens[tokenIndex];

                if (token.IsFunction(out var functionType))
                {
                    var functionTokens = new List<string>();
                    tokenIndex++;
                    token = tokens[tokenIndex];
                    while (token != Shell.DeclarationSeparator)
                    {
                        functionTokens.Add(token);
                        token = tokens[++tokenIndex];
                    }
                    var output = functionTokens.JoinTokens();

                    switch (functionType)
                    {
                        case FunctionType.Print:

                            invokables.Add(new Operation(ref variables, inputVariables =>
                            {
                                if (evaluator != null)
                                {
                                    terminal?.Print(evaluator.Evaluate(ReplacePlaceholders(inputVariables, output)));
                                }
                            }));
                            break;
                        case FunctionType.Use:
                            
                            foreach (var function in functionTokens)
                            {
                                invokables.Add(new Operation(ref variables, inputVariables =>
                                {
                                    engine.GetFunction(function).Invoke();
                                }));
                            }
                            break;
                    }
                }

                if (token == Shell.BeginStatement)
                {
                    var beginingIndex = tokenIndex - 1;
                    token = tokens[beginingIndex];

                    StatementType statementType;
                    var conditionTokens = new List<string>();
                    
                    while (!token.IsStatement(out statementType))
                    {
                        conditionTokens.Add(token);
                        token = tokens[--beginingIndex];
                    }

                    conditionTokens.Reverse();
                    
                    var condition = conditionTokens.JoinTokens();

                    bracketsCount++;
                    tokenIndex++;

                    var internalData = new List<string>();
                    
                    while (bracketsCount > 0)
                    {
                        token = tokens[tokenIndex];
                        
                        switch (token)
                        {
                            case Shell.BeginStatement:
                                bracketsCount++;
                                break;
                            case Shell.EndStatement:
                                bracketsCount--; 
                                break;
                        }

                        if (bracketsCount > 0)
                        {
                            internalData.Add(token);
                        }
                        
                        tokenIndex++;
                    }

                    tokenIndex--;
                    token = tokens[tokenIndex];

                    var combinedInternalData = internalData.JoinTokens();
                    
                    bool ConditionCheck(IReadOnlyDictionary<string, object> inputVariables)
                    {
                        if (evaluator != null)
                        {
                            return (bool) evaluator.Evaluate(ReplacePlaceholders(inputVariables, condition));
                        }

                        return false;
                    }
                    
                    switch (statementType)
                    {
                        case StatementType.If:
                            var ifStatementInstance = new If(ref variables, ConditionCheck, combinedInternalData);
                            invokables.Add(ifStatementInstance);
                            break;
                        case StatementType.While:
                            var whileStatementInstance = new While(ref variables, ConditionCheck, combinedInternalData);
                            invokables.Add(whileStatementInstance);
                            break;
                    }
                }

                if (token.IsDeclarationToken(out var declarationType))
                {
                    var declarationTokens = new List<string>();
                    
                    while (token != Shell.DeclarationSeparator)
                    {
                        declarationTokens.Add(token);
                        token = tokens[++tokenIndex];
                    }

                    int localIndex;
                    
                    switch (declarationType)
                    {
                        case DeclarationType.Variable:
                            for (localIndex = 1; localIndex < declarationTokens.Count; localIndex++)
                            {
                                var localName = declarationTokens[localIndex];
                                variables.Add(localName, new object());
                            }
                            break;
                        case DeclarationType.Operation:
                            var locals = new List<string>();
                            localIndex = 1;
                            var localPlaceholder = declarationTokens[localIndex];

                            OperationType operation;
                            
                            while (!localPlaceholder.IsOperator(out operation))
                            {
                                locals.Add(localPlaceholder);
                                localPlaceholder = declarationTokens[++localIndex];
                            }
                            
                            var expressionTokens = new List<string>();
                            
                            for (localIndex++; localIndex < declarationTokens.Count; localIndex++)
                            {
                                expressionTokens.Add(declarationTokens[localIndex]);
                            }

                            var expression = expressionTokens.JoinTokens();

                            foreach (var local in locals)
                            {
                                invokables.Add(new Operation(ref variables, TraceOperation(local, expression, operation))); 
                            }
                            break;
                    }
                }
            }
        }

        private static OperationFunction TraceOperation(string local, string expression, OperationType operation)
        {
            var evaluator = Evaluator.Instance;

            if (evaluator is null)
            {
                throw new Exception("Evaluator should be initialized before expression parsing");
            }

            switch (operation)
            {
                case OperationType.Equalize:
                    return inputVariables =>
                    {
                        expression = ReplacePlaceholders(inputVariables, expression);
                        inputVariables[local] = evaluator.Evaluate(expression);
                    };
                case OperationType.Multiply:
                    return inputVariables =>
                    {
                        expression = ReplacePlaceholders(inputVariables, expression);
                        inputVariables[local] = (float) inputVariables[local] * (float) evaluator.Evaluate(expression);
                    };
                case OperationType.Divide:
                    return inputVariables =>
                    {
                        expression = ReplacePlaceholders(inputVariables, expression);
                        inputVariables[local] = (float) inputVariables[local] / (float) evaluator.Evaluate(expression);
                    };
                case OperationType.Add:
                    return inputVariables =>
                    {
                        expression = ReplacePlaceholders(inputVariables, expression);
                        inputVariables[local] = (float) inputVariables[local] + (float) evaluator.Evaluate(expression);
                    };
                case OperationType.Subtract:
                    return inputVariables =>
                    {
                        expression = ReplacePlaceholders(inputVariables, expression);
                        inputVariables[local] = (float) inputVariables[local] - (float) evaluator.Evaluate(expression);
                    };
            }

            return null;
        }

        private static string ReplacePlaceholders(IReadOnlyDictionary<string, object> inputVariables, string source)
        {
            var inputTokens = Lexer.Separate(source);
            var resultTokens = new List<string>();
            
            foreach (var token in inputTokens)
            {
                if (inputVariables.ContainsKey(token))
                {
                    resultTokens.Add(inputVariables[token].ToString());
                    continue;
                }
                            
                resultTokens.Add(token);
            }
            
            return resultTokens.JoinTokens();
        }

        public virtual void Invoke()
        {
            foreach (var invokable in invokables)
            {
                invokable.Invoke();
            }
        }
    }
}