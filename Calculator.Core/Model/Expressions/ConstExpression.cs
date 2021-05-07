namespace Calculator.Core.Model.Expressions
{
    public class ConstExpression : Expression
    {
        private double _constValue;
        public void Interpret(Context context)
        {
            context.Results.Push((ResultType.Success, _constValue.ToString()));
        }

        public ConstExpression(double constValue)
        {
            _constValue = constValue;
        }
    }
}