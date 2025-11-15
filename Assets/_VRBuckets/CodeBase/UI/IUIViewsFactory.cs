using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.UI
{
    public interface IUIViewsFactory
    {
        UniTask LoadViews();
        IView CreateView(ViewType viewType);
    }
}