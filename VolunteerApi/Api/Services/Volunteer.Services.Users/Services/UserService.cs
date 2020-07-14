using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Volunteer.DAL;
using Volunteer.DAL.Entities;
using Volunteer.DAL.Enums;
using Volunteer.DAL.Relations;
using Volunteer.Services.Users.Interfaces;
using Volunteer.Services.Users.Models;

namespace Volunteer.Services.Users.Services
{
    public class UserService : IUserService
    {
        private readonly DalContext _context;

        public UserService(DalContext context)
        {
            _context = context;
        }

        public async Task<UserAccountDto> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users.Include(x => x.RoleUserAccount).SingleOrDefaultAsync(x => username == x.UserName);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return new UserAccountDto
            {
                UserAccountId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                Role = user.Role
            };
        }

        public async Task<UserAccountDto> Create(RegisterRequestDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
                throw new Exception("Password is required");

            if (_context.Users.Any(x => x.UserName == model.UserName))
                throw new Exception("Username \"" + model.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            var userEntity = new UserAccountEntity()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserName = model.UserName,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                Role = model.Role
            };

            _context.Users.Add(userEntity);
            _context.SaveChanges();

            return new UserAccountDto 
            {
                UserAccountId = userEntity.Id,
                UserName = userEntity.UserName
            };
        }

        public UserAccountDto GetUserById(int id)
        {
            var user = _context.Users.Find(id);

            return new UserAccountDto
            {
                UserAccountId = user.Id,
                UserName = user.UserName
            };
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
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
    }
}
