using System.Collections.Generic;
using System.Linq;
using Code.Core.Camera;
using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Services.EntityContainer;
using Code.Services.Input;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly IInputService _inputService;

        public GameFactory(IStaticData staticData, IEntityContainer entityContainer, IInputService inputService)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _inputService = inputService;
        }

        public void CreateRocket(float yPosition)
        {
            Rocket rocket = Object.Instantiate(_staticData.Prefabs.RocketPrefab, new Vector3(0, yPosition), Quaternion.identity);
            rocket.Construct(_staticData.MainConfiguration.MaxRocketSpeed);
            _entityContainer.RegisterEntity(new RocketInputHandler(_inputService, rocket, _staticData.MainConfiguration.ClampAngle));
            _entityContainer.RegisterEntity(rocket);
        }

        public void CreatePermanentEnvironmentSystem(EnvironmentPart startEnvironmentPart) =>
            _entityContainer.RegisterEntity(new PermanentLevelSystem(CreateEnvironmentParts(), startEnvironmentPart
                ,_entityContainer.GetEntity<Rocket>(), _staticData.MainConfiguration.EnvironmentPartReplaceDistance,
                _staticData.MainConfiguration.MaxLevelPartsCount));

        public void CreateLevelCamera(Camera camera, float yPosition)
        {
            LevelCamera levelCamera = camera.gameObject.AddComponent<LevelCamera>();
            levelCamera.transform.position = new Vector3(0, yPosition, levelCamera.transform.position.z);
            levelCamera.Construct(_entityContainer.GetEntity<Rocket>(), _staticData.MainConfiguration.GameOverCameraOffset);
            _entityContainer.RegisterEntity(levelCamera);
        }

        public EnvironmentPart CreateStartEnvironmentPart() =>
            Object.Instantiate(_staticData.Prefabs.StartEnvironmentPartPrefab);

        private EnvironmentPart[] CreateEnvironmentParts()
        {
            EnvironmentPart[] partPrefabs = _staticData.Prefabs.EnvironmentPartPrefabs;
            List<EnvironmentPart> parts = new List<EnvironmentPart>(partPrefabs.Length);
            parts.AddRange(partPrefabs.Select(CreateLevelEnvironmentPart));
            return parts.ToArray();
        }

        private EnvironmentPart CreateLevelEnvironmentPart(EnvironmentPart prefab)
        {
            EnvironmentPart environmentPart = Object.Instantiate(prefab);
            environmentPart.Disable();
            return environmentPart;
        }
    }
}