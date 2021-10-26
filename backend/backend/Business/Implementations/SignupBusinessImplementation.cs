using backend.Configurations;
using backend.Data.VO;
using backend.Repository;
using backend.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend.Business.Implementations
{
    public class SignupBusinessImplementation : ISignupBusiness
    {
        private IUserRepository _repository;
        private const string DATE_FORMAT = "yyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public SignupBusinessImplementation(TokenConfiguration configuration,  IUserRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public TokenVO CreateNewUser(NewUserVO user)
        {
            if (user.UserName.Length < 4) return null;
            var usernamePattern = @"^\w+$";
            Match usernameMatch = Regex.Match(user.UserName, usernamePattern);
            bool isUsername = usernameMatch.Success;
            if (!isUsername) return null;

            var emailPattern = @"^\S+@\S+\.\S+$";
            Match emailMatch = Regex.Match(user.Email, emailPattern);
            bool isEmail = emailMatch.Success;
            if (!isEmail) return null;

            var namePattern = @"^[a-zA-Z\s]+$";
            Match nameMatch = Regex.Match(user.FullName, namePattern);
            bool isName = nameMatch.Success;
            if (!isName) return null;

            if (user.Password.Length < 8) return null;
            var passwordPattern = @"^\S+$";
            Match passwordMatch = Regex.Match(user.Password, passwordPattern);
            bool isPassword = passwordMatch.Success;
            if (!isPassword) return null;

            if (user.PasswordConfirmation == null) return null;
            if (!user.Password.Equals(user.PasswordConfirmation)) return null;

            if (_repository.CheckIfUserAlreadyExists(user)) return null;



            var saveResult = _repository.SaveNewUserOnDB(user);
            if (!saveResult) return null;



            var freshUser = _repository.ValidateCredentials(user.UserName);
            if (freshUser == null) return null; //APAGA ISSO DEPOIS

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            freshUser.RefreshToken = refreshToken;
            freshUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfo(freshUser);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }
    }
}
