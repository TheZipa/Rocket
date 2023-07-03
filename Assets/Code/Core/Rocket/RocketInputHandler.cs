using System;
using Code.Services.EntityContainer;
using Code.Services.Input;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class RocketInputHandler : IFactoryEntity, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly float _clampAngle;
        private readonly Rocket _rocket;

        public RocketInputHandler(IInputService inputService, Rocket rocket, float clampAngle)
        {
            _inputService = inputService;
            _rocket = rocket;
            _clampAngle = clampAngle;
            PrepareInput();
        }

        public void Dispose()
        {
            _inputService.OnDragStart -= _rocket.EnableFly;
            _inputService.OnDragEnd -= _rocket.DisableFly;
            _inputService.OnDrag -= MoveRocket;
            _inputService.Disable();
            _rocket.Fuel.OnFuelEmpty -= _inputService.DisableDrag;
        }
        
        private void PrepareInput()
        {
            _inputService.OnDragStart += _rocket.EnableFly;
            _inputService.OnDragEnd += _rocket.DisableFly;
            _inputService.OnDrag += MoveRocket;
            _rocket.Fuel.OnFuelEmpty += _inputService.DisableDrag;
        }
        
        private void MoveRocket(float screenX)
        {
            float angle = screenX * -Time.deltaTime;
            float clampedAngle = Mathf.Clamp(angle, -_clampAngle, _clampAngle);
            _rocket.Move(clampedAngle);
        }
    }
}