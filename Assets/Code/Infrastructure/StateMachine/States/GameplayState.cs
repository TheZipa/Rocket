using System.Collections;
using Code.Core.Camera;
using Code.Core.Environment;
using Code.Core.MeterCounter;
using Code.Core.Rocket;
using Code.Core.UI.Gameplay;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.CoroutineRunner;
using Code.Services.EntityContainer;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private readonly IInputService _inputService;
        private readonly IPersistentProgress _persistentProgress;
        private readonly IStaticData _staticData;

        private PermanentLevelSystem _permanentLevel;
        private MeterCounterSystem _meterCounterSystem;
        private RocketInputHandler _rocketInputHandler;
        private GameOverWindow _gameOverWindow;
        private LevelCamera _levelCamera;
        private Rocket _rocket;

        public GameplayState(IGameStateMachine stateMachine, IEntityContainer entityContainer, 
            IInputService inputService, IPersistentProgress persistentProgress, IStaticData staticData)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
            _inputService = inputService;
            _persistentProgress = persistentProgress;
            _staticData = staticData;
        }

        public void Enter()
        {
            SetEntities();
            _levelCamera.EnableRocketTracking();
            _rocket.OnExplode += DefineGameOver;
            _gameOverWindow.OnRetryClick += RetryGame;
            _rocket.Launch(_staticData.MainConfiguration.RocketLaunchTime, StartGameplay);
        }

        public void Exit()
        {
            _rocketInputHandler.Dispose();
            _permanentLevel.Dispose();
            _rocket.OnExplode -= DefineGameOver;
            _gameOverWindow.OnRetryClick -= RetryGame;
        }

        private void SetEntities()
        {
            _permanentLevel = _entityContainer.GetEntity<PermanentLevelSystem>();
            _meterCounterSystem = _entityContainer.GetEntity<MeterCounterSystem>();
            _rocketInputHandler = _entityContainer.GetEntity<RocketInputHandler>();
            _rocket = _entityContainer.GetEntity<Rocket>();
            _gameOverWindow = _entityContainer.GetEntity<GameOverWindow>();
            _levelCamera = _entityContainer.GetEntity<LevelCamera>();
        }

        private void StartGameplay()
        {
            _meterCounterSystem.StartCounting();
            _inputService.Enable();
        }

        private bool UpdateRecord()
        {
            if (_meterCounterSystem.Meters <= _persistentProgress.Progress.MetersRecord) return false;
            _persistentProgress.SetNewMeterRecord(_meterCounterSystem.Meters);
            return true;
        }

        private void DefineGameOver()
        {
            _meterCounterSystem.StopCounting();
            _levelCamera.DisableRocketTracking();
            _inputService.Disable();
            UpdateRecord(); 
            _gameOverWindow.SetMeters(_meterCounterSystem.Meters);
            _gameOverWindow.Show();
        }

        private void RetryGame()
        {
            _gameOverWindow.Hide();
            _stateMachine.Enter<LoadGameState>();
        }
    }
}