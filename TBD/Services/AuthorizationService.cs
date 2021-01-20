using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TBD.DbModels;
using TBD.Interfaces;
using TBD.Models;


namespace TBD.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly TBDDbContext _context;
        private readonly IValidationProvider _validationProvider;
        public AuthorizationService(TBDDbContext context, IValidationProvider validationProvider)
        {
            _context = context;
            _validationProvider = validationProvider;
        }

        public void CreateUser(UserViewModel user)
        {
            _validationProvider.Validate(user);
            _context.Add(new User()
            {
                Login = user.Login.Trim(),
                PasswordHash = HashPassword(user.Password),
                Name = user.Name.Trim(),
                Role = user.Role,
                ApiKey = CreateApiKey()
            });
            _context.SaveChanges();
        }

        private string CreateApiKey()
        {
            var rgx = new Regex("[^a-z0-9]");
            var apiKey = rgx.Replace(Convert.ToBase64String(GenerateRandomBytes(32)).ToLower(), "");
            return apiKey;
        }

        private byte[] GenerateRandomBytes(int size)
        {
            var bytes = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return bytes;
        }

        private string HashPassword(string password)
        {
            var salt = GenerateRandomBytes(16);
            var hash = CreateHash(password, salt);
            return ($"{Convert.ToBase64String(salt)}${hash}");
        }

        private string CreateHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                30000,
                32));
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var split = hashedPassword.Split("$");
            var salt = Convert.FromBase64String(split[0]);
            var hash = split[1];

            return hash == CreateHash(password, salt);
        }
    }
}
