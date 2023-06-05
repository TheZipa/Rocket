using Code.Services.EntityContainer;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;

        public UIFactory(IStaticData staticData, IEntityContainer entityContainer)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
        }

        public GameObject CreateRootCanvas() => Object.Instantiate(_staticData.Prefabs.RootCanvasPrefab);

        public void CreateGameOverWindow(Transform root) =>
            _entityContainer.RegisterEntity(Object.Instantiate(_staticData.Prefabs.GameOverWindowPrefab, root));

        public void CreateMainMenu(Transform root) =>
            _entityContainer.RegisterEntity(Object.Instantiate(_staticData.Prefabs.MainMenuPrefab, root));
    }
}