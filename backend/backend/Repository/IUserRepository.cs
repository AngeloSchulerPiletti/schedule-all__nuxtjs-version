using backend.Data.VO;
using backend.Models;
using System.Collections.Generic;

namespace backend.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User GetUserByUsername(string userName);
        User GetUserById(long id);
        MessageBadgeVO ValidateUserVO(UserVO user, MessageBadgeVO error);
        MessageBadgeVO ValidateNewUserVO(NewUserVO user);
        bool SaveNewUserOnDB(NewUserVO user);
        MessageBadgeVO CheckIfUserAlreadyExists(NewUserVO user);
        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
        List<User> GetUsersByIdList(List<long> ids);
    }
}
