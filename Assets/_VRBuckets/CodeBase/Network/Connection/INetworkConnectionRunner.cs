using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;

namespace _VRBuckets.CodeBase.Network.Connection
{
    public interface INetworkConnectionRunner
    {
        UniTask Connect(GameMode gameMode, CancellationToken token);
    }
}