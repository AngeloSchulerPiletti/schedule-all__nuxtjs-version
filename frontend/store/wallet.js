export const state = () => ({
    walletAddress: null,
    hasMetaMask: null,
    connected: false,
    isTheConnectedWalletSigned: null,
});

export const mutations = {
    isTheConnectedWalletSigned(state, payload){
        state.isTheConnectedWalletSigned = payload;
    },
    setWalletAddress(state, address){
        state.walletAddress = address;
    },
    setWalletConnection(state, status){
        state.connected = status;
    },
    hasMetaMask(state){
        state.hasMetaMask = true;
    },
    hasNotMetaMask(state){
        state.hasMetaMask = false;
    },
};

export const actions = {
    connectedWallet({commit}, {address, isSignedWallet}){
        commit("setWalletAddress", address);
        commit("setWalletConnection", true);
        commit("isTheConnectedWalletSigned", isSignedWallet)
    },
    disconnectedWallet({commit}){
        commit("setWalletAddress", null);
        commit("setWalletConnection", false);
        commit("isTheConnectedWalletSigned", null)
    },
};