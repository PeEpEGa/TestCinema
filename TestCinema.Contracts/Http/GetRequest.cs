using TestCinema.Contracts.Database;

public class GetAllCinemasRequest
{
}

public class GetAllCinemasResponse
{
    public List<Cinema> Cinemas { get; set; }
}


public class GetCinemaHallByIdRequest
{
    public int CinemaHallId { get; set; }
}

public class GetCinemaHallByIdResponse
{
    public CinemaHall CinemaHall { get; set; }
}