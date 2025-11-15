using System;
using System.Collections.Generic;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IState> _states;

        public StateMachine()
        {

        }

        public void SetStates(Dictionary<Type, IState> states)
        {
            _states = states;
        }

        public void Enter<TState>(object payload = null) where TState : class, IState
        {
            TState state = ChangeState<TState>(payload);
            state.Enter(payload);
        }

        private TState ChangeState<TState>(object payload) where TState : class, IState
        {
            TState state = GetState<TState>();

            if (state is IExitState exitState)
            {
                exitState.Exit();
            }

            state.Enter(payload);
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}