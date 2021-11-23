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
        FriendshipInviteVO GetAllUserInvites(long receiverId);
        MessageBadgeVO AnswerTheInvite(FriendshipAnswerVO inviteAnswer, long userId);

    }
}
