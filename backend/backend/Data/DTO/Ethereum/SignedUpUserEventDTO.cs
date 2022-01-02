using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace backend.Data.DTO.Ethereum
{
    [Event("SignedUpUser")]
    public class SignedUpUserEventDTO : IEventDTO
    {
        [Parameter("address", "_user", 1, true)]
        public string User { get; set; }

        [Parameter("string", "_username", 1, true)]
        public string UserName { get; set; }
    }
}
