using backend.Data.VO;
using backend.Models;

namespace backend.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string userName);
        MessageBadgeVO ValidateUserVO(UserVO user, MessageBadgeVO error);
        MessageBadgeVO ValidateNewUserVO(NewUserVO user, MessageBadgeVO error);
        bool SaveNewUserOnDB(NewUserVO user);
        MessageBadgeVO CheckIfUserAlreadyExists(NewUserVO user, MessageBadgeVO error);
        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
    }
}
