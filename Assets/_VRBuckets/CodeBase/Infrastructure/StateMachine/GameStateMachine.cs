using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _previousState;

        public void SetStates(Dictionary<Type, IState> states)
        {
            _states = states;
        }

        public void Enter<TState>(object payload = null) where TState : class, IState
        {
            ChangeState<TState>(payload);
        }

        private void ChangeState<TState>(object payload) where TState : class, IState
        {
            TState state = GetState<TState>();

            if (_previousState is IExitState exitState)
            {
                exitState.Exit();
            }

            state.Enter(payload);
            _previousState = state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}