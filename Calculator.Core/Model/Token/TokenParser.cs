namespace Calculator.Core.Model.Token
{
    public interface TokenParser
    {
        bool TryParseBeginningsOfTheString(
            string input,
            out Token token,
            out string restOfInput
        );
        
        
    }
}