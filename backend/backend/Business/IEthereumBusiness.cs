using backend.Data.DTO.Ethereum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace backend.Business
{
    public interface IEthereumBusiness
    {
        public BigInteger GetAccountBalance(string address);
        public bool CheckIfTaskIsInStaking(long taskId);
    }
}
