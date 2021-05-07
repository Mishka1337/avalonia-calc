namespace Calculator.Core.Model.Expressions
{
    public interface Expression
    {
        void Interpret(Context context);
    }
}