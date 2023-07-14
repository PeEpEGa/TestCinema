using MediatR;
using Microsoft.Extensions.Logging;
using TestCinema.Contracts.Database;
using TestCinema.Domain.Base;
using TestCinema.Domain.Database;

namespace TestCinema.Domain.Commands;

public class AddCinemaCommand : IRequest<AddCinemaCommandResult>
{
    public string Name { get; init; }
    public int TotalHalls { get; set; }
    public int LocationId { get; set; }
}

public class AddCinemaCommandResult
{
    public Cinema Cinema;
}

internal class AddCinemaCommandHandler : BaseHandler<AddCinemaCommand, AddCinemaCommandResult>
{
    private readonly CinemaDbContext _dbContext;

    public AddCinemaCommandHandler(CinemaDbContext listDbContext, ILogger<AddCinemaCommandHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override async Task<AddCinemaCommandResult> HandleInternal(AddCinemaCommand request, CancellationToken cancellationToken)
    {
        var cinema = new Cinema
        {
            CinemaName = request.Name,
            TotalHalls = request.TotalHalls,
            LocationId = request.LocationId
        };

        await _dbContext.Cinemas.AddAsync(cinema, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddCinemaCommandResult
        {
            Cinema = cinema
        };
    }
}