using System;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IUIService _uiService;

        public MainMenuState(ISceneLoaderService sceneLoaderService, IUIService uiService)
        {
            _sceneLoaderService = sceneLoaderService;
            _uiService = uiService;
        }

        public void Enter(object payload)
        {
            _sceneLoaderService.LoadScene(SceneNames.MainMenu);
            _uiService.Show(ViewType.MainMenu);
        }
    }
}