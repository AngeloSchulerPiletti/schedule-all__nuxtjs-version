using backend.Data.VO;
using backend.Data.VO.Friendship;
using backend.Data.VO.Notification;
using backend.Models;
using backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business.Implementations
{
    public class FriendshipBusinessImplementation : IFriendshipBusiness
    {
        private IFriendshipRepository _repository;
        private IUserRepository _userRepository;
        private INotificationRepository _notificationRepository;
        public FriendshipBusinessImplementation(IFriendshipRepository repository, INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public MessageBadgeVO AnswerQuestion(Notification notification)
        {
            if (notification.Answer == 2) {
                MessageBadgeVO answerResult =_repository.AnswerTheInvite(notification.UserId, notification.CorrelatedUserId);
                if(!answerResult.isError)
                {
                    List<User> usersData = _userRepository.GetUsersDataFromIdList(new List<long> { notification.UserId, notification.CorrelatedUserId });

                    Notification acceptNotification1 = new(usersData[0].Id, "Novo amigo!", String.Concat("Agora, você e ", usersData[1].UserName, " são amigos!"), "friendship");
                    _notificationRepository.CreateNewNotification(acceptNotification1);
                    Notification acceptNotification2 = new(usersData[1].Id, "Novo amigo!", String.Concat("Agora, você e ", usersData[0].UserName, " são amigos!"), "friendship");
                    _notificationRepository.CreateNewNotification(acceptNotification2);
                }
            }
            return _repository.DeleteInvite(notification.UserId, notification.CorrelatedUserId);
        }

        public MessageBadgeVO AnswerTheInvite(FriendshipAnswerVO inviteAnswer, long userId)
        {
            if(inviteAnswer.Answer) return _repository.AnswerTheInvite(inviteAnswer, userId);
            return _repository.DeleteInvite(inviteAnswer.InviteId);
        }

        public MessageBadgeVO CheckIfFriendIsI(string userName, string friendUserName)
        {
            if (userName == friendUserName) return new MessageBadgeVO(new List<string> { "Você não pode adicionar você mesmo como amigo" });
            return new MessageBadgeVO(new List<string> { "Sucesso" }, false);
        }

        public MessageBadgeVO CheckIfInviteExists(long inviteId)
        {
            return _repository.CheckIfInviteExists(inviteId);
        }

        public MessageBadgeVO CheckIfIsAlreadyFriend(string friendUserName, long userId)
        {
            var friendId = _repository.GetIdFromUserName(friendUserName);
            return _repository.CheckIfIsAlreadyFriend(friendId, userId);
        }

        public MessageBadgeVO CheckIfUserExists(string friendUserName)
        {
            return _repository.CheckIfUserExists(friendUserName);
        }

        public MessageBadgeVO DeleteFriendship(long friendshipId)
        {
            return _repository.DeleteInvite(friendshipId);
        }

        public List<FriendshipInviteVO> GetAllUserFriends(long userId)
        {
            List<FriendshipSenderVO> sendersVo = _repository.GetSenderVoFromReceiverId(userId, true);
            if (sendersVo == null) return null;
            return GetUsersDataFromSendersId(sendersVo);
        }

        public List<FriendshipInviteVO> GetAllUserInvites(long receiverId)
        {
            List<FriendshipSenderVO> sendersVo = _repository.GetSenderVoFromReceiverId(receiverId, false);
            if (sendersVo == null) return null;
            return GetUsersDataFromSendersId(sendersVo);
        }

        public long GetIdFromUserName(string friendUserName)
        {
            return _repository.GetIdFromUserName(friendUserName);
        }

        public MessageBadgeVO SendInvite(long senderId, long receiverId)
        {
            try
            {
                _repository.SendInvite(senderId, receiverId);
                User sender = _userRepository.GetUserDataFromId(senderId);
                Notification notification = new(receiverId, "Convite de Amizade", String.Concat(sender.UserName, " enviou um convite de amizade!"), "friendship", senderId, null, true);
                _notificationRepository.CreateNewNotification(notification);
                return new MessageBadgeVO(new List<string> { "Convite enviado com sucesso!" }, false);
            }
            catch (Exception)
            {
                return new MessageBadgeVO(new List<string> { "Não foi possível enviar seu convite!" });
            }
        }

        private List<FriendshipInviteVO> GetUsersDataFromSendersId(List<FriendshipSenderVO> sendersId)
        {
            return _repository.GetUsersDataFromSendersVo(sendersId);
        }
    }
}
