
using System.Collections.Generic;

namespace backend.Data.VO
{
    public class NewUserVO
    {
        public NewUserVO()
        {
            this.inputsName = new();
            this.inputsName.Add("Password", "senha");
            this.inputsName.Add("UserName", "nickname");
            this.inputsName.Add("PasswordConfirmation", "confirmação de senha");
            this.inputsName.Add("Email", "email");
            this.inputsName.Add("FullName", "nome completo");
        }

        public Dictionary<string, string> inputsName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
