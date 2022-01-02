using backend.Data.VO;
using backend.Models;
using backend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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


        public User GetUserByUsername(string userName)
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

        public MessageBadgeVO CheckIfUserAlreadyExists(NewUserVO user)
        {
            MessageBadgeVO error = new(new List<string>());

            var usernameResult = _context.Users.SingleOrDefault(u => u.UserName.Equals(user.UserName));
            if (usernameResult != null) error.messages.Add("Nick de usuário já cadastrado. Escolha outro nick");
            var emailResult = _context.Users.SingleOrDefault(u => u.Email.Equals(user.Email));
            if (emailResult != null) error.messages.Add("Email de usuário já cadastrado. Escolha outro email ou faça login");
            var walletResult = _context.Users.SingleOrDefault(u => u.WalletAddress.Equals(user.WalletAddress));
            if (walletResult != null) error.messages.Add("Outro usuário já está usando esta wallet. Se você acha que isso e um erro, reivindique-a aqui");

            if (error.messages.Count == 0) error.isError = false;
            return error;
        }

        public bool SaveNewUserOnDB(NewUserVO userVo)
        {
            string hashedPassword = ComputeHash(userVo.Password, new SHA256CryptoServiceProvider());
            string fullName = userVo.FullName;
            string email = userVo.Email;
            string userName = userVo.UserName;
            string walletAddress = userVo.WalletAddress;

            User user = new(userName, fullName, email, hashedPassword, walletAddress);

            _context.Add(user);
            _context.SaveChanges();
            return true;
        }

        public MessageBadgeVO ValidateUserVO(UserVO user, MessageBadgeVO error)
        {
            foreach (PropertyInfo input in typeof(UserVO).GetProperties())
            {
                if(input.GetValue(user) == null)
                {
                    error.messages.Add($"O campo {user.inputsName[input.Name]} não pode ser nulo");
                    return error;
                }
            }

            var usernamePattern = @"^[\w-]+$";
            var emailPattern = @"^\S+@\S+\.\S+$";

            Match usernameMatch = Regex.Match(user.UserName, usernamePattern);
            bool isUsername = usernameMatch.Success;
            Match emailMatch = Regex.Match(user.UserName, emailPattern);
            bool isEmail = emailMatch.Success;

            if (!isEmail && !isUsername) error.messages.Add("Email ou nickname inválido");

            var passwordPattern = @"^\S+$";
            Match passwordMatch = Regex.Match(user.Password, passwordPattern);
            bool isPassword = passwordMatch.Success;
            if (!isPassword) error.messages.Add("Formato de senha inválido");

            if (error.messages.Count > 0) return error;
            return null;
        }

        public MessageBadgeVO ValidateNewUserVO(NewUserVO user)
        {
            MessageBadgeVO error = new(new List<string>());

            foreach (PropertyInfo input in typeof(NewUserVO).GetProperties())
            {
                if (input.GetValue(user) == null)
                {
                    error.messages.Add($"O campo {user.inputsName[input.Name]} não pode ser nulo");
                    return error;
                }
            }


            if (user.WalletAddress.Length != 40) error.messages.Add("Endereço da wallet inválido");
            var walletPattern = @"^0x[a-fA-F0-9]{40}$";
            Match walletMatch = Regex.Match(user.WalletAddress, walletPattern);
            bool isWallet = walletMatch.Success;
            if (!isWallet) error.messages.Add("Endereço de wallet inválido. Você precisa estar conectado a uma wallet para se cadastrar");


            if (user.UserName.Length < 4) error.messages.Add("Nickname precisa ter pelo menos 5 caracteres");
            var usernamePattern = @"^[\w-]+$";
            Match usernameMatch = Regex.Match(user.UserName, usernamePattern);
            bool isUsername = usernameMatch.Success;
            if (!isUsername) error.messages.Add("Nome de usuário inválido. Utilize apenas letras, números e underscore");

            var emailPattern = @"^\S+@\S+\.\S+$";
            Match emailMatch = Regex.Match(user.Email, emailPattern);
            bool isEmail = emailMatch.Success;
            if (!isEmail) error.messages.Add("Email inválido");


            var namePattern = @"^[a-zA-Z áàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]+$";
            Match nameMatch = Regex.Match(user.FullName, namePattern);
            bool isName = nameMatch.Success;
            if (!isName) error.messages.Add("Nome inválido, preencha apenas com caracteres válidos");

            if (user.Password.Length < 8) error.messages.Add("Senha precisa ter pelo menos 8 caracteres");
            var passwordPattern = @"^\S+$";
            Match passwordMatch = Regex.Match(user.Password, passwordPattern);
            bool isPassword = passwordMatch.Success;
            if (!isPassword) error.messages.Add("Senha não deve conter espaços em branco");

            if (user.PasswordConfirmation == null) error.messages.Add("Preencha a confirmação de senha");
            if (!user.Password.Equals(user.PasswordConfirmation)) error.messages.Add("A confirmação de senha é diferente da senha");

            if (error.messages.Count == 0) error.isError = false;
            return error;
        }

        public User GetUserById(long id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetUsersByIdList(List<long> ids)
        {
            return _context.Users.Where(u => ids.Contains(u.Id)).ToList();
        }
    }
}
