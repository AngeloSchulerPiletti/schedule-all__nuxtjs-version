import Web3 from "web3";

export default ({app}, inject) => {
    const user = window.ethereum; //Se o usuário não tiver uma wallet, pode dar problema
    // Adicionar endereço de wallet do user como default
    inject('userWeb3', new Web3(user));
}