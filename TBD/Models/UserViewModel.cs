using System.ComponentModel.DataAnnotations;
using TBD.Enums;

namespace TBD.Models
{
    public class UserViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
