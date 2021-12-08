using backend.Data.VO;
using backend.Data.VO.Friendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository
{
    public interface IFriendshipRepository
    {
        MessageBadgeVO CheckIfUserExists(string friendUserName);

        long GetIdFromUserName(string friendUserName);
        MessageBadgeVO DeleteInvite(long inviteId);
        MessageBadgeVO DeleteInvite(long userId, long senderId);
        MessageBadgeVO SendInvite(long senderId, long receiverId);
        MessageBadgeVO CheckIfInviteExists(long inviteId);
        MessageBadgeVO AnswerTheInvite(long userId, long senderId);
        MessageBadgeVO AnswerTheInvite(FriendshipAnswerVO inviteAnswer, long userId);
        List<FriendshipSenderVO> GetSenderVoFromReceiverId(long receiverId, bool inviteAnswer);
        List<FriendshipInviteVO> GetUsersDataFromSendersVo(List<FriendshipSenderVO> sendersId);
        MessageBadgeVO CheckIfIsAlreadyFriend(long friendId, long userId);
    }
}
