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
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            string trimmedTitle = title.Trim();
            MessageBadgeVO validateResult = _business.ValidateTitle(trimmedTitle);
            if (validateResult != null) return BadRequest(validateResult);

            object result = _business.CreateCategory(title, user.Id);
            if (result is MessageBadgeVO) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-user-categories")]
        public IActionResult GetUserCategorys()
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            List<Category> result = _business.GetUserCategorys(user.Id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-category")]
        public IActionResult DeleteCategory([FromBody] long categoryId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO validateResult = _business.ValidateId(categoryId);
            if (validateResult != null) return BadRequest(validateResult);

            MessageBadgeVO result = _business.DeleteCategory(categoryId, user.Id);
            if (result != null) return BadRequest(result);
            return NoContent();
        }


        [HttpPut]
        [Route("change-category")]
        public IActionResult ChangeCategory([FromBody] CategoryVO category)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO resultValidate = _business.ValidateCategory(category);
            if (resultValidate != null) return BadRequest(resultValidate);

            var result = _business.ChangeCategory(category, user.Id);
            if (result is MessageBadgeVO) return BadRequest(result);
            return Ok(result);
        }
    }
}
