using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TBD.Core;
using TBD.DbModels;
using TBD.Enums;
using TBD.Interfaces;
using TBD.Models;
using static System.Enum;


namespace TBD.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly TBDDbContext _context;
        private readonly IValidationProvider _validationProvider;
        private readonly AppSettings _appSettings;

        public AuthorizationService(TBDDbContext context, IValidationProvider validationProvider,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _validationProvider = validationProvider;
            _appSettings = appSettings.Value;
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
                ApiKey = GenerateApiKey()
            });
            _context.SaveChanges();
        }

        public string CreateToken(CredentialsViewModel credentials)
        {
            var user = _context.User.FirstOrDefault(x => x.Login.ToLower() == credentials.Login.ToLower());

            if (user == null) return null;
            if (!VerifyPassword(credentials.Password, user.PasswordHash)) return null;
            return CreateToken(user);
        }

        public User ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Convert.FromBase64String(_appSettings.Secret);
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                if (principal.Identity is not ClaimsIdentity identity)
                    return null;
                if (!identity.IsAuthenticated)
                    return null;

                if (!TryParse(identity.FindFirst(ClaimTypes.Role).Value, out Role role))
                {
                    role = Role.Guest;
                }

                return new User
                {
                    Login = identity.FindFirst(ClaimTypes.Name)?.Value,
                    Name = identity.FindFirst(ClaimTypes.GivenName)?.Value,
                    Role = role
                };
            }
            catch
            {
                return null;
            }
        }
        private string CreateToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.GivenName, user.Name)
                }),

                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenLifetime),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Convert.FromBase64String(_appSettings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var sToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(sToken);

            return token;
        }

        private static string GenerateApiKey()
        {
            var rgx = new Regex("[^a-z0-9]");
            var apiKey = rgx.Replace(Convert.ToBase64String(GenerateRandomBytes(32)).ToLower(), "");
            return apiKey;
        }

        private static byte[] GenerateRandomBytes(int size)
        {
            var bytes = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return bytes;
        }

        private static string HashPassword(string password)
        {
            var salt = GenerateRandomBytes(16);
            var hash = CreateHash(password, salt);
            return ($"{Convert.ToBase64String(salt)}${hash}");
        }

        private static string CreateHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                30000,
                32));
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            var split = hashedPassword.Split("$");
            var salt = Convert.FromBase64String(split[0]);
            var hash = split[1];

            return hash == CreateHash(password, salt);
        }
    }
}
