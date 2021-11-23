using backend.Data.VO;
using backend.Data.VO.Friendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business.Implementations
{
    public class FriendshipBusinessImplementation : IFriendshipBusiness
    {
        public MessageBadgeVO AnswerTheInvite(FriendshipAnswerVO inviteAnswer, long userId)
        {
            throw new NotImplementedException();
        }

        public MessageBadgeVO CheckIfInviteExists(long inviteId)
        {
            throw new NotImplementedException();
        }

        public MessageBadgeVO CheckIfUserExists(string friendUserName)
        {
            throw new NotImplementedException();
        }

        public FriendshipInviteVO GetAllUserInvites(long receiverId)
        {
            throw new NotImplementedException();
        }

        public long GetIdFromUserName(string friendUserName)
        {
            throw new NotImplementedException();
        }

        public MessageBadgeVO SendInvite(long senderId, long receiverId)
        {
            throw new NotImplementedException();
        }

        private List<long> GetUsersDataFromSendersId(List<long> sendersId)
        {
            throw new NotImplementedException();
        }
    }
}
