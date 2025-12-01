using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;

namespace InfuraDeploy;

public class Web3Client : IWeb3Client
{
    private readonly Configuration _configuration;

    public Web3Client(Configuration configuration)
    {
        _configuration = configuration;
    }

    public async Task<BigInteger> GetBalance()
    {
        var chainId = _configuration.ChainId;
        var privateKey = _configuration.PrivateKey;
        var url = _configuration.Url;
        var account = new Account(privateKey, chainId);
        var web3 = new Web3(account, url);

        var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);

        return balance.Value; 
    }

    public async Task<BigInteger> ReturnGasEstimate(Contract contract)
    {
        var chainId = _configuration.ChainId;
        var privateKey = _configuration.PrivateKey;
        var url = _configuration.Url;
        var account = new Account(privateKey, chainId);
        var web3 = new Web3(account, url);

        var constructorParams = new object[] { new string[] { "coffee", "tea" } };

        var gasEstimate = await web3.Eth.DeployContract.EstimateGasAsync(
                contract.Abi, contract.ByteCode, account.Address, constructorParams);

        return gasEstimate.Value; 
    }

    public async Task<TransactionReceipt> Deploy(Contract contract, BigInteger gasEstimate)
    {
        var chainId = _configuration.ChainId;
        var privateKey = _configuration.PrivateKey;
        var url = _configuration.Url;
        var account = new Account(privateKey, chainId);
        var web3 = new Web3(account, url);

        var constructorParams = new object[] { new string[] { "coffee", "tea" } };

        // Deploy contract and wait for receipt
        var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(
            abi: contract.Abi,
            contractByteCode: contract.ByteCode, 
            from: account.Address, 
            gas: new HexBigInteger(gasEstimate),
            receiptRequestCancellationToken: null,
            values: constructorParams);

        return receipt; 
    }
}
