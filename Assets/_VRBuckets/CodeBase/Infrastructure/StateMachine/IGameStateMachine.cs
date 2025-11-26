using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        void SetStates(Dictionary<Type, IState> states);
        UniTask Enter<TState>(object payload = null) where TState : class, IState;
    }
}