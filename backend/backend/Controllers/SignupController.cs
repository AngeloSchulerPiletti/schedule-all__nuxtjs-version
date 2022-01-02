using backend.Business;
using backend.Data.VO;
using backend.Models;
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
        private IEthereumBusiness _ethereumBusiness;

        public SignupController(ISignupBusiness signupBusiness, IEthereumBusiness ethereumBusiness, IUserRepository userRepository) : base(userRepository)
        {
            _signupBusiness = signupBusiness;
            _ethereumBusiness = ethereumBusiness;
        }


        [HttpPost]
        [Route("signup")]
        public IActionResult Signup([FromBody] NewUserVO user)
        {
            MessageBadgeVO validateResult = _signupBusiness.ValidateInputs(user);
            if (validateResult.isError) return BadRequest(validateResult);

            MessageBadgeVO existanceResult = _signupBusiness.CheckIfUserAlreadyExists(user);
            if (existanceResult.isError) return BadRequest(existanceResult);

            // Adicionar a questão da wallet no Auth e etc
            MessageBadgeVO walletEventResult = _ethereumBusiness.CheckIfSignUpWasEmitted(user.UserName, user.WalletAddress);
            if (walletEventResult.isError) return BadRequest(walletEventResult);

            MessageBadgeVO saveResult = _signupBusiness.CreateNewUser(user);
            if (saveResult.isError) return BadRequest(saveResult);

            User freshUser = _signupBusiness.GetTheNewUser(user.UserName);
            if (freshUser == null) return BadRequest( new MessageBadgeVO(new List<string> { "Ocorreu um erro na obtenção dos dados do um novo usuário" }));

            UserDataVO userData = _signupBusiness.AuthTheNewUser(freshUser);
            if (userData == null) return BadRequest(new MessageBadgeVO(new List<string> { "Ocorreu um erro na autenticação automática de um novo usuário" }));

            return Ok(userData);
        }
    }
}
