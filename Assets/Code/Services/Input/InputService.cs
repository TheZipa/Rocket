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

        private Coroutine _dragRoutine;

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
            FinishDragRoutine();
            _inputActions.Main.Press.performed -= StartDrag;
            _inputActions.Main.Press.canceled -= StopDrag;
            _inputActions.Disable();
        }

        public void DisableDrag() => FinishDragRoutine();

        private void StartDrag(InputAction.CallbackContext context)
        {
            FinishDragRoutine();
            _dragRoutine = _coroutineRunner.StartCoroutine(Drag());
            OnDragStart?.Invoke();
        }

        private void StopDrag(InputAction.CallbackContext context)
        {
            FinishDragRoutine();
            OnDragEnd?.Invoke();
        }

        private IEnumerator Drag()
        {
            while (true)
            {
                yield return null;
                float x = _inputActions.Main.Screen.ReadValue<Vector2>().x;
                OnDrag?.Invoke(x - _screenHalf);
            }
        }

        private void FinishDragRoutine()
        {
            if (_dragRoutine == null) return; 
            _coroutineRunner.StopCoroutine(_dragRoutine);
            _dragRoutine = null;
        }
    }
}