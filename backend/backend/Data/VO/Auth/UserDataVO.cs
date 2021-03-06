using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.VO
{
    public class UserDataVO
    {
        public UserDataVO(string userName, string fullName, string email, string walletAddress, TokenVO tokenData)
        {
            UserName = userName;
            FullName = fullName;
            Email = email;
            WalletAddress = walletAddress;
            this.tokenData = tokenData;
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string WalletAddress { get; set; }
        public TokenVO tokenData { get; set; }
    }
}
