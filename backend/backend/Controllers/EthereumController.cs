﻿using backend.Business;
using backend.Data.DTO.Ethereum;
using backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EthereumController : ControllerBase
    {
        private IEthereumBusiness _business;

        public EthereumController(IUserRepository userRepository, IEthereumBusiness business) : base(userRepository) {
            _business = business;
        }

        [HttpGet]
        [Route("ether")]
        public IActionResult Ether([FromBody] string address)
        {
            var balance = _business.GetAccountBalance(address);

            return Ok(balance);
        }

    }
}
