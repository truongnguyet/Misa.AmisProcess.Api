using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MisaWebApi.Entities;
using MisaWebApi.Helpers;
using MisaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace MisaWebApi.Services
{
    public interface IUserService
    {
        Users Authenticate(string username, string password);
        IEnumerable<Users> GetAll();
        Users GetById(string id);
        UserEntity Create(UserEntity user, string password);
        
    }

    public class UsersService : IUserService
    {
        private AmisContext _context;
        private IMapper _mapper;

        public UsersService(AmisContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Users Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Email == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct

            //if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    return null;
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            if(!(user.PasswordHash == passwordHash.ToString() && user.PasswordSalt == passwordSalt.ToString()))
            {
                return null;
            }
           
            // authentication successful
            return user ;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users;
        }

        public Users GetById(string id)
        {
            return _context.Users.Find(id);
        }

        public UserEntity Create(UserEntity user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email \"" + user.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
         

            var newUser = new Users
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Address = user.Address,
                Position = user.Position,
                DateOfBirth = user.DateOfBirth,
                PasswordHash = passwordHash.ToString(),
                PasswordSalt = passwordSalt.ToString()
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return user;
        }



        // private helper methods
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

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static string Base64Hash(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
            
        }
        



    }
}