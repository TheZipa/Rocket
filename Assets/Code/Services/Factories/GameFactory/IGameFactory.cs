using Code.Core.Environment;
using Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Code.Services.Factories.GameFactory
{
    public interface IGameFactory : IService
    {
        void CreatePermanentEnvironmentSystem(EnvironmentPart startEnvironmentPart);
        void CreateLevelCamera(Camera camera, float yPosition);
        void CreateRocket(float yPosition);
        EnvironmentPart CreateStartEnvironmentPart();
    }
}