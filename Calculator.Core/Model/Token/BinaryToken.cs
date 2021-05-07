
using Calculator.Core.Model.Expressions;

namespace Calculator.Core.Model.Token
{
    public interface BinaryToken : Token
    {
        BinaryOperator CreateExpression(Expression left, Expression right);
    }
}