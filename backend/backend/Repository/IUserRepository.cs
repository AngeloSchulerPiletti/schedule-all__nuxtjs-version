using backend.Data.VO;
using backend.Models;

namespace backend.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string userName);
        bool SaveNewUserOnDB(NewUserVO user);
        bool CheckIfUserAlreadyExists(NewUserVO user);
        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
    }
}
