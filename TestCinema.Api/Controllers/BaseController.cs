using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TestCinema.Contracts.Http;
using TestCinema.Domain.Exceptions;

namespace TestCinema.Api.Controllers;

public class BaseController : ControllerBase
{
    private readonly ILogger _logger;

    protected BaseController(ILogger logger)
    {
        _logger = logger;
    }

    protected async Task<IActionResult> SafeExecute(Func<Task<IActionResult>> action, CancellationToken cancellationToken)
    {
        try
        {
           return await action(); 
        }
        catch(CinemaException le)
        {
            var response = new ErrorResponse
            {
                Code = le.ErrorCode,
                Message = le.Message
            };
            return ToActionResult(response);
        }
        catch (InvalidOperationException ioe) when (ioe.InnerException is NpgsqlException)
        {
            var response = new ErrorResponse
            {
                Code = ErrorCode.DbFailureError,
                Message = "DB failure"
            };
            return ToActionResult(response);
        }
        catch (System.Exception)
        {
            var response = new ErrorResponse
            {
                Code = ErrorCode.InternalServerError,
                Message = "Unhandled error"
            };
            return ToActionResult(response);
        }
    }

    protected IActionResult ToActionResult(ErrorResponse errorResponse)
    {
        return StatusCode((int)errorResponse.Code / 100, errorResponse);
    }
}