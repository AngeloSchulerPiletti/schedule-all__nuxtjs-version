

export default async function ({ redirect, $axios, store, $userWeb3 }) {
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
        }
        else {
            console.log("wallet not connected");
            store.dispatch("wallet/disconnectedWallet");
        }
    }
}
