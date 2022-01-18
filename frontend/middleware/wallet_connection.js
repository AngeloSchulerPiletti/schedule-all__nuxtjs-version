

export default async function (app = { redirect, $axios, store, $userWeb3 }) {
    // Precisa acontecer na primeira chamada/reload também...
    if (!process.server) {
        if (store.state.wallet.hasMetaMask === null) {
            (typeof window.ethereum !== 'undefined' && window.ethereum.isMetaMask) ?
                store.commit("wallet/hasMetaMask") :
                store.commit("wallet/hasNotMetaMask");
        }

        //if (!store.state.wallet.hasMetaMask) // precisa mostrar mensagem de erro ou coisa parecida

        var userWallet = (await $userWeb3.eth.getAccounts())[0];
        // console.log(userWallet);
        if (userWallet) {
            console.log("wallet connected");
            store.dispatch("wallet/connectedWallet", userWallet);
            // store.commit('wallet/setWalletAddress', userWallet);
            // store.commit('wallet/setWalletConnection', true);
        }
        else {
            console.log("wallet not connected");
            store.dispatch("wallet/disconnectedWallet");
            // store.commit('wallet/setWalletAddress', null);
            // store.commit('wallet/setWalletConnection', false);
        }
    }
}
