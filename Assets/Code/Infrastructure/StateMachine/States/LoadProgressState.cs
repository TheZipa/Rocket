using Code.Data.Progress;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgress _playerProgress;
        private readonly ISaveLoad _saveLoadService;

        public LoadProgressState(IGameStateMachine gameStateMachine, 
            IPersistentProgress playerProgress, ISaveLoad saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _playerProgress = playerProgress;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadGameState>();
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew() =>
            _playerProgress.Progress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
    }
}