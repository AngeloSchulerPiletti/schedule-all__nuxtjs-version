using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace backend.Data.DTO.Ethereum
{
    [FunctionOutput]
    public class TaskIsInStakingOutputDTO : IFunctionOutputDTO
    {
        [Parameter("bool", "isInStaking", 1)]
        public bool IsInStaking { get; set; }
    }
}
