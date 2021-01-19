using TBD.Enums;

namespace TBD.Interfaces
{
    public interface IAuthorizationService
    {
        public bool CreateUser(string login, string password, string name, Role role);
    }
}
