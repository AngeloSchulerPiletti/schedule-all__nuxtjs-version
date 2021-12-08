using backend.Data.VO;
using backend.Models;
using System.Collections.Generic;

namespace backend.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string userName);
        User GetUserDataFromId(long id);
        MessageBadgeVO ValidateUserVO(UserVO user, MessageBadgeVO error);
        MessageBadgeVO ValidateNewUserVO(NewUserVO user, MessageBadgeVO error);
        bool SaveNewUserOnDB(NewUserVO user);
        MessageBadgeVO CheckIfUserAlreadyExists(NewUserVO user, MessageBadgeVO error);
        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
        List<User> GetUsersDataFromIdList(List<long> ids);
    }
}
