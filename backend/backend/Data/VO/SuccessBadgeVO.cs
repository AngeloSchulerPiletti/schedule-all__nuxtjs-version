using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.VO
{
    public class SuccessBadgeVO
    {
        public SuccessBadgeVO(List<string> messages)
        {
            this.messages = messages;
        }

        public List<string> messages { get; set; }
    }
}
