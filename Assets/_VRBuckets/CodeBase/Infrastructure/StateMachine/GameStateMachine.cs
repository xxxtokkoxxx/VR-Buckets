using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine, IInitializable
    {
        private Dictionary<Type, IState> _states;

        public GameStateMachine()
        {
            UnityEngine.Debug.Log("asd");
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

        public void Initialize()
        {
            UnityEngine.Debug.Log("dsa");
        }
    }
}