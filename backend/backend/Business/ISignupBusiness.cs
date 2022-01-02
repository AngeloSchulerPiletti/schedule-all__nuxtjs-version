using backend.Data.VO;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business
{
    public interface ISignupBusiness
    {
        public UserDataVO AuthTheNewUser(User freshUser);
        public MessageBadgeVO CreateNewUser(NewUserVO user);
        public MessageBadgeVO CheckIfUserAlreadyExists(NewUserVO user);
        public User GetTheNewUser(string username);
        public MessageBadgeVO ValidateInputs(NewUserVO user);
    }
}
