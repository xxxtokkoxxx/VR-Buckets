using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public interface IExitState : IState
    {
        UniTask Exit();
    }
}