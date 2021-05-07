using System.Globalization;

namespace Calculator.Core.Model.Expressions
{
    public class DivideExpression : BinaryOperator
    {
        public DivideExpression(Expression left, Expression right) : base(left, right)
        {
        }

        protected override void DoInterpret(Context context, double leftValue, double rightValue)
        {
            context.Results.Push((ResultType.Success, (leftValue / rightValue).ToString()));
        }
    }
}