using backend.Configurations;
using backend.Data.DTO.Ethereum;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace backend.Business.Implementations
{
    public class EthereumBusiness : IEthereumBusiness
    {
        private EthereumConfiguration _configuration;
        public EthereumBusiness(EthereumConfiguration configuration)
        {
            _configuration = configuration;
        }

        public BigInteger GetAccountBalance(string address)
        {
            var web3 = new Web3(_configuration.Host);

            var balanceOfFunctionMessage = new BalanceOfFunction()
            {
                Owner = address
            };

            var balanceHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
            var balance = balanceHandler.QueryDeserializingToObjectAsync<BalanceOfOutputDTO>(balanceOfFunctionMessage, _configuration.TaskTokenContractAddress).Result;

            return balance.Balance;
        }
    }
}
