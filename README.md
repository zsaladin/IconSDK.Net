# ICON SDK for .Net

ICON SDK for .Net supports to communicate ICON network. It will help you integrate your application in .Net with ICON network. It follows [ICON JSON-RPC API v3](https://github.com/icon-project/icon-rpc-server/blob/master/docs/icon-json-rpc-v3.md).

## Quick start
```C#
using IconSDK.Account;

// Create new wallet.
// It will have random private key if you do not privide.
var wallet = Wallet.Create();

// Get your balance.
// It will retrieve the value from Testnet if you do not specify network.
var balance = await wallet.GetBalance();

// Transfer ICX
// It will transfer the value in Testnet if you do not specify network.
Address to = "hx0000000000000000000000000000000000000000";
BigInteger amount = 10 * Consts.Loop2ICX;
BigInteger stepLimit = 1 * Consts.Loop2ICX;
await wallet.Transfer(to, amount, stepLimit);

// Specify network.
wallet.ApiUrl = Consts.ApiUrl.MainNet;

// Store your key with password.
wallet.Store("yourPassword", "yourKeystorePath");

// Load your wallet from keystore.
wallet = Wallet.Load("yourPassword", "yourKeystorePath");
```

## Installation

### Referencing this project as a library.
```Shell
$ git clone https://github.com/zsaladin/IconSDK.Net
$ mkdir myproject
$ cd myproject
$ dotnet new console
$ dotnet add reference ../IconSDK.Net/IconSDK/IconSDK.csproj
```
### Getting DLL files
```Shell
$ git clone https://github.com/zsaladin/IconSDK.Net
$ cd IconSDK.Net
$ dotnet restore
$ dotnet test IconSDK.Tests
$ dotnet publish --configuration Release
```

## RPC
There are two methods to do it.
```C#
using IconSDK.RPCs;

// First
var getBalance = GetBalance.Create(Consts.ApiUrl.TestNet);
var balance = await getBalance("hx0000000000000000000000000000000000000000");
```

```C#
using IconSDK.RPCs;

// Second
var getBalance = new GetBalance(Consts.Api.TestNet);
var balance = await getBalance.Invoke("hx0000000000000000000000000000000000000000");
```

#### Support
- icx_getLastBlock
- icx_getBlockByHeight
- icx_getBlockByHash
- icx_call
- icx_getBalance
- icx_getScoreApi
- icx_getTotalSupply
- icx_getTransactionResult
- icx_getTransactionByHash
- icx_sendTransaction
```C#
using IconSDK.RPCs;

// GetLastBlock
var getLastBlock = GetLastBlock.Create(Consts.ApiUrl.TestNet);
var block = await getLastBlock();

// GetTotalSupply
var getTotalSupply = new GetTotalSupply(Consts.ApiUrl.TestNet);
var totalSupply = await getTotalSupply.Invoke();

// SendTransaction
var txBuilder = new TransactionBuilder();
txBuilder.PrivateKey = PrivateKey.Random();  // Your private key
txBuilder.To = "hx0000000000000000000000000000000000000000";
txBuilder.Value = 10 * Consts.Loop2ICX;
txBuilder.StepLimit = 1 * Consts.Loop2ICX;;
txBuilder.NID = 2;
var tx = txBuilder.Build();
var sendTransaction = new SendTransaction(Consts.ApiUrl.TestNet);

// It will raise an exception if your address does not have ICX enough.
var txHash = await sendTransaction.Invoke(tx);

// GetScoreApi
var getScoreApi = new GetScoreApi(Consts.ApiUrl.TestNet);
var scoreApi = await getScoreApi.Invoke("cx0000000000000000000000000000000000000001");

Console.WriteLine(JsonConvert.SerializeObject(scoreApi, Formatting.Indented));

var privateKey = PrivateKey.Random();
var address = Addresser.Create(privateKey);

// Call
var call = new Call(Consts.ApiUrl.TestNet);
var result = await call.Invoke(
    address,
    "cx0000000000000000000000000000000000000001",
    "isDeployer",
    ("address", address)
);

// 0x0
Console.WriteLine(result);

var call0 = new Call<bool>(Consts.ApiUrl.TestNet);
var result0 = await call0.Invoke(
    address,
    "cx0000000000000000000000000000000000000001",
    "isDeployer",
    ("address", address)
);

// false
Console.WriteLine(result0);

var call1 = new Call<BigInteger>(Consts.ApiUrl.TestNet);
var result1 = await call1.Invoke(
    address,
    "cx0000000000000000000000000000000000000001",
    "getStepPrice"
);

Console.WriteLine(result1);

var call2 = new Call<Dictionary<string, BigInteger>>(Consts.ApiUrl.TestNet);
var result2 = await call2.Invoke(
    address,
    "cx0000000000000000000000000000000000000001",
    "getStepCosts"
);

Console.WriteLine(JsonConvert.SerializeObject(result2));
```

## Requirements
- [dotnet core](https://docs.microsoft.com/dotnet/core/)
  - [System.Collections.Immutable](https://www.nuget.org/packages/System.Collections.Immutable)
  - [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
  - [nethereum.keystore](https://www.nuget.org/packages/Nethereum.KeyStore/)

### TODO
- Nuget
- Unity Asset store
- Transaction Improvement(Deploy, Call, Message)
- Sync RPC
- Query v2 tx