using backend.Business;
using backend.Data.VO;
using backend.Repository;
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
    public class SignupController : ControllerBase
    {
        private ISignupBusiness _signupBusiness;

        public SignupController(ISignupBusiness signupBusiness)
        {
            _signupBusiness = signupBusiness;
        }


        [HttpPost]
        [Route("signup")]
        public IActionResult Signup([FromBody] NewUserVO user)
        {
            var token = _signupBusiness.CreateNewUser(user);
            if (token == null) return BadRequest("Dado Inválido");

            return Ok(token);
        }
    }
}
