﻿using backend.Data.VO;
using backend.Data.VO.Friendship;
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
        public FriendshipBusinessImplementation(IFriendshipRepository repository)
        {
            _repository = repository;
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
            return _repository.SendInvite(senderId, receiverId);
        }

        private List<FriendshipInviteVO> GetUsersDataFromSendersId(List<FriendshipSenderVO> sendersId)
        {
            return _repository.GetUsersDataFromSendersVo(sendersId);
        }
    }
}