import { verifiyIfIsMetaMaskWallet, walletConnection } from "../utils/walletConnectionManager.js";

export default async function ({ store, $userWeb3, redirect, $cookies, error }) {
    if (store.state.wallet.walletAddress != $cookies.get('userData').walletAddress) {
        // Mostra aviso universal explícito avisando que a wallet conectada não é a cadastrada e isso pode limitar as ferramentas do usuario (mostrar o endereço correto da wallet)
        // O melhor seria criar algo no vuex (um atributo no store/wallet.js)
        // Realizar as features do projeto (colocar as tarefas em staking e tal pela API, isso porque senão o dApp se torna muito caro ao usuário)
    }
}
