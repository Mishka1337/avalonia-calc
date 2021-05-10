using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Core.Model.Expressions;
using Calculator.Core.Model.Token;

namespace Calculator.Core
{
    public class ExpressionParser
    {
        private List<TokenParser> _tokenParsers = typeof(ExpressionParser)
            .Assembly
            .GetTypes()
            .Where(t =>
                t.IsClass 
                && !t.IsAbstract 
                && typeof(TokenParser).IsAssignableFrom(t))
            .Select(t => (TokenParser) Activator.CreateInstance(t))
            .ToList();
            
        public Expression Parse(string input)
        {
            var formattedInput = input.Replace(" ", string.Empty);
            var tokens = ParserStringToListOfTokens(input);
            var expression = FormExpressionFromTokenList(tokens);
            return expression;
        }

        private List<Token> ParserStringToListOfTokens(string input)
        {
            string parsingString = input;
            List<Token> returnValue = new();
            while (!String.IsNullOrEmpty(parsingString))
            {
                bool isParsed = false;
                Token token;
                string restOfInput;
                foreach (TokenParser tokenParser in _tokenParsers)
                {
                    if (tokenParser.TryParseBeginningsOfTheString(
                        parsingString,
                        out token,
                        out restOfInput))
                    {
                        returnValue.Add(token);
                        parsingString = restOfInput;
                        isParsed = true;
                        break;
                    }
                }

                if (!isParsed)
                {
                    return new List<Token>();
                } 
            }
            return returnValue;
        }

        private Expression FormExpressionFromTokenList(List<Token> tokens)
        {
            if (tokens.Count == 0)
            {
                return null;
            }
            Dictionary<Token, Expression> tokenToExpressionMap = new Dictionary<Token, Expression>();
            foreach (var token in tokens)
            {
                if (token is DoubleNumberToken numberToken)
                {
                    tokenToExpressionMap.Add(token, new ConstExpression(numberToken.Value));
                }
                else
                {
                    tokenToExpressionMap.Add(token, null);
                }
            }

            int distinctExpressions = tokenToExpressionMap
                .Select(pair => pair.Value)
                .Distinct()
                .Count();
            while (distinctExpressions != 1)
            {
                var tokensWithoutExpression = tokens
                    .Where(t => tokenToExpressionMap[t] == null);
                if (!tokensWithoutExpression.Any())
                {
                    return null;
                }
                var token = tokensWithoutExpression
                    .Aggregate((t,ta) => ta.Priority > t.Priority ? ta : t);

                var tokenIndex = tokens.IndexOf(token);
                if (tokenIndex == 0
                    && tokenIndex == tokens.Count - 1)
                {
                    return null;
                }

                var leftExpression = tokenToExpressionMap[tokens[tokenIndex - 1]];
                var rightExpression = tokenToExpressionMap[tokens[tokenIndex + 1]];
                if (leftExpression != null
                    && rightExpression != null
                    && token is BinaryToken binaryToken)
                {
                    Expression newExpression = binaryToken.CreateExpression(leftExpression, rightExpression);
                    if (newExpression == null)
                    {
                        return null;
                    }
                    tokenToExpressionMap[token] = newExpression;
                    foreach (var keyToken in tokenToExpressionMap.Keys)
                    {
                        if (rightExpression == tokenToExpressionMap[keyToken]
                            || leftExpression == tokenToExpressionMap[keyToken])
                            tokenToExpressionMap[keyToken] = newExpression;
                    }
                    int newDistinctExpressions = tokenToExpressionMap
                        .Select(pair => pair.Value)
                        .Distinct()
                        .Count();
                    if (newDistinctExpressions == distinctExpressions)
                    {
                        return null;
                    }
                    distinctExpressions = newDistinctExpressions;
                }
                else
                {
                    return null;
                }
            }
            return tokenToExpressionMap.First().Value;
        }
    }
}
