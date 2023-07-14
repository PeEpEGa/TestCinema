namespace TestCinema.Contracts.Http;

//Cinema
public class CreateCinemaRequest
{
    public string Name { get; init; }
    public int TotalHalls { get; set; }
    public int LocationId { get; set; }
}

public class CreateCinemaResponse
{
    public int CinemaId { get; set; }
}


//CinemaHall
public class CreateCinemaHallRequest
{
    public int CinemaId { get; set; }
    public string Name { get; init; }
    public int TotalSeats { get; set; }
}

public class CreateCinemaHallResponse
{
    public int CinemaHallId { get; set; }
}


//CinemaHallSeat
public class CreateCinemaHallSeatRequest
{
    public int CinemaHallId { get; set; }
    public int SeatRow { get; set; }
    public int SeatColumn { get; set; }
    public decimal Price { get; set; }
}

public class CreateCinemaHallSeatResponse
{
    public int CinemaHallSeatId { get; set; }
}