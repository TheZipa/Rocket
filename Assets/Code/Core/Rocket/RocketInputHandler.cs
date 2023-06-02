using System;
using Code.Services.Input;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class RocketInputHandler : IDisposable
    {
        private readonly IInputService _inputService;
        private readonly float _clampAngle;
        private Rocket _rocket;

        public RocketInputHandler(IInputService inputService, IStaticData staticData)
        {
            _inputService = inputService;
            _clampAngle = staticData.MainConfiguration.ClampAngle;
        }

        public void ConnectRocketInput(Rocket rocket)
        {
            _rocket = rocket;
            PrepareInput();
        }

        public void Dispose()
        {
            _inputService.OnDragStart -= _rocket.EnableFly;
            _inputService.OnDragEnd -= _rocket.DisableFly;
            _inputService.OnDrag -= MoveRocket;
            _inputService.Disable();
        }
        
        private void PrepareInput()
        {
            _inputService.OnDragStart += _rocket.EnableFly;
            _inputService.OnDragEnd += _rocket.DisableFly;
            _inputService.OnDrag += MoveRocket;
            _inputService.Enable();
        }
        
        private void MoveRocket(float screenX)
        {
            float angle = screenX * -Time.deltaTime;
            float clampedAngle = Mathf.Clamp(angle, -_clampAngle, _clampAngle);
            _rocket.Move(clampedAngle);
        }
    }
}