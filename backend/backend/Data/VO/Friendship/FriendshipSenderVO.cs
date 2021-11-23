using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.VO.Friendship
{
    public class FriendshipSenderVO
    {
        public FriendshipSenderVO(long senderId, long friendshipId)
        {
            SenderId = senderId;
            FriendshipId = friendshipId;
        }
        public long SenderId { get; set; }
        public long FriendshipId { get; set; }
    }
}
