using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using UnityEngine;
using VContainer;

namespace _VRBuckets.CodeBase.Infrastructure.Bootstrap
{
    public class AppBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        private IStatesFactory _statesFactory;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IStatesFactory statesFactory)
        {
            UnityEngine.Debug.Log("inject");
            _statesFactory = statesFactory;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Dictionary<Type, IState> states = _statesFactory.CreateStates();
            _gameStateMachine.SetStates(states);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}