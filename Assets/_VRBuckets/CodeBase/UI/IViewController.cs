namespace _VRBuckets.CodeBase.UI
{
    public interface IViewController
    {
        ViewType ViewType { get; }
        void Show();
        void Hide();
    }
}