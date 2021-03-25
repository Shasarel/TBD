using TBD.DbModels;
using TBD.Models;

namespace TBD.Interfaces
{
    public interface IAuthorizationService
    {
        public void CreateUser(UserViewModel userViewModel);
        public string CreateToken(CredentialsViewModel userViewModel);
        public User ValidateToken(string token);
    }
}
