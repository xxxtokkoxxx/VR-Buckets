using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.VersionControl;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        void SetStates(Dictionary<Type, IState> states);
        void Enter<TState>(object payload = null) where TState : class, IState;
    }
}