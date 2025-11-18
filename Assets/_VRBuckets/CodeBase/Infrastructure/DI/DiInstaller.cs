using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;
using _VRBuckets.CodeBase.UI.MainMenu;
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
            builder.Register<IStatesFactory, StatesFactory>(Lifetime.Singleton);
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<IUIService, UIService>(Lifetime.Singleton);
            builder.Register<IViewController, MainMenuController>(Lifetime.Singleton);
            builder.Register<IBallFactory, BallFactory>(Lifetime.Singleton);
            builder.Register<IHoopFactory, HoopFactory>(Lifetime.Singleton);
            builder.Register<IEnvironmentFactory, EnvironmentFactory>(Lifetime.Singleton);

            builder.RegisterComponent(_monoBehavioursProvider).AsImplementedInterfaces();
        }
    }
}