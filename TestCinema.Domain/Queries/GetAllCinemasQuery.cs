using MediatR;
using Microsoft.Extensions.Logging;
using TestCinema.Contracts.Database;
using TestCinema.Domain.Base;
using TestCinema.Domain.Database;

namespace TestCinema.Domain.Commands;

public class GetAllCinemasQuery : IRequest<GetAllCinemasQueryResult>
{
}

public class GetAllCinemasQueryResult
{
    public List<Cinema> Cinemas;
}

internal class GetAllCinemasQueryHandler : BaseHandler<GetAllCinemasQuery, GetAllCinemasQueryResult>
{
    private readonly CinemaDbContext _dbContext;

    public GetAllCinemasQueryHandler(CinemaDbContext listDbContext, ILogger<GetAllCinemasQueryHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override Task<GetAllCinemasQueryResult> HandleInternal(GetAllCinemasQuery request, CancellationToken cancellationToken)
    {
        var cinemas = _dbContext.Cinemas.Select(n => n).ToList();

        return Task.FromResult(new GetAllCinemasQueryResult
        {
            Cinemas = cinemas
        });
    }
}