using backend.Business;
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
    [ApiController]
    [Authorize("Bearer")]
    public class CategoryController : ControllerBase
    {
        private ICategoryBusiness _business;

        public CategoryController(ICategoryBusiness business, IUserRepository user) : base(user)
        {
            _business = business;
        }

        [HttpPost]
        [Route("create-category")]
        public IActionResult CreateCategory([FromBody] string title)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            var result = _business.CreateCategory(title, user.Id);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-user-categories")]
        public IActionResult GetUserCategorys()
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            var result = _business.GetUserCategorys(user.Id);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-category")]
        public IActionResult DeleteCategory([FromBody] long categoryId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            var result = _business.DeleteCategory(categoryId, user.Id);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return NoContent();
        }


        [HttpPut]
        [Route("change-category")]
        public IActionResult ChangeCategory([FromBody] CategoryVO category)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new ErrorBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            var result = _business.ChangeCategory(category, user.Id);
            if (result is ErrorBadgeVO) return BadRequest(result);
            return Ok(result);
        }
    }
}
