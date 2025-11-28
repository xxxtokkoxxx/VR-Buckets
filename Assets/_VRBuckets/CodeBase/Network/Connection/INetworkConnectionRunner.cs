using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;

namespace _VRBuckets.CodeBase.Network.Connection
{
    public interface INetworkConnectionRunner
    {
        void Initialize(NetworkRunner networkRunner);
        UniTask Connect(CancellationToken token);
    }
}