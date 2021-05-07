using Calculator.Core.Model.Expressions;

namespace Calculator.Core.Model.Token
{
    public class DivideToken : BinaryToken
    {
        public int Priority => 5;
        public BinaryOperator CreateExpression(Expression left, Expression right)
        {
            return new DivideExpression(left, right);
        }
    }
    
    public class DivideTokenParser : TokenParser
    {
        public bool TryParseBeginningsOfTheString(string input, out Token token, out string restOfInput)
        {
            if (!input.StartsWith("/"))
            {
                token = null;
                restOfInput = input;
                return false;
            }

            token = new DivideToken();
            restOfInput = input.Remove(0,1);
            return true;
        }
    }
}