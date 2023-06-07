using Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Code.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        GameObject CreateRootCanvas();
        void CreateGameOverWindow(Transform root);
        void CreateMainMenu(Transform root);
        void CreateMeterCounterView(Transform root);
    }
}