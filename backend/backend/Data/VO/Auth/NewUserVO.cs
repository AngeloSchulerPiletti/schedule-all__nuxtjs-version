
using System.Collections.Generic;

namespace backend.Data.VO
{
    public class NewUserVO
    {
        private Dictionary<string, string> _inputsName = new() { 
            { "Password", "senha" }, 
            { "UserName", "nickname" },
            { "PasswordConfirmation", "confirmação de senha" },
            { "Email", "email" },
            { "FullName", "nome completo" },
            { "WalletAddress", "endereço da wallet" }
        };

        public Dictionary<string, string> inputsName { get => _inputsName; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string WalletAddress { get; set; }
    }
}
