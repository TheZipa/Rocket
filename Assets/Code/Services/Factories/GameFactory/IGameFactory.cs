using System.Collections.Generic;
using Cinemachine;
using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Infrastructure.ServiceContainer;

namespace Code.Services.Factories.GameFactory
{
    public interface IGameFactory : IService
    {
        List<EnvironmentPart> CreateEnvironmentParts();
        Rocket CreateRocket();
        CinemachineVirtualCamera CreatePlayerVirtualCamera();
        EnvironmentPart CreateStartEnvironmentPart();
    }
}