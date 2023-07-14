using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCinema.Contracts.Database;

[Table("tbl_cinemaHalls", Schema = "public")]
public class CinemaHall
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int CinemaHallId { get; set; }

    [ForeignKey(nameof(Cinema))]
    [Column("cinema_id")]
    public int CinemaId { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(250)]
    public string CinemaHallName { get; set; }

    [Required]
    [Column("total_seats")]
    public int TotalSeats { get; set; }
}