using backend.Data.VO;
using backend.Data.VO.Friendship;
using backend.Models;
using backend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository.Implementations
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly MySQLContext _context;

        public FriendshipRepository(MySQLContext context)
        {
            _context = context;
        }

        public MessageBadgeVO AnswerTheInvite(FriendshipAnswerVO inviteAnswer, long userId)
        {
            try
            {
                Friendship invite = _context.Friendships.FirstOrDefault(i => i.Id == inviteAnswer.InviteId);
                invite.InviteAccepted = inviteAnswer.Answer;
                _context.SaveChanges();
                return new MessageBadgeVO(new List<string> { "Convite respondido com sucesso!" }, false);
            }
            catch (Exception)
            {
                return new MessageBadgeVO(new List<string> { "Não possível responder este convite" });
            }

        }

        public MessageBadgeVO AnswerTheInvite(long userId, long senderId)
        {
            Friendship invite = _context.Friendships.FirstOrDefault(i => i.ReceiverId == userId && i.SenderId == senderId);
            invite.InviteAccepted = true;
            _context.SaveChanges();
            return null;
        }

        public MessageBadgeVO CheckIfInviteExists(long inviteId)
        {
            var result = _context.Friendships.FirstOrDefault(i => i.Id == inviteId);
            if (result == null) return new MessageBadgeVO(new List<string> { "O convite não existe" });
            return new MessageBadgeVO(new List<string> { "O convite existe" }, false);
        }

        public MessageBadgeVO CheckIfUserExists(string friendUserName)
        {
            var result = _context.Users.FirstOrDefault(u => u.UserName == friendUserName);
            if (result == null) return new MessageBadgeVO(new List<string> { "Nickname de amigo inválido" });
            return new MessageBadgeVO(new List<string> { "Amigo encontrado" }, false);
        }

        public MessageBadgeVO DeleteInvite(long inviteId)
        {
            try
            {
                Friendship invite = _context.Friendships.FirstOrDefault(i => i.Id == inviteId);
                _context.Friendships.Remove(invite);
                _context.SaveChanges();
                return new MessageBadgeVO(new List<string> { "Convite respondido com sucesso!" }, false);
            }
            catch (Exception)
            {
                return new MessageBadgeVO(new List<string> { "Não foi possível responder seu convite" });
            }
        }

        public MessageBadgeVO DeleteInvite(long userId, long senderId)
        {
            Friendship invite = _context.Friendships.FirstOrDefault(i => i.ReceiverId == userId && i.SenderId == senderId);
            _context.Friendships.Remove(invite);
            _context.SaveChanges();
            return null;
        }

        public List<FriendshipSenderVO> GetSenderVoFromReceiverId(long receiverId, bool inviteAnswer)
        {
            List<FriendshipSenderVO> friendsOrInvites = _context.Friendships.Where(i => (i.ReceiverId == receiverId) && (i.InviteAccepted == inviteAnswer)).Select(i => new FriendshipSenderVO(i.SenderId, i.Id)).ToList();
            if (inviteAnswer)
                friendsOrInvites = friendsOrInvites.Concat(_context.Friendships.Where(i => (i.SenderId == receiverId) && (i.InviteAccepted == true)).Select(i => new FriendshipSenderVO(i.ReceiverId, i.Id)).ToList()).ToList();
            return friendsOrInvites;
        }

        public long GetIdFromUserName(string friendUserName)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserName == friendUserName);
            return user.Id;
        }

        public List<FriendshipInviteVO> GetUsersDataFromSendersVo(List<FriendshipSenderVO> sendersVo)
        {
            return _context.Users.AsEnumerable().Join(
                sendersVo,
                user => user.Id,
                senderVo => senderVo.SenderId,
                (user, senderVo) => new FriendshipInviteVO(
                        user.UserName,
                        user.FullName,
                        senderVo.FriendshipId
                    )
                ).ToList();
        }

        public MessageBadgeVO SendInvite(long senderId, long receiverId)
        {
            Friendship friendship = new(senderId, receiverId);
            _context.Friendships.Add(friendship);
            _context.SaveChanges();
            return null;
        }

        public MessageBadgeVO CheckIfIsAlreadyFriend(long friendId, long userId)
        {
            MessageBadgeVO caseError = new MessageBadgeVO(new List<string> { "Você já é amigo dessa pessoa ou já tem um convite pendente" });
            var possibility1 = _context.Friendships.FirstOrDefault(f => f.ReceiverId == userId && f.SenderId == friendId);
            if (possibility1 != null) return caseError;
            var possibility2 = _context.Friendships.FirstOrDefault(f => f.SenderId == userId && f.ReceiverId == friendId);
            if (possibility2 != null) return caseError;
            return new MessageBadgeVO(new List<string> { "Você ainda não é amigo dessa pessoa" }, false);
        }

    }
}
