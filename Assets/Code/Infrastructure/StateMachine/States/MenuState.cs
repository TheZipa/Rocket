using Code.Core.UI.Menu;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.EntityContainer;
using Code.Services.LoadingScreen;

namespace Code.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private readonly ILoadingScreen _loadingScreen;

        private MainMenu _mainMenu;

        public MenuState(IGameStateMachine stateMachine, IEntityContainer entityContainer, ILoadingScreen loadingScreen)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
            _loadingScreen = loadingScreen;
        }

        public void Enter()
        {
            _loadingScreen.Hide();
            _mainMenu = _entityContainer.GetEntity<MainMenu>();
            _mainMenu.Show();
            _mainMenu.OnPlayClick += SwitchToGameplay;
        }

        public void Exit()
        {
            _mainMenu.Hide();
            _mainMenu.OnPlayClick -= SwitchToGameplay;
        }

        private void SwitchToGameplay() => _stateMachine.Enter<GameplayState>();
    }
}