using Calculator.Core.Model.Expressions;

namespace Calculator.Core.Model.Token
{
    public class MultiplyToken : BinaryToken
    {
        public int Priority => 5;
        public BinaryOperator CreateExpression(Expression left, Expression right)
        {
            return new MultiplyExpression(left,right);
        }
    }

    public class MultiplyTokenParser : TokenParser
    {
        public bool TryParseBeginningsOfTheString(string input, out Token token, out string restOfInput)
        {
            if (!input.StartsWith("*"))
            {
                token = null;
                restOfInput = input;
                return false;
            }

            token = new MultiplyToken();
            restOfInput = input.Remove(0,1);
            return true;
        }
    }
}