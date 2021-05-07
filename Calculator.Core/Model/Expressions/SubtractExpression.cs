namespace Calculator.Core.Model.Expressions
{
    public class SubtractExpression : BinaryOperator
    {
        public SubtractExpression(Expression left, Expression right) : base(left, right)
        {
        }
        
        protected override void DoInterpret(Context context, double leftValue, double rightValue)
        {
            context.Results.Push((ResultType.Success, (leftValue - rightValue).ToString()));
        }
    }
}