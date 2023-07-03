using System;
using Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Code.Services.Input
{
    public interface IInputService : IService
    {
        event Action<float> OnDrag;
        void Enable();
        void Disable();
        event Action OnDragStart;
        event Action OnDragEnd;
        void DisableDrag();
    }
}