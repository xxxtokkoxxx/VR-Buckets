using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;
using UnityEngine;
using VContainer;

namespace _VRBuckets.CodeBase.Infrastructure.Bootstrap
{
    public class AppBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        private IStatesFactory _statesFactory;
        private IUIService _uiService;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IStatesFactory statesFactory, IUIService uiService)
        {
            _uiService = uiService;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _uiService.Show(ViewType.MainMenu);
            }
        }
    }
}