using Calculator.Core.Model.Expressions;

namespace Calculator.Core.Model.Token
{
    public class SubtractToken : BinaryToken
    {
        public int Priority => 1;
        public BinaryOperator CreateExpression(Expression left, Expression right)
        {
            return new SubtractExpression(left, right);
        }
    }

    public class SubtractTokenParser : TokenParser
    {
        public bool TryParseBeginningsOfTheString(string input, out Token token, out string restOfInput)
        {
            if (!input.StartsWith("-"))
            {
                token = null;
                restOfInput = input;
                return false;
            }

            token = new SubtractToken();
            restOfInput = input.Remove(0,1);
            return true;
        }
    }
}