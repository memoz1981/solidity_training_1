using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace InfuraDeploy;

public interface IWeb3Client
{
    Task<BigInteger> GetBalance();
    Task<BigInteger> ReturnGasEstimate(Contract contract);
    Task<TransactionReceipt> Deploy(Contract contract, BigInteger gasEstimate); 
}
