using backend.Configurations;
using backend.Data.VO;
using backend.Models;
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

        public UserDataVO AuthTheNewUser(User freshUser)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, freshUser.UserName)
            };

            string accessToken = _tokenService.GenerateAccessToken(claims);
            string refreshToken = _tokenService.GenerateRefreshToken();

            freshUser.RefreshToken = refreshToken;
            freshUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfo(freshUser);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);


            TokenVO token = new(
                            true,
                            createDate.ToString(DATE_FORMAT),
                            expirationDate.ToString(DATE_FORMAT),
                            accessToken,
                            refreshToken
                            );

            return new UserDataVO(freshUser.UserName, freshUser.FullName, freshUser.Email, freshUser.WalletAddress, token);
        }

        public MessageBadgeVO CheckIfUserAlreadyExists(NewUserVO user)
        {
            return _repository.CheckIfUserAlreadyExists(user);
        }

        public MessageBadgeVO CreateNewUser(NewUserVO user)
        {
            bool saveResult = _repository.SaveNewUserOnDB(user);
            
            if (!saveResult) return new MessageBadgeVO(new List<string> { "Houve um erro ao salvar seu usuário. Tente novamente mais tarde" });
            return new MessageBadgeVO(new List<string> { "Usuário salvo com sucesso" }, false);
        }

        public User GetTheNewUser(string username)
        {
            return _repository.GetUserByUsername(username);
        }

        public MessageBadgeVO ValidateInputs(NewUserVO user)
        {
            return _repository.ValidateNewUserVO(user);
        }


    }
}
