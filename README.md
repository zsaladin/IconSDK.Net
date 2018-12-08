# ICON SDK for .Net

ICON SDK for .Net supports to communicate ICON network. It will help you integrate your application in .Net with ICON network. It follows [ICON JSON-RPC API v3](https://github.com/icon-project/icon-rpc-server/blob/master/docs/icon-json-rpc-v3.md).

## Quick start
```C#
// Create new wallet.
// It will have random private key if you do not privide.
var wallet = Wallet.Create();

// Get your balance.
// It will retrieve the value from Testnet if you do not specify network.
var balance = await wallet.GetBalance();

// Store your key with password
wallet.Store("yourPassword", "yourKeystorePath");
```

## Installation
```Shell
$ git clone https://github.com/zsaladin/IconSDK.Net
$ cd IconSDK.Net
$ dotnet restore
$ dotnet test IconSDK.Tests
$ dotnet publish --configuration Release
```
You will get DLL files. (./IconSDK/bin/Release/netstandard2.0/publish). They have to be referenced from your application.

## RPC
There are two methods to do it.
```C#
// First
var getBalance = GetBalance.Create(Consts.ApiUrl.TestNet);
var balance = await getBalance("hx0000000000000000000000000000000000000000");

// Second
var getBalance = new GetBalance(Consts.Api.TestNet);
var balance = await getBalance.Invoke("hx0000000000000000000000000000000000000000");
```

```C#
var getLastBlock = GetLastBlock.Create(Consts.ApiUrl.TestNet);
var block = await getLastBlock();

var getScoreApi = new GetScoreApi(Consts.Api.Url.TestNet);
var scoreApi = await getScoreApi("cx0000000000000000000000000000000000000001");

var txBuilder = new TransactionBuilder();
txBuilder.PrivateKey = PrivateKey.Random();  // Your private key
txBuilder.To = "hx0000000000000000000000000000000000000000";
txBuilder.Value = 10 * Consts.Loop2ICX;
txBuilder.StepLimit = 1 * Consts.Loop2ICX;;
txBuilder.NID = 2;
var tx = txBuilder.Build();
var sendTransaction = SendTransaction.Create(Consts.ApiUrl.TestNet);
var txHash = await sendTransaction(tx); // It will raise exception if your address does not have ICX enough.
```


## Requirements
- [dotnet core](https://docs.microsoft.com/dotnet/core/)
  - [System.Collections.Immutable](https://www.nuget.org/packages/System.Collections.Immutable)
  - [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
  - [nethereum.keystore](https://www.nuget.org/packages/Nethereum.KeyStore/)

### TODO
- Nuget
- Unity Asset store
- Sync RPC
- Query v2 tx