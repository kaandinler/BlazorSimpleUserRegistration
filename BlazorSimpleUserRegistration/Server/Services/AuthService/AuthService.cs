

using BlazorSimpleUserRegistration.Shared;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace BlazorSimpleUserRegistration.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                return true; // if query finds same name email address then return true and yes there is a recond with this email
            }

            return false; //but if there is no, then return false, and we can register with this email.


        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {

            //Firstly check the email address.
            if (await IsUserExists(user.Email))
            {
                //if it returns true
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Email Already Exists!"
                };
            }

            //Adding new user with Hashed and salted password.
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "Registration Successful!" };

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            };
        }
    }
}
