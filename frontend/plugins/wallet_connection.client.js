import { verifiyIfIsMetaMaskWallet, walletConnection } from "../utils/walletConnectionManager.js";

export default function ({ app, store }) {
    app.router.onReady(async () => {
        verifiyIfIsMetaMaskWallet(store);
        await walletConnection(store, app.$userWeb3);
    })
}