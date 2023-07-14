using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestCinema.Contracts.Database;
using TestCinema.Domain.Base;
using TestCinema.Domain.Database;

namespace TestCinema.Domain.Commands;

public class AddCinemaHallCommand : IRequest<AddCinemaHallCommandResult>
{
    public int CinemaId { get; set; }
    public string Name { get; init; }
    public int TotalSeats { get; set; }
}

public class AddCinemaHallCommandResult
{
    public CinemaHall CinemaHall;
}

internal class AddCinemaHallCommandHandler : BaseHandler<AddCinemaHallCommand, AddCinemaHallCommandResult>
{
    private readonly CinemaDbContext _dbContext;

    public AddCinemaHallCommandHandler(CinemaDbContext listDbContext, ILogger<AddCinemaHallCommandHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override async Task<AddCinemaHallCommandResult> HandleInternal(AddCinemaHallCommand request, CancellationToken cancellationToken)
    {
        var cinemaId = request.CinemaId;
        var cinemaToAdd = await _dbContext.Cinemas.SingleOrDefaultAsync(n => n.CinemaId == cinemaId, cancellationToken);
        if (cinemaToAdd == null)
        {
            throw new ArgumentException("Incorrect cinema id.");
        }

        var cinemaHall = new CinemaHall
        {
            CinemaId = request.CinemaId,
            CinemaHallName = request.Name,
            TotalSeats = request.TotalSeats
        };

        await _dbContext.CinemaHalls.AddAsync(cinemaHall, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddCinemaHallCommandResult
        {
            CinemaHall = cinemaHall
        };
    }
}