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
                    headers.TryGetValue("Authorization", out var stream);
                    AuthenticationHeaderValue.TryParse(stream, out var tokenValue);

                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(tokenValue.Parameter);
                    var token = jsonToken as JwtSecurityToken;

                    var userName = token.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
                    if (userName == null) return null;
                    var user = _userRepository.GetUserByUsername(userName);
                    return user;
        }
            return null;
        }
    }
}
