using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCinema.Contracts.Database;

[Table("tbl_cinemas", Schema = "public")]
public class Cinema
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int CinemaId { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(300)]
    public string CinemaName { get; set; }

    [Required]
    [Column("total_halls")]
    public int TotalHalls { get; set; }

    [Required]
    [Column("location_id")]
    public int LocationId { get; set; }
}