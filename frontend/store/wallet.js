export const state = () => ({
    walletAddress: null,
    hasMetaMask: null,
    connected: false,
});

export const mutations = {
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
    connectedWallet({commit}, address){
        commit("setWalletAddress", address);
        commit("setWalletConnection", true);
    },
    disconnectedWallet({commit}){
        commit("setWalletAddress", null);
        commit("setWalletConnection", false);
    },
};