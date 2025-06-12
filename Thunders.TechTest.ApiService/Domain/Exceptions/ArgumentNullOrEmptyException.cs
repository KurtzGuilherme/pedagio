namespace Thunders.TechTest.ApiService.Domain.Exceptions;

public class ArgumentNullOrEmptyException : ArgumentException
{
    public ArgumentNullOrEmptyException(string paramName)
            : base(ApplicationMessages.ArgumentCannotNullEmpty, paramName)
    { }
}
