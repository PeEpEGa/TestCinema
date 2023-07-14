using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestCinema.Contracts.Database;
using TestCinema.Domain.Base;
using TestCinema.Domain.Database;

namespace TestCinema.Domain.Commands;

public class GetCinemaHallByIdQuery : IRequest<GetCinemaHallByIdQueryResult>
{
    public int CinemaHallId { get; set; }
}

public class GetCinemaHallByIdQueryResult
{
    public CinemaHall CinemaHall;
}

internal class GetCinemaHallByIdQueryHandler : BaseHandler<GetCinemaHallByIdQuery, GetCinemaHallByIdQueryResult>
{
    private readonly CinemaDbContext _dbContext;

    public GetCinemaHallByIdQueryHandler(CinemaDbContext listDbContext, ILogger<GetCinemaHallByIdQueryHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override  async Task<GetCinemaHallByIdQueryResult> HandleInternal(GetCinemaHallByIdQuery request, CancellationToken cancellationToken)
    {
        var cinemaHallId = request.CinemaHallId;

        var cinemaHall = await _dbContext.CinemaHalls.FirstOrDefaultAsync(n => n.CinemaHallId == cinemaHallId);

        if(cinemaHall == null)
        {
            throw new ArgumentException("Incorrect cinemaHall id.");
        }
        return new GetCinemaHallByIdQueryResult
        {
            CinemaHall = cinemaHall
        };
    }
}