using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.DTO.Ethereum
{
    [Function("taskIsInStaking", "bool")]
    public class TaskIsInStakingFunction : FunctionMessage
    {
        [Parameter("uint256", "_taskId", 1)]
        public long TaskId { get; set; }
    }
}
