using System.ComponentModel.DataAnnotations;
using TBD.Enums.Blinds;

namespace TBD.DbModels
{
    public class BlindSchedule
    {
        [Key] public int Id { get; set; }
        [Required] public Device Device { get; set; }
        [Required] public Action Action { get; set; }
        [Required] public HourType HourType { get; set; }
        [Required] public int TimeOffset { get; set; }
        [Required] public virtual User User { get; set; }
        [Required] public int IsActive { get; set; }
    }
}
