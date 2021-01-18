using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD.DbModels
{
    public class DailyMeteoSummary
    {
        [Key] public int Id { get; set; }
        [Required] public DateTimeOffset Date { get; set; }
        [Required] public double TemperatureMin { get; set; }
        [Required] public double TemperatureAvg { get; set; }
        [Required] public double TemperatureMax { get; set; }
        [Required] public double HumidityMin { get; set; }
        [Required] public double HumidityAvg { get; set; }
        [Required] public double HumidityMax { get; set; }
        [Required] public double PressureMin { get; set; }
        [Required] public double PressureAvg { get; set; }
        [Required] public double PressureMax { get; set; }
        [Required] public int DustPM10Min { get; set; }
        [Required] public int DustPM10Avg { get; set; }
        [Required] public int DustPM10Max { get; set; }
        [Required] public int DustPM25Min { get; set; }
        [Required] public int DustPM25Avg { get; set; }
        [Required] public int DustPM25Max { get; set; }
        [Required] public int DustPM100Min { get; set; }
        [Required] public int DustPM100Avg { get; set; }
        [Required] public int DustPM100Max { get; set; }
        [Required] public bool IsDataCorrect { get; set; }

    }
}
