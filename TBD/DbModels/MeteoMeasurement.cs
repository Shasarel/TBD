using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD.DbModels
{
    public class MeteoMeasurement
    {
        [Key] public int Id { get; set; }
        [Required] [Column("Timestamp")] public DateTimeOffset DateTime { get; set; }
        [Required] public double Temperature { get; set; }
        [Required] public double Pressure { get; set; }
        [Required] public int DustPM10 { get; set; }
        [Required] public int DustPM25 { get; set; }
        [Required] public int DustPM100 { get; set; }
        [NotMapped] public bool IsDataCorrect { get; set; }
    }
}
