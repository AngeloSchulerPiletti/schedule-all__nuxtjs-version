using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.VO
{
    public class MessageBadgeVO
    {
        public MessageBadgeVO(List<string> messages, bool isError = true) 
        {
            this.messages = messages;
            this.isError = isError;
        }

        public List<string> messages { get; set; }
        public bool isError { get; set; }
    }
}
