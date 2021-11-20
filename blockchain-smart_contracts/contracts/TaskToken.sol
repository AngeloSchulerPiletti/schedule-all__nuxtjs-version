// SPDX-License-Identifier: MIT

pragma solidity >=0.4.22 <0.9.0;

contract TaskToken{
    string public message;

    constructor(){
        message = "Heyoo";
    }

    function displayMessage() public view returns(string memory){
        return message;
    }
}