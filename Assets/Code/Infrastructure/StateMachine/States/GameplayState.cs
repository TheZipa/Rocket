using Code.Core.Camera;
using Code.Core.Environment;
using Code.Core.MeterCounter;
using Code.Core.Rocket;
using Code.Core.UI.Gameplay;
using Code.Data.Enums;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.CollectableService;
using Code.Services.EntityContainer;
using Code.Services.Input;
using Code.Services.LoadingScreen;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private readonly ILoadingScreen _loadingScreen;
        private readonly IInputService _inputService;
        private readonly ICollectableService _collectableService;
        private readonly IStaticData _staticData;

        private PermanentLevelSystem _permanentLevel;
        private MeterCounterSystem _meterCounterSystem;
        private RocketInputHandler _rocketInputHandler;
        private GameOverWindow _gameOverWindow;
        private HudView _hudView;
        private LevelCamera _levelCamera;
        private Rocket _rocket;

        public GameplayState(IGameStateMachine stateMachine, IEntityContainer entityContainer, ILoadingScreen loadingScreen,
            IInputService inputService, ICollectableService collectableService, IStaticData staticData)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
            _loadingScreen = loadingScreen;
            _inputService = inputService;
            _collectableService = collectableService;
            _staticData = staticData;
        }

        public void Enter()
        {
            CacheEntities();
            _levelCamera.EnableRocketTracking();
            _rocket.OnExplode += DefineGameOver;
            _rocket.OnCollect += CollectItem;
            _rocket.OnFuelChanged += _hudView.SetFuel;
            _gameOverWindow.OnRetryClick += RetryGame;
            _rocket.Launch(_staticData.MainConfiguration.RocketLaunchTime, StartGameplay);
        }

        public void Exit()
        {
            _loadingScreen.Show();
            _rocketInputHandler.Dispose();
            _permanentLevel.Dispose();
            _rocket.OnExplode -= DefineGameOver;
            _rocket.OnCollect -= CollectItem;
            _rocket.OnFuelChanged -= _hudView.SetFuel;
            _gameOverWindow.OnRetryClick -= RetryGame;
            _collectableService.CleanUp();
        }

        private void CacheEntities()
        {
            _permanentLevel = _entityContainer.GetEntity<PermanentLevelSystem>();
            _meterCounterSystem = _entityContainer.GetEntity<MeterCounterSystem>();
            _rocketInputHandler = _entityContainer.GetEntity<RocketInputHandler>();
            _rocket = _entityContainer.GetEntity<Rocket>();
            _gameOverWindow = _entityContainer.GetEntity<GameOverWindow>();
            _hudView = _entityContainer.GetEntity<HudView>();
            _levelCamera = _entityContainer.GetEntity<LevelCamera>();
        }

        private void CollectItem(Collider collectedCollider)
        {
            CollectableType collectableType = _collectableService.Collect(collectedCollider);
        }

        private void StartGameplay()
        {
            _meterCounterSystem.StartCounting();
            _inputService.Enable();
        }

        private void DefineGameOver()
        {
            _meterCounterSystem.StopCounting();
            _levelCamera.DisableRocketTracking();
            _inputService.Disable();
            _gameOverWindow.SetGameResultData(_meterCounterSystem.Meters, _collectableService.CollectableProgressData.Coins,
                _meterCounterSystem.TryDefineRecord());
            _gameOverWindow.Show();
            _collectableService.SaveCollectedItems();
        }

        private void RetryGame()
        {
            _gameOverWindow.Hide();
            _stateMachine.Enter<LoadGameState>();
        }
    }
}