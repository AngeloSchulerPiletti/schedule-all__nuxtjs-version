
export function verifiyIfIsMetaMaskWallet(store) {
    if (store.state.wallet.hasMetaMask === null) {
        (typeof window.ethereum !== 'undefined' && window.ethereum.isMetaMask) ?
            store.commit("wallet/hasMetaMask") :
            store.commit("wallet/hasNotMetaMask");
        //Adicionar um watch para, caso a wallet seja desconectada, algo acontecer
    }

    //if (!store.state.wallet.hasMetaMask) // precisa mostrar mensagem de erro ou coisa parecida
}

export async function walletConnection(store, $userWeb3) {
    var userWallet = (await $userWeb3.eth.getAccounts())[0];

    userWallet ?
        store.dispatch("wallet/connectedWallet", userWallet) :
        store.dispatch("wallet/disconnectedWallet");
}