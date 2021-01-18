using System;
using System.ComponentModel.DataAnnotations;

namespace TBD.DbModels
{
    public class EnergyCorrection
    {
        [Key] public int Id { get; set; }
        [Required] public DateTimeOffset Date { get; set; }
        [Required] public double Correction { get; set; }
    }
}
