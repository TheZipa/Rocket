using System;
using Code.Services.EntityContainer;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core.UI.Menu
{
    public class MainMenu : UIElement, IFactoryEntity
    {
        public event Action OnPlayClick;
        [SerializeField] private Button _playButton;

        private void Start() => _playButton.onClick.AddListener(SendPlayClick);

        private void SendPlayClick()
        {
            OnPlayClick?.Invoke();
        }

        private void OnDestroy() => _playButton.onClick.RemoveListener(SendPlayClick);
    }
}