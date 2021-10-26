using backend.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business
{
    public interface ISignupBusiness
    {
        TokenVO CreateNewUser(NewUserVO user);
    }
}
