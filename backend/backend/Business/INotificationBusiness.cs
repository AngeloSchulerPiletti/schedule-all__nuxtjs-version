using backend.Data.VO;
using backend.Data.VO.Notification;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business
{
    public interface INotificationBusiness
    {
        public List<Notification> GetAllUserNotifications(long userId);
        public MessageBadgeVO AnswerBasedOnSubject(Notification notification);
        public Notification CheckNotificationOwner(long userId, long notificationId);
        public MessageBadgeVO CheckNotificationExistance(long notificationId);
        public Notification GetUserNotification(long notificationId);
        public MessageBadgeVO UpdateUserNotificationsSeen(long userId);
        public MessageBadgeVO DeleteNotification(long notificationId);
    }
}
