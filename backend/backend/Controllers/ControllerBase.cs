using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;

namespace backend.Controllers
{
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private IUserRepository _userRepository;

        public ControllerBase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public User GetUserFromJWT()
        {
            var headers = Request.Headers;
            if (headers.ContainsKey("Authorization"))
            {
                try
                {
                    headers.TryGetValue("Authorization", out var stream);
                    AuthenticationHeaderValue.TryParse(stream, out var tokenValue);

                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(tokenValue.Parameter);
                    var token = jsonToken as JwtSecurityToken;

                    var userName = token.Claims.First(claim => claim.Type == "unique_name").Value;
                    var user = _userRepository.ValidateCredentials(userName);
                    return user;
            }
                catch (Exception)
            {
                return null;
            }

        }
            return null;
        }
    }
}
