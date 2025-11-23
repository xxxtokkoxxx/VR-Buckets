using System;
using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Infrastructure.DI;
using _VRBuckets.CodeBase.Infrastructure.Factory;
using _VRBuckets.CodeBase.Logging;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _VRBuckets.CodeBase.UI
{
    public class UIViewsFactory : BaseFactory, IUIViewsFactory
    {
        private IAssetLoaderService _loaderService;
        private IList<BaseView> _viewReferences;
        private List<BaseView> _activeViews = new();

        private readonly IMonoBehaviourProvider _monoBehaviourProvider;

        public UIViewsFactory(IAssetLoaderService loaderService,
            IMonoBehaviourProvider monoBehaviourProvider)
        {
            Debug.Log("Creating UI Views Factory");
            _loaderService = loaderService;
            _monoBehaviourProvider = monoBehaviourProvider;
        }

        public async UniTask LoadViews()
        {
            Debug.Log("load views view");
            IList<BaseView> views = await _loaderService.LoadPrefabs<BaseView>(AssetsDataPath.View);
            Debug.Log("views are laoded");
            _viewReferences = views;
        }

        public TView CreateView<TView>(ViewType viewType) where TView : BaseView
        {
            BaseView reference = _viewReferences.FirstOrDefault(a => a.ViewType == viewType);
            if (reference == null)
            {
                AppLogger.LogError(LogCategory.UI, $"View {viewType} not found");
                return null;
            }

            TView view = Create(reference, _monoBehaviourProvider.UIViewsParent).GetComponent<TView>();
            view.Id = Guid.NewGuid();
            Debug.Log("try to create view " + view.Id);

            _activeViews.Add(view);
            return view;
        }

        public void DestroyView(Guid id)
        {
            Debug.Log("call destroy " + id);
            BaseView view = _activeViews.FirstOrDefault(a => a.Id == id);

            if (view == null)
            {
                AppLogger.LogError(LogCategory.UI, $"View with id: {id} not found");
                return;
            }

            _activeViews.Remove(view);
            Object.Destroy(view.gameObject);
        }
    }
}