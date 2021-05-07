using System.Collections.Generic;

namespace Calculator.Core.Model
{
    public class Context
    {
        public Stack<(ResultType, string)> Results { get; } = new();
    }
}