
using System.Collections.Generic;

namespace backend.Data.VO
{
    public class UserVO
    {
        public UserVO()
        {
            this.inputsName = new();
            this.inputsName.Add("Password", "senha");
            this.inputsName.Add("UserName", "nickname ou email");
        }

        public Dictionary<string, string> inputsName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
