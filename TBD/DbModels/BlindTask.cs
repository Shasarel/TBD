using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TBD.Enums.Blinds;
using Action = TBD.Enums.Blinds.Action;

namespace TBD.DbModels
{
    public class BlindTask
    {
        [Key] public int Id { get; set; }
        [Required][Column("Timestamp")] public DateTimeOffset DateTime { get; set; }
        [Required] public Device Device { get; set; }
        [Required] public Action Action { get; set; }
        public virtual User User { get; set; }
        public virtual BlindSchedule BlindsSchedule { get; set; }
        [Required] public int Timeout { get; set; }
        [Required] public TaskStatus Status { get; set; }
    }
}
