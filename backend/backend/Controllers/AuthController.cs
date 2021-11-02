using backend.Business;
using backend.Data.VO;
using backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness, IUserRepository userRepository) : base(userRepository)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user) 
        {
            if (user == null) return BadRequest("Usuário Inválido");
            var userData = _loginBusiness.ValidateCredentials(user);
            if (userData is MessageBadgeVO) return BadRequest(userData);
         
            return Ok(userData);
        }


        [HttpPost]
        [Route("validate-token")]
        [Authorize("Bearer")]
        public IActionResult ValidateToken()
        {
            return Ok("Token Válido");
        }


        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null) return BadRequest("Invalid Request");
            var userData = _loginBusiness.ValidateCredentials(tokenVo);
            if (userData == null) return BadRequest("Token Inválido");

            return Ok(userData);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);

            if (!result) return BadRequest("Invalid Request");

            return NoContent();
        }
    }
}
