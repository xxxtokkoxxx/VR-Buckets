using System;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.UI
{
    public interface IUIViewsFactory
    {
        UniTask LoadViews();
        TView CreateView<TView>(ViewType viewType) where TView : BaseView;
        void DestroyView(Guid id);
    }
}