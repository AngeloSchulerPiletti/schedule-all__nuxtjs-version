using backend.Data.VO;
using backend.Models;
using backend.Models.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            /*
                Verificação do username, verificar se é um email ou username
            */
            var emailPattern = @"^\S+@\S+\.\S+$";
            Match match = Regex.Match(user.UserName, emailPattern);
            bool isEmail = match.Success;

            // if (Caso o email do usuario seja nulo, faça a verificação por username)
            return _context.Users.FirstOrDefault(u => ((isEmail) ? u.Email == user.UserName : (u.UserName == user.UserName)) && (u.Password == pass));
        }


        public User ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }


        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;
          
            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public bool CheckIfUserAlreadyExists(NewUserVO user)
        {
            var usernameResult = _context.Users.SingleOrDefault(u => u.UserName == user.UserName);
            if (usernameResult != null) return true;
            var emailResult = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            if (emailResult != null) return true;
            return false;
        }

        public bool SaveNewUserOnDB(NewUserVO userVo)
        {
            string hashedPassword = ComputeHash(userVo.Password, new SHA256CryptoServiceProvider());
            string fullName = userVo.FullName;
            string email = userVo.Email;
            string userName = userVo.UserName;

            User user = new(userName, fullName, email, hashedPassword);

            _context.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}
