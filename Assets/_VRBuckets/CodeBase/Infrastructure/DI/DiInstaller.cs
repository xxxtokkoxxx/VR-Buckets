using _VRBuckets.CodeBase.Configuration;
using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Core.GameFlow;
using _VRBuckets.CodeBase.GamePlay.Core.Preparation;
using _VRBuckets.CodeBase.GamePlay.Data;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.GamePlay.Player;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Network.Configuration;
using _VRBuckets.CodeBase.Network.Connection;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;
using _VRBuckets.CodeBase.UI.GameOver;
using _VRBuckets.CodeBase.UI.MainMenu;
using Fusion;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _VRBuckets.CodeBase.Infrastructure.DI
{
    public class DiInstaller : LifetimeScope
    {
        [SerializeField] private MonoBehavioursProvider _monoBehavioursProvider;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IAssetLoaderService, AssetLoaderService>(Lifetime.Singleton);
            builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
            builder.Register<IUIViewsFactory, UIViewsFactory>(Lifetime.Singleton);
            builder.Register<IStatesFactory, StatesProvider>(Lifetime.Singleton);
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<IUIService, UIService>(Lifetime.Singleton);
            builder.Register<IViewController, MainMenuController>(Lifetime.Singleton);
            builder.Register<IBallFactory, BallFactory>(Lifetime.Singleton);
            builder.Register<IHoopFactory, HoopFactory>(Lifetime.Singleton);
            builder.Register<IEnvironmentFactory, EnvironmentFactory>(Lifetime.Singleton);
            builder.Register<IState, BootstrapState>(Lifetime.Singleton);
            builder.Register<IState, MainMenuState>(Lifetime.Singleton);
            builder.Register<IState, GamePreparationState>(Lifetime.Singleton);
            builder.Register<IState, GameState>(Lifetime.Singleton);
            builder.Register<IGameSession, GameSession>(Lifetime.Singleton);
            builder.Register<IGameplayProcessor, GameplayProcessor>(Lifetime.Singleton);
            builder.Register<IGameplayConfiguration, GameplayConfiguration>(Lifetime.Singleton);
            builder.Register<IPlayersContainer, PlayersContainer>(Lifetime.Singleton);
            builder.Register<IGameResultsContainer, GameResultsContainer>(Lifetime.Singleton);
            builder.Register<IViewController, GameOverController>(Lifetime.Singleton);
            builder.Register<IBallLifecycleSystem, ITickable, BallLifecycleSystem>(Lifetime.Singleton);
            builder.Register<INetworkRunnerCallbacks, NetworkConnectionMessagesHandler>(Lifetime.Singleton);
            builder.Register<INetworkConnectionRunner, NetworkConnectionRunner>(Lifetime.Singleton);
            builder.Register<INetworkConfigurationProvider, NetworkConfigurationProvider>(Lifetime.Singleton);

            builder.RegisterComponent(_monoBehavioursProvider).AsImplementedInterfaces();
        }
    }
}