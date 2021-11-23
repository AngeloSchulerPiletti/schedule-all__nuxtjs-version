using backend.Business;
using backend.Data.VO;
using backend.Data.VO.Friendship;
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
    public class FriendshipController : ControllerBase
    {
        private IFriendshipBusiness _business;
        public FriendshipController(IUserRepository userRepository, IFriendshipBusiness business) : base(userRepository)
        {
            _business = business;
        }

        [HttpPost]
        [Route("invite-friend")]
        public IActionResult InviteFriend([FromBody] string friendUserName)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO checkResult = _business.CheckIfUserExists(friendUserName);
            if (checkResult.isError) return BadRequest(checkResult);

            MessageBadgeVO checkI = _business.CheckIfFriendIsI(user.UserName, friendUserName);
            if (checkI.isError) return BadRequest(checkI);

            MessageBadgeVO alreadyFriendResult = _business.CheckIfIsAlreadyFriend(friendUserName, user.Id);
            if (alreadyFriendResult.isError) return BadRequest(alreadyFriendResult);

            long friendId = _business.GetIdFromUserName(friendUserName);
            MessageBadgeVO sendResult = _business.SendInvite(user.Id, friendId);
            if (sendResult.isError) return BadRequest(sendResult);
            return Ok(sendResult);

        } 

        [HttpPut]
        [Route("answer-invite")]
        public IActionResult AnswerInvite([FromBody] FriendshipAnswerVO inviteAnswer)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO checkResult = _business.CheckIfInviteExists(inviteAnswer.InviteId);
            if (checkResult.isError) return BadRequest(checkResult);

            MessageBadgeVO answerResult = _business.AnswerTheInvite(inviteAnswer, user.Id);
            if (answerResult.isError) return StatusCode(500, answerResult);
            return Ok(answerResult);

        }

        [HttpDelete]
        [Route("delete-friendship")]
        public IActionResult DeleteFriendship([FromBody] long friendshipId)
        {
            MessageBadgeVO checkResult = _business.CheckIfInviteExists(friendshipId);
            if (checkResult.isError) return BadRequest(checkResult);

            MessageBadgeVO deleteResult = _business.DeleteFriendship(friendshipId);
            if (deleteResult.isError) return BadRequest(deleteResult);
            return Ok(deleteResult);
        }

        [HttpGet]
        [Route("get-all-invites")]
        public IActionResult GetAllInvites()
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            List<FriendshipInviteVO> invites = _business.GetAllUserInvites(user.Id);
            return Ok(invites);
        }

        [HttpGet]
        [Route("get-all-friends")]
        public IActionResult GetAllFriends()
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            List<FriendshipInviteVO> invites = _business.GetAllUserFriends(user.Id);

            return Ok(invites);
        }
    }
}
