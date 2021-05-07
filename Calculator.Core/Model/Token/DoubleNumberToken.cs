using System;
using System.Linq;

namespace Calculator.Core.Model.Token
{
    public class DoubleNumberToken : Token
    {
        public DoubleNumberToken(double value)
        {
            Value = value;
        }

        public double Value { get; }
        public int Priority => 10;
    }

    public class DoubleNumberTokenParser : TokenParser
    {
        public bool TryParseBeginningsOfTheString(string input, out Token token, out string restOfInput)
        {
            var numberChars = input.TakeWhile(c => Char.IsDigit(c) || c == ',');
            string numberString = String.Concat(numberChars);
            int numberStringLength = numberString.Length;
            if (numberStringLength == 0)
            {
                token = null;
                restOfInput = input;
                return false;
            }

            if (!double.TryParse(numberString, out double parsedTokeValue))
            {
                token = null;
                restOfInput = input;
                return false;
            }

            token = new DoubleNumberToken(parsedTokeValue);
            restOfInput = String.Concat(input.Skip(numberStringLength));
            return true;
        }
    }
}