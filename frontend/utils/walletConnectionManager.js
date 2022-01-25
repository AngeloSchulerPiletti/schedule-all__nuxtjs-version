
export function verifiyIfIsMetaMaskWallet(store) {
    if (store.state.wallet.hasMetaMask === null) {
        (typeof window.ethereum !== 'undefined' && window.ethereum.isMetaMask) ?
            store.commit("wallet/hasMetaMask") :
            store.commit("wallet/hasNotMetaMask");
    }

    //if (!store.state.wallet.hasMetaMask) // precisa mostrar mensagem de erro ou coisa parecida
}

export async function checkWalletConnection(store, $userWeb3, $cookies) {
    var userWallet = (await $userWeb3.eth.getAccounts())[0];
    let isSignedWallet = $cookies.get('userData') ? $cookies.get('userData').walletAddress == userWallet : false;

    userWallet ?
        store.dispatch("wallet/connectedWallet", { address: userWallet, isSignedWallet: isSignedWallet }) :
        store.dispatch("wallet/disconnectedWallet");
}

export async function connectWallet(userWeb3, store, $cookies) {
    await userWeb3.eth
        .requestAccounts()
        .then((accounts) => {
            let walletAddress = accounts[0]
            let isSignedWallet = $cookies.get('userData') ? $cookies.get('userData').walletAddress == walletAddress : false;

            store.dispatch('wallet/connectedWallet', { address: walletAddress, isSignedWallet: isSignedWallet })
        })
        .catch((err) => {
            console.log(err);
        })
}