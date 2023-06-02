using System;
using System.Collections;
using Code.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.Input
{
    public class InputService : IInputService
    {
        public event Action OnDragStart;
        public event Action OnDragEnd;
        public event Action<float> OnDrag;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly InputActions _inputActions = new InputActions();
        private readonly float _screenHalf;
        
        private bool _isDrag;
        
        public InputService(ICoroutineRunner coroutineRunner)
        {
            _screenHalf = Screen.width * 0.5f;
            _coroutineRunner = coroutineRunner;
        }

        public void Enable()
        {
            _inputActions.Enable();
            _inputActions.Main.Press.performed += StartDrag;
            _inputActions.Main.Press.canceled += StopDrag;
        }

        public void Disable()
        {
            _isDrag = false;
            _inputActions.Main.Press.performed -= StartDrag;
            _inputActions.Main.Press.canceled -= StopDrag;
            _inputActions.Disable();
        }

        private void StartDrag(InputAction.CallbackContext context)
        {
            _isDrag = true;
            _coroutineRunner.StartCoroutine(Drag());
            OnDragStart?.Invoke();
        }

        private void StopDrag(InputAction.CallbackContext context)
        {
            _isDrag = false;
            OnDragEnd?.Invoke();
        }

        private IEnumerator Drag()
        {
            while (_isDrag)
            {
                yield return null;
                float x = _inputActions.Main.Screen.ReadValue<Vector2>().x;
                OnDrag?.Invoke(x - _screenHalf);
            }
        }
    }
}