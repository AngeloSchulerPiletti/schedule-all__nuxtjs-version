﻿using backend.Configurations;
using backend.Data.VO;
using backend.Repository;
using backend.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;

        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public object ValidateCredentials(UserVO userCredentials)
        {
            MessageBadgeVO error = new(new List<string>());
            MessageBadgeVO result = _repository.ValidateUserVO(userCredentials, error);
            if (result != null) return result;

            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return new MessageBadgeVO(new List<string>(new string[] { "email, usuário ou senha incorretos" }));

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            TokenVO token = new(
                            true,
                            createDate.ToString(DATE_FORMAT),
                            expirationDate.ToString(DATE_FORMAT),
                            accessToken,
                            refreshToken
                            );

            return new UserDataVO(user.UserName, user.FullName, user.Email, token);
        }

        public UserDataVO ValidateCredentials(TokenVO tokenVo)
        {
            var accessToken = tokenVo.AccessToken;
            var refreshToken = tokenVo.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null) return null;

            var username = principal.Identity.Name;

            var user = _repository.ValidateCredentials(username);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            TokenVO token = new(
                            true,
                            createDate.ToString(DATE_FORMAT),
                            expirationDate.ToString(DATE_FORMAT),
                            accessToken,
                            refreshToken
                            );

            return new UserDataVO(user.UserName, user.FullName, user.Email, token);
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
