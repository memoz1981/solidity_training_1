using System.Numerics;

namespace InfuraDeploy;

public interface IWeb3Client
{
    Task<BigInteger> GetBalance(); 
}
