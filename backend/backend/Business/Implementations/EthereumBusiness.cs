using backend.Configurations;
using backend.Data.DTO.Ethereum;
using backend.Data.VO;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
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
        private Web3 _anonymousWeb3;
        // private Web3 _accountedWeb3
        public EthereumBusiness(EthereumConfiguration configuration)
        {
            _configuration = configuration;
            _anonymousWeb3 = new Web3(configuration.Host);
        }

        public BigInteger GetAccountBalance(string address)
        {
            var balanceOfFunctionMessage = new BalanceOfFunction()
            {
                Owner = address
            };

            var balanceHandler = _anonymousWeb3.Eth.GetContractQueryHandler<BalanceOfFunction>();
            var balance = balanceHandler.QueryDeserializingToObjectAsync<BalanceOfOutputDTO>(balanceOfFunctionMessage, _configuration.TaskTokenContractAddress).Result;

            return balance.Balance;
        }

        public bool CheckIfTaskIsInStaking(long taskId)
        {
            var taskIsInStakingFunctionMessage = new TaskIsInStakingFunction()
            {
                TaskId = taskId
            };

            var taskIsInStakingHandler = _anonymousWeb3.Eth.GetContractQueryHandler<TaskIsInStakingFunction>();
            var taskIsInStaking = taskIsInStakingHandler.QueryDeserializingToObjectAsync<TaskIsInStakingOutputDTO>(taskIsInStakingFunctionMessage, _configuration.TaskTokenContractAddress).Result;

            return taskIsInStaking.IsInStaking;
        }

        public MessageBadgeVO CheckIfSignUpWasEmitted(string userName, string walletAddress)
        {
            var signedUpEventHandler = _anonymousWeb3.Eth.GetEvent<SignedUpUserEventDTO>(_configuration.TaskTokenContractAddress);

            var filterSignedUpUserEvents = signedUpEventHandler.CreateFilterInput(walletAddress);
            var signedUpUserEvents = signedUpEventHandler.GetAllChangesAsync(filterSignedUpUserEvents);
            var signedUpUserEventsResponse = signedUpUserEvents.Result;


            if(signedUpUserEventsResponse.Count > 0)
            {
                for (int i = 0; i < signedUpUserEventsResponse.Count; i++)
                {
                    if(signedUpUserEventsResponse[i].Event.UserName == userName)
                    {
                        return new MessageBadgeVO(new List<string> { "O nickname corresponde ao nickname emitido pelo contrato" }, false);
                    }
                }
            }
            return new MessageBadgeVO(new List<string> { "O nickname enviado não corresponde ao nickname pago" });
        }
    }
}