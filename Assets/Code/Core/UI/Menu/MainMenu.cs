using System;
using Code.Services.EntityContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core.UI.Menu
{
    public class MainMenu : UIElement, IFactoryEntity
    {
        public event Action OnPlayClick;
        [SerializeField] private TextMeshProUGUI _record;
        [SerializeField] private TextMeshProUGUI _coins;
        [SerializeField] private Button _playButton;

        public void Configure(float record, int coins)
        {
            _record.text = $"Record: {record}m";
            _coins.text = $"Coins: {coins}";
        }

        private void Start() => _playButton.onClick.AddListener(SendPlayClick);

        private void SendPlayClick()
        {
            OnPlayClick?.Invoke();
        }

        private void OnDestroy() => _playButton.onClick.RemoveListener(SendPlayClick);
    }
}