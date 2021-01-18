using System.ComponentModel.DataAnnotations;
using TBD.Enums;

namespace TBD.DbModels
{
    public class User
    {
        [Key] public int Id { get; set; }
        [Required] public string Login { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required] public string Name { get; set; }
        [Required] public Role Role { get; set; }
        [Required] public string ApiKey { get; set; }
    }
}
