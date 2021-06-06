using CouponsLtd.Data;
using CouponsLtd.Data.Entities;
using CouponsLtd.Helpers;
using CouponsLtd.UpsertModels;
using CouponsLtd.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CouponsLtd.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _environment;
        private readonly UnitOfWork _unitOfWork;

        public UserService(
            IOptions<AppSettings> appSettings, IWebHostEnvironment environment, UnitOfWork unitOfWork)
        {
            _environment = environment;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return null;

            var user = (await _unitOfWork.Users.GetAsync(0, 1, x => x.UserName == model.Username)).FirstOrDefault();

            if (user == null)
                return null;

            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public async Task<bool> CreateUsers(List<UserUpsert> users, bool usePrefilledData)
        {
            if (usePrefilledData)
            {
                users = JsonHelper.LoadFromJson<List<UserUpsert>>(_environment.ContentRootPath + "/Data/Mocks/users.json");
            }

            foreach (var user in users)
            {
                if (string.IsNullOrWhiteSpace(user.Password))
                    continue;

                var userDAO = (await _unitOfWork.Users.GetAsync(0, 1, x => x.UserName == user.UserName)).FirstOrDefault();
                if (userDAO != null)
                    continue;

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                var userDao = new UserDAO()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Id = Guid.NewGuid(),
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Created = System.DateTime.UtcNow
                };

                _unitOfWork.Users.Insert(userDao);
            }

            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<UserDAO> GetById(Guid id)
        {
            return (await _unitOfWork.Users.GetAsync(0, 1, x => x.Id == id)).FirstOrDefault();
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string GenerateJwtToken(UserDAO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}