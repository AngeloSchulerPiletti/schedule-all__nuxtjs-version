# Schedule-All

## Important Instructions
#### Intro
The commands mentioned about should run on the machine responsible for running Ganache and handling the Blockchain settings, abis, and source code (you can find those things [here](https://github.com/AngeloSchulerPiletti/schedule-all_blockchain-network)).

<br/>

### Contract Address
Every time your contract address is updated, either for running `truffle migrate --reset` or just instancing a new Ganache workspace you must update the address in the following places:
- C# API: `appsettings.json`
- Front-end: `.env`

### Contract Abi
Every time your contract is updated and you run `truffle compile`, `truffle migrate --reset`, or any other cmd that changes the actual ABI, you must update de contract abi in the following places:
- Front-end: `/interfaces/XXXX.json`

### Ganache IP Address
Check out your browser wallet to be sure you are connected to your local Ethereum Network (Ganache). If you're not, go to the wallet settings, then network, and update the URL there.
