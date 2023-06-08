using System;
using System.Collections;
using UnityEngine;

namespace Code.Services.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake() => DontDestroyOnLoad(this);

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 1;
            StartCoroutine(FadeHide());
        }

        private IEnumerator FadeHide()
        {
            while (_canvasGroup.alpha > 0)
            {
                yield return null;
                _canvasGroup.alpha -= 0.075f;
            }
            _canvas.enabled = false;
        }
    }
}