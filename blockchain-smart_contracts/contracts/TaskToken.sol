// SPDX-License-Identifier: MIT

pragma solidity >=0.4.22 <0.9.0;

contract TaskToken{
    string public name = "TaskToken";
    address public owner;
    mapping (address => uint) public taskTokenBalance;
    mapping (address => mapping(uint256 => uint256[10])) internal activeTasks;
    // {taskOwnerAddress : {task_id1: [array de usuários ID atribuídos]}}

    constructor() {
        owner = msg.sender;
        taskTokenBalance[address(this)] = 1000000; //1.000.000
    }

    function refill(uint amount) public{
        require(msg.sender == owner, "Only the owner can refill.");
        taskTokenBalance[address(this)] += amount;
    }

    function purchase(uint amount) public payable {
        require(msg.value >= amount * 10**13 wei, "You must pay at least 10 MicroEther per tasktoken");
        require(taskTokenBalance[address(this)] >= amount, "Not enough TaskTokens in stock to complete this purchase");
        taskTokenBalance[address(this)] -= amount;
        taskTokenBalance[msg.sender] += amount;
    }


    
}
