using System.Dynamic;

namespace Calculator.Core.Model.Expressions
{
    public abstract class BinaryOperator : Expression
    {
        protected BinaryOperator(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }
        private Expression _left;
        private Expression _right;

        public void Interpret(Context context)
        {
            if (!tryEvaluateExpression(_left, context, out var leftResultValue))
            {
                return;
            }

            if (!tryEvaluateExpression(_right, context, out var rightResultValue))
            {
                return;
            }
            DoInterpret(context, leftResultValue, rightResultValue);
        }

        protected abstract void DoInterpret(Context context, double leftValue, double rightValue);

        private bool tryEvaluateExpression(Expression expression, Context context, out double result)
        {
            expression.Interpret(context);
            if (!context.Results.TryPop(out var leftResult))
            {
                context.Results.Push((ResultType.Error, "Не удалось получить результат выражения..."));
                result = default;
                return false;
            }
            if (leftResult.Item1 == ResultType.Error)
            {
                context.Results.Push(leftResult);
                result = default;
                return false;
            }
            double resultValue;
            if (!double.TryParse(leftResult.Item2, out resultValue))
            {
                context.Results.Push((ResultType.Error, "Ошибка парсинга результата выражения..."));
                result = default;
                return false;
            }
            result = resultValue;
            return true;
        }
    }
}