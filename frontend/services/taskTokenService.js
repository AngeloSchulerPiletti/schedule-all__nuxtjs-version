import Web3 from "web3";

export default class taskTokenService {
    static async sendUserSignedUp(contract, userName, walletAddress) {
        await contract.methods
            .userSignedUp(userName)
            .send(
                { from: walletAddress },
                function (error, transactionHash) {
                    console.log(error, transactionHash)
                }
            )
    }

    static async eventUserSignedUp(contract, wallet = null) {
        let eventFilter = wallet ? {
            filter: { _user: wallet },
            fromBlock: 0,
            toBlock: 'latest'
        } : {
            fromBlock: 0,
            toBlock: 'latest'
        };
        return await contract.getPastEvents('SignedUpUser', eventFilter);
    }
}