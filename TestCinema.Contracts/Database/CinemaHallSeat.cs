using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCinema.Contracts.Database;

[Table("tbl_cinemaHallSeats", Schema = "public")]
public class CinemaHallSeat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int CinemaHallSeatId { get; set; }

    [ForeignKey(nameof(CinemaHall))]
    [Column("cinemaHall_id")]
    public int CinemaHallId { get; set; }

    [Required]
    [Column("seat_row")]
    public int SeatRow { get; set; }

    [Required]
    [Column("seat_column")]
    public int SeatColumn { get; set; }

    [Required]
    [Column("is_reserverd")]
    public bool IsReserved { get; set; }

    [Required]
    [Column("price")]
    public decimal Price { get; set; }
}