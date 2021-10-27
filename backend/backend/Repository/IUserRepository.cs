using backend.Data.VO;
using backend.Models;

namespace backend.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string userName);
        ErrorBadgeVO ValidateUserVO(UserVO user, ErrorBadgeVO error);
        ErrorBadgeVO ValidateNewUserVO(NewUserVO user, ErrorBadgeVO error);
        bool SaveNewUserOnDB(NewUserVO user);
        ErrorBadgeVO CheckIfUserAlreadyExists(NewUserVO user, ErrorBadgeVO error);
        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
    }
}
