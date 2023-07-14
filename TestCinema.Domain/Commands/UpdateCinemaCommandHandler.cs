using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestCinema.Contracts.Database;
using TestCinema.Domain.Base;
using TestCinema.Domain.Database;

namespace TestCinema.Domain.Commands;

public class UpdateCinemaCommand : IRequest<UpdateCinemaCommandResult>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public int TotalHalls { get; set; }
    public int LocationId { get; set; }
}

public class UpdateCinemaCommandResult
{
    public Cinema Cinema;
}

internal class UpdateCinemaCommandHandler : BaseHandler<UpdateCinemaCommand, UpdateCinemaCommandResult>
{
    private readonly CinemaDbContext _dbContext;

    public UpdateCinemaCommandHandler(CinemaDbContext listDbContext, ILogger<UpdateCinemaCommandHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override async Task<UpdateCinemaCommandResult> HandleInternal(UpdateCinemaCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var cinemaToUpdate = await _dbContext.Cinemas.SingleOrDefaultAsync(n => n.CinemaId == id);

        if(cinemaToUpdate == null)
        {
            throw new ArgumentException("Incorrect cinema id.");
        }

        cinemaToUpdate.CinemaName = request.Name;
        cinemaToUpdate.TotalHalls = request.TotalHalls;
        cinemaToUpdate.LocationId = request.LocationId;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateCinemaCommandResult
        {
            Cinema = cinemaToUpdate
        };
    }
}