using TestCinema.Contracts.Http;

namespace TestCinema.Domain.Exceptions;

public class CinemaException : Exception
{
    public ErrorCode ErrorCode { get;}

    public CinemaException(ErrorCode errorCode) : this(errorCode, null)
    {
    }

    public CinemaException(ErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}