namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public interface IState
    {
        void Enter(object payload);
    }
}