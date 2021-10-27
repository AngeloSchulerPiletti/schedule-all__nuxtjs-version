using backend.Data.VO;

namespace backend.Business
{
    public interface ILoginBusiness
    {
        object ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
