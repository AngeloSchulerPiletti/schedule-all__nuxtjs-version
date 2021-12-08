using backend.Data.VO;
using backend.Data.VO.Notification;
using backend.Models;
using backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend.Business.Implementations
{
    public class NotificationBusiness : INotificationBusiness
    {
        private INotificationRepository _repository;
        private Dictionary<string, dynamic> _business;

        public NotificationBusiness(INotificationRepository repository, IFriendshipBusiness friendshipBusiness, ISimpleTodoBusiness simpleTodoBusiness)
        {
            _repository = repository;
            _business = new Dictionary<string, dynamic>()
            {
                { "friendship", friendshipBusiness},
                {"simpletodo", simpleTodoBusiness }
            };
        }

        public MessageBadgeVO AnswerBasedOnSubject(Notification notification)
        {
            try
            {
                _business[notification.Subject].AnswerQuestion(notification);
                _repository.DeleteNotification(notification.Id);
                return new MessageBadgeVO(new List<string> { "Convite respondido com sucesso!" }, false);
            }
            catch (Exception)
            {
                return new MessageBadgeVO(new List<string> { "Não possível responder este convite" });
            }
        }

        public MessageBadgeVO CheckNotificationExistance(long notificationId)
        {
            Notification notification = _repository.GetNotification(notificationId);
            if (notification == null) return new MessageBadgeVO(new List<string> { "Notificação não existe" });
            return new MessageBadgeVO(new List<string> { "Notificação existe" }, false);
        }

        public Notification CheckNotificationOwner(long userId, long notificationId)
        {
            return _repository.CheckNotificationOwner(userId, notificationId);
        }

        public MessageBadgeVO DeleteNotification(long notificationId)
        {
            try
            {
                Notification notification = _repository.DeleteNotification(notificationId);
                return new MessageBadgeVO(new List<string> { "Notificação deletada" }, false);
            }
            catch (Exception)
            {
                return new MessageBadgeVO(new List<string> { "Ocorreu um erro ao deletar a notificação" });
            }
        }

        public List<Notification> GetAllUserNotifications(long userId)
        {
            return _repository.GetAllUserNotifications(userId);
        }

        public Notification GetUserNotification(long notificationId)
        {
            return _repository.GetNotification(notificationId);
        }

        public MessageBadgeVO UpdateUserNotificationsSeen(long userId)
        {
            try
            {
                _repository.UpdateUserNotificationsSeen(userId);
                return new MessageBadgeVO(new List<string> { "Notificações visualizadas" }, false);
            }
            catch (Exception)
            {
                return new MessageBadgeVO(new List<string> { "Houve um erro atualizando o estado das notificações" });
            }

        }
    }
}
