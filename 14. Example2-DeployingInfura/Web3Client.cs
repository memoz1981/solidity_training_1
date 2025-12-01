
using Nethereum.Hex.HexTypes;
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
}
