using System.Collections;
using Code.Core.Camera;
using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Core.UI;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.CoroutineRunner;
using Code.Services.EntityContainer;
using Code.Services.Input;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private readonly IInputService _inputService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticData _staticData;

        private PermanentLevelSystem _permanentLevel;
        private RocketInputHandler _rocketInputHandler;
        private GameOverWindow _gameOverWindow;
        private LevelCamera _levelCamera;
        private Rocket _rocket;

        public GameplayState(IGameStateMachine stateMachine, IEntityContainer entityContainer, 
            IInputService inputService, ICoroutineRunner coroutineRunner, IStaticData staticData)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
            _inputService = inputService;
            _coroutineRunner = coroutineRunner;
            _staticData = staticData;
        }

        public void Enter()
        {
            SetEntities();
            _levelCamera.EnableRocketTracking();
            _rocket.OnExplode += DefineGameOver;
            _gameOverWindow.OnRetryClick += RetryGame;
            _coroutineRunner.StartCoroutine(StartRocket());
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
            _rocketInputHandler = _entityContainer.GetEntity<RocketInputHandler>();
            _rocket = _entityContainer.GetEntity<Rocket>();
            _gameOverWindow = _entityContainer.GetEntity<GameOverWindow>();
            _levelCamera = _entityContainer.GetEntity<LevelCamera>();
        }

        private IEnumerator StartRocket()
        {
            _rocket.EnableFly();
            float flyTime = 0f;
            while (flyTime < _staticData.MainConfiguration.StartRocketFlyTime)
            {
                _rocket.Move(0);
                flyTime += Time.deltaTime;
                yield return null;
            }
            _rocket.DisableFly();
            _rocket.Activate();
            _inputService.Enable();
        }

        private void DefineGameOver()
        {
            _gameOverWindow.Show();
            _levelCamera.DisableRocketTracking();
            _inputService.Disable();
        }

        private void RetryGame()
        {
            _gameOverWindow.Hide();
            _stateMachine.Enter<LoadGameState>();
        }
    }
}