namespace Calculator.Core.Model.Expressions
{
    public class MultiplyExpression : BinaryOperator
    {
        public MultiplyExpression(Expression left, Expression right) : base(left, right)
        {
        }

        protected override void DoInterpret(Context context, double leftValue, double rightValue)
        {
            context.Results.Push((ResultType.Success, (leftValue * rightValue).ToString()));
        }
    }
}