using backend.Business;
using backend.Business.Implementations;
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
    [Authorize("Bearer")]
    [ApiController]
    public class SimpleTodoController : ControllerBase
    {
        private ISimpleTodoBusiness _business;
        public SimpleTodoController(ISimpleTodoBusiness business, IUserRepository userRepository) : base(userRepository)
        {
            _business = business;
        }

        [HttpPost]
        [Route("create-simpletodo")]
        public IActionResult CreateSimpleTodo([FromBody] NewSimpleTodoVO simpletodo)
        {
            ErrorBadgeVO errors = new(new List<string>());
            var user = GetUserFromJWT();
            if (user == null)
            {
                errors.messages.Add("Houve um erro com seu token de autenticação");
                return BadRequest(errors);
            }
            var result = _business.CreateSimpleTodo(simpletodo, user);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }
    }
}
