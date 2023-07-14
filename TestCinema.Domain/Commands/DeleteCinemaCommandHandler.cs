using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestCinema.Contracts.Database;
using TestCinema.Domain.Base;
using TestCinema.Domain.Database;

namespace TestCinema.Domain.Commands;

public class DeleteCinemaCommand : IRequest<DeleteCinemaCommandResult>
{
    public int Id { get; set; }
}

public class DeleteCinemaCommandResult
{
    public int CinemaId;
}

internal class DeleteCinemaCommandHandler : BaseHandler<DeleteCinemaCommand, DeleteCinemaCommandResult>
{
    private readonly CinemaDbContext _dbContext;

    public DeleteCinemaCommandHandler(CinemaDbContext listDbContext, ILogger<DeleteCinemaCommandHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override async Task<DeleteCinemaCommandResult> HandleInternal(DeleteCinemaCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var cinemaToDelete = await _dbContext.Cinemas.SingleOrDefaultAsync(n => n.CinemaId == id);

        if(cinemaToDelete == null)
        {
            throw new ArgumentException("Incorrect cinema id.");
        }

        _dbContext.Cinemas.Remove(cinemaToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteCinemaCommandResult
        {
            CinemaId = cinemaToDelete.CinemaId
        };
    }
}