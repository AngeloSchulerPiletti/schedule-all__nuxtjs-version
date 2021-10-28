using backend.Data.VO;

namespace backend.Business
{
    public interface ILoginBusiness
    {
        object ValidateCredentials(UserVO user);
        UserDataVO ValidateCredentials(TokenVO tokenVo);
        bool RevokeToken(string userName);
    }
}
