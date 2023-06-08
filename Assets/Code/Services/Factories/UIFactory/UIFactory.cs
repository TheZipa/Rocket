using Code.Core.UI.Menu;
using Code.Services.EntityContainer;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _progress;

        public UIFactory(IStaticData staticData, IEntityContainer entityContainer, IPersistentProgress progress)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _progress = progress;
        }

        public GameObject CreateRootCanvas() => Object.Instantiate(_staticData.Prefabs.RootCanvasPrefab);

        public void CreateGameOverWindow(Transform root) =>
            _entityContainer.RegisterEntity(Object.Instantiate(_staticData.Prefabs.GameOverWindowPrefab, root));

        public void CreateMainMenu(Transform root)
        {
            MainMenu mainMenu = Object.Instantiate(_staticData.Prefabs.MainMenuPrefab, root);
            mainMenu.Configure(_progress.Progress.MetersRecord, _progress.Progress.Coins);
            _entityContainer.RegisterEntity(mainMenu);
        }

        public void CreateMeterCounterView(Transform root) =>
            _entityContainer.RegisterEntity(Object.Instantiate(_staticData.Prefabs.MeterCounterViewPrefab, root));
    }
}