using backend.Data.VO;
using backend.Data.VO.Friendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business
{
    public interface IFriendshipBusiness
    {
        MessageBadgeVO CheckIfUserExists(string friendUserName);
        MessageBadgeVO CheckIfInviteExists(long inviteId);
        long GetIdFromUserName(string friendUserName);
        MessageBadgeVO SendInvite(long senderId, long receiverId);
        List<FriendshipInviteVO> GetAllUserInvites(long receiverId);
        MessageBadgeVO AnswerTheInvite(FriendshipAnswerVO inviteAnswer, long userId);
        List<FriendshipInviteVO> GetAllUserFriends(long userId);
        MessageBadgeVO CheckIfFriendIsI(string userName, string friendUserName);
        MessageBadgeVO CheckIfIsAlreadyFriend(string friendUserName, long userId);
        MessageBadgeVO DeleteFriendship( long friendshipId);
    }
}
