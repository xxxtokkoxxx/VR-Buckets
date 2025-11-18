using System;
using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Debug;
using _VRBuckets.CodeBase.Infrastructure.DI;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _VRBuckets.CodeBase.UI
{
    public class UIViewsFactory : IUIViewsFactory
    {
        private IAssetLoaderService _loaderService;
        private IList<BaseView> _views;
        private List<BaseView> _activeViews = new();

        private readonly IMonoBehaviourProvider _monoBehaviourProvider;

        public UIViewsFactory(IAssetLoaderService loaderService,
             IMonoBehaviourProvider monoBehaviourProvider)
        {
            UnityEngine.Debug.Log("Creating UI Views Factory");
            _loaderService = loaderService;
            _monoBehaviourProvider = monoBehaviourProvider;
        }

        public async UniTask LoadViews()
        {
            UnityEngine.Debug.Log("load views view");
            IList<BaseView> views = await _loaderService.LoadPrefabs<BaseView>(AssetsDataPath.View);
            UnityEngine.Debug.Log("views are laoded");
            _views = views;
        }

        public TView CreateView<TView>(ViewType viewType) where TView : BaseView
        {
            UnityEngine.Debug.Log("try to create view");
            BaseView reference = _views.FirstOrDefault(a => a.ViewType == viewType);
            if (reference == null)
            {
                AppLogger.LogError(LogCategory.UI, $"View {viewType} not found");
                return null;
            }

            TView view = Object.Instantiate(reference, _monoBehaviourProvider.UIViewsParent).GetComponent<TView>();
            view.Id = Guid.NewGuid();
            Transform userCameraTransform = _monoBehaviourProvider.UserCameraTransform.transform;

            view.transform.position = userCameraTransform.position + userCameraTransform.forward;
            _activeViews.Add(view);
            return view;
        }

        public void DestroyView(Guid id)
        {
            BaseView view = _views.FirstOrDefault(a => a.Id == id);

            if (view == null)
            {
                AppLogger.LogError(LogCategory.UI, $"View with id: {id} not found");
                return;
            }

            _views.Remove(view);
            Object.Destroy(view.gameObject);
        }
    }
}