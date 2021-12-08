using backend.Data.VO;
using backend.Data.VO.Notification;
using backend.Models;
using backend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend.Repository.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MySQLContext _context;

        public NotificationRepository(MySQLContext context)
        {
            _context = context;
        }

        public Notification CheckNotificationOwner(long userId, long notificationId)
        {
            return _context.Notifications.FirstOrDefault(n => n.UserId == userId && n.Id == notificationId);
        }

        public Notification CreateNewNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return notification;
        }

        public Notification DeleteNotification(long notificationId)
        {
            Notification notification = _context.Notifications.FirstOrDefault(n => n.Id == notificationId);
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
            return notification;
        }

        public List<Notification> GetAllUserNotifications(long userId)
        {
            List<Notification> notifications = _context.Notifications.Where(n => n.UserId == userId).ToList();
            return notifications;
        }

        public Notification GetNotification(long notificationId)
        {
            return _context.Notifications.FirstOrDefault(n => n.Id == notificationId);
        }

        public List<Notification> UpdateUserNotificationsSeen(long userId)
        {
            List<Notification> notifications = _context.Notifications.Where(n => n.UserId == userId && n.WasSeen == false).ToList();
            foreach (Notification n in notifications)
            {
                n.WasSeen = true;
            }
            _context.SaveChanges();
            return notifications;
        }
    }
}
