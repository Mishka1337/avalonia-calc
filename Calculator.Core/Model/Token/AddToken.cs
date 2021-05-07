using System.Runtime.CompilerServices;
using Calculator.Core.Model.Expressions;

namespace Calculator.Core.Model.Token
{
    public class AddToken : BinaryToken
    {
        public int Priority => 1;
        public BinaryOperator CreateExpression(Expression left, Expression right)
        {
            return new AddExpression(left, right);
        }
    }
    
    public class AddTokenParser : TokenParser
    {
        public bool TryParseBeginningsOfTheString(string input, out Token token, out string restOfInput)
        {
            if (!input.StartsWith("+"))
            {
                token = null;
                restOfInput = input;
                return false;
            }

            token = new AddToken();
            restOfInput = input.Remove(0,1);
            return true;
        }
    }
}