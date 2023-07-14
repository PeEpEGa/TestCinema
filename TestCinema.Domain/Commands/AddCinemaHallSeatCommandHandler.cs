using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestCinema.Contracts.Database;
using TestCinema.Domain.Base;
using TestCinema.Domain.Database;

namespace TestCinema.Domain.Commands;

public class AddCinemaHallSeatCommand : IRequest<AddCinemaHallSeatCommandResult>
{
    public int CinemaHallId { get; set; }
    public int SeatRow { get; set; }
    public int SeatColumn { get; set; }
    public decimal Price { get; set; }
}

public class AddCinemaHallSeatCommandResult
{
    public CinemaHallSeat CinemaHallSeat;
}

internal class AddCinemaHallSeatCommandHandler : BaseHandler<AddCinemaHallSeatCommand, AddCinemaHallSeatCommandResult>
{
    private readonly CinemaDbContext _dbContext;

    public AddCinemaHallSeatCommandHandler(CinemaDbContext listDbContext, ILogger<AddCinemaHallSeatCommandHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override async Task<AddCinemaHallSeatCommandResult> HandleInternal(AddCinemaHallSeatCommand request, CancellationToken cancellationToken)
    {
        var cinemaHallToAdd = await _dbContext.CinemaHalls.SingleOrDefaultAsync(n => n.CinemaHallId == request.CinemaHallId);
        if (cinemaHallToAdd == null)
        {
            throw new ArgumentException("Incorrect cinemaHall id.");
        }

        var cinemaHallSeat = new CinemaHallSeat
        {
            CinemaHallId = request.CinemaHallId,
            IsReserved = false,
            SeatColumn = request.SeatColumn,
            SeatRow = request.SeatRow,
            Price = request.Price
        };

        await _dbContext.CinemaSeats.AddAsync(cinemaHallSeat, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddCinemaHallSeatCommandResult
        {
            CinemaHallSeat = cinemaHallSeat
        };
    }
}