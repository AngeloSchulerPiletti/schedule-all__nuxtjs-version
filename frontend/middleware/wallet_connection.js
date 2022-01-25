import { verifiyIfIsMetaMaskWallet, checkWalletConnection } from "../utils/walletConnectionManager.js";

export default async function ({ store, $userWeb3, $cookies }) {
    if (!process.server) {
        verifiyIfIsMetaMaskWallet(store);
        await checkWalletConnection(store, $userWeb3, $cookies);

        if ($cookies.get('userData')) {
            if (store.state.wallet.walletAddress != $cookies.get('userData').walletAddress) {
                store.commit("wallet/isTheConnectedWalletSigned", false);
                // DONE : Mostra aviso universal explícito avisando que a wallet conectada não é a cadastrada e isso pode limitar as ferramentas do usuario (mostrar o endereço correto da wallet)
                // DONE : O melhor seria criar algo no vuex (um atributo no store/wallet.js)
                // Realizar as features do projeto (colocar as tarefas em staking e tal pela API, isso porque senão o dApp se torna muito caro ao usuário)
            }
            else {
                store.commit("wallet/isTheConnectedWalletSigned", true);

            }
        }
    }

}
