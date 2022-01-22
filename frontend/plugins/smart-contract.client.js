import Web3 from "web3";

export default ({app, $config: {contractAddress, contractJsonPath, networkAddress}}, inject) => {
    const client = window.ethereum; //Se o usuário não tiver uma wallet, pode dar problema
    // var contractJson = require(`../${contractJsonPath}`); // Não está funcionando corretamente por conta do webpack
    var contractJson = require('../interface/TaskToken.json');
    var smartContract = new (new Web3(client)).eth.Contract(contractJson.abi, contractAddress);
    inject('smartContract', smartContract);
}