using System;
using Code.Infrastructure.ServiceContainer;

namespace Code.Services.SceneLoader
{
    public interface ISceneLoader : IService
    {
        void LoadScene(string sceneName, Action onLoaded = null);
    }
}