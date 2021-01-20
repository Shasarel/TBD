using TBD.Models;

namespace TBD.Interfaces
{
    public interface IAuthorizationService
    {
        public void CreateUser(UserViewModel userViewModel);
    }
}
