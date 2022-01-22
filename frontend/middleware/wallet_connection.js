import { verifiyIfIsMetaMaskWallet, walletConnection } from "../utils/walletConnectionManager.js";

export default function ({ store, $userWeb3 }) {
    // Precisa acontecer na primeira chamada/reload também...

    if (!process.server) {
        verifiyIfIsMetaMaskWallet(store);
        walletConnection(store, $userWeb3);
    }
}
