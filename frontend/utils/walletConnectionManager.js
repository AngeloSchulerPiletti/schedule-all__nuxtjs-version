
export function verifiyIfIsMetaMaskWallet(store) {
    if (store.state.wallet.hasMetaMask === null) {
        (typeof window.ethereum !== 'undefined' && window.ethereum.isMetaMask) ?
            store.commit("wallet/hasMetaMask") :
            store.commit("wallet/hasNotMetaMask");
    }

    //if (!store.state.wallet.hasMetaMask) // precisa mostrar mensagem de erro ou coisa parecida
}

export async function walletConnection(store, $userWeb3) {
    var userWallet = (await $userWeb3.eth.getAccounts())[0];

    userWallet ?
        store.dispatch("wallet/connectedWallet", userWallet) :
        store.dispatch("wallet/disconnectedWallet");
}

export async function connectWallet(userWeb3, store) {
    await userWeb3.eth
        .requestAccounts()
        .then((accounts) => {
            let walletAddress = accounts[0]
            store.dispatch('wallet/connectedWallet', walletAddress)
        })
        .catch((err) => {
            console.log(err.response);
        })
}