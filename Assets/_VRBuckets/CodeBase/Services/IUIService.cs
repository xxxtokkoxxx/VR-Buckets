using _VRBuckets.CodeBase.UI;

namespace _VRBuckets.CodeBase.Services
{
    public interface IUIService
    {
        void Initialize(IViewController[] viewControllers);
        void Show(ViewType viewType);
        void Hide(ViewType viewType);
    }
}