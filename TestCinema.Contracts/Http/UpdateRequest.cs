public class UpdateCinemaRequest
{
    public int Id { get; set; }
    public string Name { get; init; }
    public int TotalHalls { get; set; }
    public int LocationId { get; set; }
}

public class UpdateCinemaResponse
{
    public int CinemaId { get; set; }
}