using System;
using Calculator.Core.Model;
using Calculator.Core.Model.Expressions;

namespace Calculator.Core
{
    public class Interpreter
    {
        public string Interpret(Expression rootExpression)
        {
            var context = new Context();
            try
            {
                rootExpression.Interpret(context);
                if (!context.Results.TryPop(out var result))
                {
                    return "";
                }
                return result.Item2;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        
    }
}