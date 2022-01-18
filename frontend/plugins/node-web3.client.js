import Web3 from "web3";

//Using it is pretty strange

export default ({app}, inject) => {
    const nodeUrl = 'http://192.168.0.17:7545';
    inject('nodeWeb3', new Web3(nodeUrl));
}