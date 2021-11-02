using backend.Business;
using backend.Business.Implementations;
using backend.Data.VO;
using backend.Models;
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

        [HttpGet]
        [Route("get-all-user-simpletodos")]
        public IActionResult GetAllUserSimpleTodos()
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            var result = _business.GetSimpleTodosByUserId(user.Id);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-user-simpletodo")]
        public IActionResult GetUserSimpleTodo([FromBody] long simpletodoId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            var result = _business.GetSingleSimpleTodoByUserId(user.Id, simpletodoId);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        [Route("create-simpletodo")]
        public IActionResult CreateSimpleTodo([FromBody] NewSimpleTodoVO simpletodo)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string>{ "Houve um erro com a sua identidade" }));
            
            var result = _business.CreateSimpleTodo(simpletodo, user);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch]
        [Route("change-simpletodo-state")]
        public IActionResult ChangeSimpleTodoState([FromBody] long simpletodoId)
        {
            if (simpletodoId == 0) return BadRequest(new ErrorBadgeVO(new List<string> { "Não foi possível finalizar essa tarefa" }));
            var result = _business.SetSimpleTodoState(simpletodoId);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }


        [HttpPut]
        [Route("update-simpletodo")]
        public IActionResult UpdateSimpleTodo([FromBody] SimpleTodoVO simpletodo)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            var result = _business.UpdateSimpleTodo(simpletodo, user);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-simpletodo")]
        public IActionResult DeleteSimpleTodo([FromBody] long simpletodoId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            var result = _business.DeleteSimpleTodo(user.Id, simpletodoId);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return NoContent();
        }

    }
}
