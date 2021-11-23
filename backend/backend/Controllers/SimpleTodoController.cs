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
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            List<SimpleTodo> result = _business.GetSimpleTodosByUserId(user.Id);
            if (result == null) return BadRequest(new MessageBadgeVO(new List<string> { "Não encontramos essas tarefas..." }));
            return Ok(result);
        }

        [HttpGet]
        [Route("get-user-simpletodo")]
        public IActionResult GetUserSimpleTodo([FromBody] long simpletodoId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            SimpleTodo result = _business.GetSingleSimpleTodoByUserId(user.Id, simpletodoId);
            if (result == null) return BadRequest(new MessageBadgeVO(new List<string> { "Não encontramos essas tarefas..." }));
            return Ok(result);
        }

        [HttpGet]
        [Route("get-user-simpletodos-by-category")]
        public IActionResult GetUserSimpleTodosByCategory([FromQuery] int? pagination)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            return Ok(_business.GetUserSimpleTodosByCategory(pagination, user.Id));
        }

        [HttpPost]
        [Route("create-simpletodo")]
        public IActionResult CreateSimpleTodo([FromBody] NewSimpleTodoVO simpletodo)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string>{ "Houve um erro com a sua identidade" }));

            MessageBadgeVO validationResult = _business.ValidateSimpleTodoInput(simpletodo);
            if (validationResult != null) return BadRequest(validationResult);

            object result = _business.CreateSimpleTodo(simpletodo, user);
            if (result is MessageBadgeVO) return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch]
        [Route("change-simpletodo-state")]
        public IActionResult ChangeSimpleTodoState([FromBody] long simpletodoId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO validationResult = _business.ValidateId(simpletodoId);
            if (validationResult != null) return BadRequest(validationResult);

            dynamic result = _business.SetSimpleTodoState(simpletodoId, user.Id);
            if (result is MessageBadgeVO ? result.isError : false) return BadRequest(result);
            return Ok(result);
        

        [HttpPatch]
        [Route("change-simpletodo-importance")]
        public IActionResult ChangeSimpleTodoImportance([FromBody] long simpletodoId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO validationResult = _business.ValidateId(simpletodoId);
            if (validationResult != null) return BadRequest(validationResult);

            dynamic result = _business.SetSimpleTodoImportance(simpletodoId, user.Id);
            if (result is MessageBadgeVO ? result.isError : false) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        [Route("update-simpletodo")]
        public IActionResult UpdateSimpleTodo([FromBody] SimpleTodoVO simpletodo)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO result = _business.UpdateSimpleTodo(simpletodo, user);
            if (result.isError) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-simpletodo")]
        public IActionResult DeleteSimpleTodo([FromBody] long simpletodoId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO validationResult = _business.ValidateId(simpletodoId);
            if (validationResult != null) return BadRequest(validationResult);

            MessageBadgeVO result = _business.DeleteSimpleTodo(user.Id, simpletodoId);
            if (result != null) return BadRequest(result);
            return NoContent();
        }

    }
}
