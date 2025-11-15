using System;
using System.Collections.Generic;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        void SetStates(Dictionary<Type, IState> states);
        void Enter<TState>(object payload = null) where TState : class, IState;
    }
}