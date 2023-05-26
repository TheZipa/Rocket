using Code.Core.UI;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticData _staticData;

        public UIFactory(IStaticData staticData) => _staticData = staticData;
        
        public GameObject CreateRootCanvas() => Object.Instantiate(_staticData.Prefabs.RootCanvasPrefab);

        public GameOverWindow CreateGameOverWindow(Transform root) =>
            Object.Instantiate(_staticData.Prefabs.GameOverWindowPrefab, root);
    }
}