namespace Bleeter.Shared.Exceptions;

public class ValidatorException : Exception
{
    public Dictionary<string, List<string>> Errors;
    
    public ValidatorException(Dictionary<string,List<string>> errors)
    {
        Errors = errors;
    }
}