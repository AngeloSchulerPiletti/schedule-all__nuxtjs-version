import { verifiyIfIsMetaMaskWallet, checkWalletConnection } from "../utils/walletConnectionManager.js";

export default function ({ app, store }) {
    app.router.onReady(async () => {
        verifiyIfIsMetaMaskWallet(store);
        await checkWalletConnection(store, app.$userWeb3, app.$cookies);
    })
}