using Code.Core.UI;
using Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Code.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        GameObject CreateRootCanvas();
        GameOverWindow CreateGameOverWindow(Transform root);
    }
}