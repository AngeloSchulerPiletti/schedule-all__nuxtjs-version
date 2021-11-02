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

        public object CreateNewUser(NewUserVO user)
        {
            MessageBadgeVO errors = new(new List<string>());

            MessageBadgeVO inputsValidateResult = _repository.ValidateNewUserVO(user, errors);
            if (inputsValidateResult != null) return inputsValidateResult;

            MessageBadgeVO checkUserExistenceResult = _repository.CheckIfUserAlreadyExists(user, errors);
            if (checkUserExistenceResult is MessageBadgeVO) return checkUserExistenceResult;

            bool saveResult = _repository.SaveNewUserOnDB(user);
            if (!saveResult) return new MessageBadgeVO(new List<string> { "Houve um erro ao salvar seu usuário. Tente novamente mais tarde" });

            User freshUser = _repository.ValidateCredentials(user.UserName);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            string accessToken = _tokenService.GenerateAccessToken(claims);
            string refreshToken = _tokenService.GenerateRefreshToken();

            freshUser.RefreshToken = refreshToken;
            freshUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfo(freshUser);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            if (errors.messages.Count > 0) return errors;

            TokenVO token = new(
                            true,
                            createDate.ToString(DATE_FORMAT),
                            expirationDate.ToString(DATE_FORMAT),
                            accessToken,
                            refreshToken
                            );

            return new UserDataVO(user.UserName, user.FullName, user.Email, token);
        }

    }
}
