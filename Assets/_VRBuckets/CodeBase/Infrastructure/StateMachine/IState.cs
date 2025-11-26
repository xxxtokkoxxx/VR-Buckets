using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public interface IState
    {
        UniTask Enter(object payload = null);
    }
}