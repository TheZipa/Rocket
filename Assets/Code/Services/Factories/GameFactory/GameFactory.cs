using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticData _staticData;

        public GameFactory(IStaticData staticData) => _staticData = staticData;

        public List<EnvironmentPart> CreateEnvironmentParts()
        {
            EnvironmentPart[] partPrefabs = _staticData.Prefabs.EnvironmentPartPrefabs;
            List<EnvironmentPart> parts = new List<EnvironmentPart>(partPrefabs.Length);
            parts.AddRange(partPrefabs.Select(Object.Instantiate));
            return parts;
        }

        public EnvironmentPart CreateStartEnvironmentPart() =>
            Object.Instantiate(_staticData.Prefabs.StartEnvironmentPartPrefab);

        public Rocket CreateRocket()
        {
            Rocket rocket = Object.Instantiate(_staticData.Prefabs.RocketPrefab);
            rocket.Construct(_staticData.MainConfiguration.MaxRocketSpeed);
            return rocket;
        }

        public CinemachineVirtualCamera CreatePlayerVirtualCamera() =>
            Object.Instantiate(_staticData.Prefabs.PlayerVirtualCameraPrefab);
    }
}