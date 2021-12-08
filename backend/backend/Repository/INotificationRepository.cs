using backend.Data.VO;
using backend.Data.VO.Notification;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository
{
    public interface INotificationRepository
    {
        public List<Notification> GetAllUserNotifications(long userId);
        public Notification CheckNotificationOwner(long userId, long notificationId);
        public Notification GetNotification(long notificationId);
        public List<Notification> UpdateUserNotificationsSeen(long userId);
        public Notification DeleteNotification(long notificationId);
        public Notification CreateNewNotification(Notification notification);
    }
}
