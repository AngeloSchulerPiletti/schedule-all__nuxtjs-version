import { verifiyIfIsMetaMaskWallet, walletConnection } from "../utils/walletConnectionManager.js";

export default async function ({ store, $userWeb3 }) {
    // Precisa acontecer na primeira chamada/reload também...

    if (!process.server) {
        verifiyIfIsMetaMaskWallet(store);
        await walletConnection(store, $userWeb3);
    }
}
