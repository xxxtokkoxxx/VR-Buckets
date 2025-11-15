using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Debug;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.UI
{
    public class UIViewsFactory : IUIViewsFactory
    {
        private IAssetLoaderService _loaderService;
        private IList<IView> _views;

        public UIViewsFactory(IAssetLoaderService loaderService)
        {
            UnityEngine.Debug.Log("Creating UI Views Factory");
            _loaderService = loaderService;
        }

        public async UniTask LoadViews()
        {
            IList<IView> views = await _loaderService.LoadPrefabs<IView>(AssetsDataPath.View);
            _views = views;
        }

        public IView CreateView(ViewType viewType)
        {
            IView view = _views.FirstOrDefault(a => a.ViewType == viewType);

            if (view == null)
            {
                AppLogger.LogError(LogCategory.UI, $"View {viewType} not found");
                return null;
            }

            return view;
        }
    }
}