using System.Collections.Generic;
using Code.Core.Camera;
using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Code.Services.Factories.GameFactory
{
    public interface IGameFactory : IService
    {
        List<EnvironmentPart> CreateEnvironmentParts();
        Rocket CreateRocket();
        EnvironmentPart CreateStartEnvironmentPart();
        LevelCamera CreateLevelCamera(Camera camera, Rocket rocket);
    }
}