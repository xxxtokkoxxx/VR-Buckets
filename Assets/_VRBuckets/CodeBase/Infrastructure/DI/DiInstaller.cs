using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;
using _VRBuckets.CodeBase.UI.MainMenu;
using VContainer;
using VContainer.Unity;

namespace _VRBuckets.CodeBase.Infrastructure.DI
{
    public class DiInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IAssetLoaderService, AssetLoaderService>(Lifetime.Singleton);
            builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
            builder.Register<IUIViewsFactory, UIViewsFactory>(Lifetime.Singleton);
            builder.Register<IStatesFactory, StatesFactory>(Lifetime.Singleton);
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<IUIService, UIService>(Lifetime.Singleton);
            builder.Register<IViewController, MainMenuController>(Lifetime.Singleton);

            UnityEngine.Debug.Log("Configue");
        }
    }
}