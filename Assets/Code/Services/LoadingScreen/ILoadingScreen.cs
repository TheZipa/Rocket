using Code.Infrastructure.ServiceContainer;

namespace Code.Services.LoadingScreen
{
    public interface ILoadingScreen : IService
    {
        void Show();
        void Hide();
    }
}