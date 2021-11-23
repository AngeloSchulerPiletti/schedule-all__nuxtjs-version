using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.VO.Friendship
{
    public class FriendshipInviteVO
    {
        public FriendshipInviteVO(string userName, string fullName, long friendshipId)
        {
            UserName = userName;
            FullName = fullName;
            FriendshipId = friendshipId;
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public long FriendshipId { get; set; }
    }
}
