using backend.Business;
using backend.Data.VO;
using backend.Data.VO.Notification;
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
    public class NotificationController : ControllerBase
    {
        private INotificationBusiness _business;

        public NotificationController(INotificationBusiness business, IUserRepository userRepository) : base(userRepository)
        {
            _business = business;
        }

        //Atenção, as demais controllers precisarão acessar o business da notificação - seja diretamente ou através dos seus busines - para criar novas notificações

        [HttpGet]
        [Route("get-user-notifications")]
        public IActionResult GetUserNotifications()
        {
            //Ordenar por created_at

            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            return Ok(_business.GetAllUserNotifications(user.Id));
        }


        [HttpGet]
        [Route("get-user-specific-notification")]
        public IActionResult GetUserSpecificNotification([FromBody] long notificationId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            Notification notification = _business.CheckNotificationOwner(user.Id, notificationId);
            if (notification == null) return BadRequest(new MessageBadgeVO(new List<string> { "Notificação não encontrada" }));

            return Ok(_business.GetUserNotification(notificationId));
        }

        [HttpPatch]
        [Route("notifications-was-seen")]
        public IActionResult NotificationsWasSeen()
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            MessageBadgeVO message = _business.UpdateUserNotificationsSeen(user.Id);
            if (message.isError) return BadRequest(message);
            return Ok(message);
        }

        [HttpDelete]
        [Route("delete-notification")]
        public IActionResult DeleteNotification([FromBody] long notificationId)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            Notification notification = _business.CheckNotificationOwner(user.Id, notificationId);
            if (notification == null) return BadRequest(new MessageBadgeVO(new List<string> { "Notificação não encontrada" }));

            if (notification.HasQuestion)
            {
                notification.Answer = 1;
                MessageBadgeVO answerResult = _business.AnswerBasedOnSubject(notification);
                if (answerResult.isError) return BadRequest(answerResult);
            }

            MessageBadgeVO deleteResult = _business.DeleteNotification(notificationId);
            if (deleteResult.isError) return BadRequest(deleteResult);
            return Ok(deleteResult);
        }

        [HttpPut]
        [Route("answer-notification")]
        public IActionResult AnswerNotification([FromBody] NotificationAnswerVO notificationVO)
        {
            User user = GetUserFromJWT();
            if (user == null) return BadRequest(new MessageBadgeVO(new List<string> { "Houve um erro com a sua identidade" }));

            Notification notification = _business.CheckNotificationOwner(user.Id, notificationVO.Id);
            if (notification == null) return BadRequest(new MessageBadgeVO(new List<string> { "Notificação não encontrada" }));

            if (!notification.HasQuestion) return BadRequest(new MessageBadgeVO(new List<string> { "Essa notificação não pode ser respondida" }));

            if (notificationVO.Answer != 1 && notificationVO.Answer != 2) return BadRequest(new MessageBadgeVO(new List<string> { "Resposta inválida" }));
            notification.Answer = notificationVO.Answer;

            MessageBadgeVO answerResult = _business.AnswerBasedOnSubject(notification);
            if (answerResult.isError) return BadRequest(answerResult);

            MessageBadgeVO deleteResult = _business.DeleteNotification(notificationVO.Id);
            if (deleteResult.isError) return BadRequest(deleteResult);

            return Ok(deleteResult);
        }
    }
}