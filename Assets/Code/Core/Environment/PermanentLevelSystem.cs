using System;
using System.Collections.Generic;
using System.Linq;
using Code.Services.EntityContainer;
using Code.Services.Factories.GameFactory;

namespace Code.Core.Environment
{
    public class PermanentLevelSystem : IFactoryEntity, IDisposable
    {
        private readonly Queue<EnvironmentPart> _activeParts;
        private readonly IGameFactory _gameFactory;
        private readonly EnvironmentPart[] _levelParts;
        private readonly Random _random = new Random();
        private readonly Rocket.Rocket _rocket;
        private readonly float _partReplaceDistance;

        private EnvironmentPart _lastPart;

        public PermanentLevelSystem(EnvironmentPart[] levelParts, EnvironmentPart startPart, 
            Rocket.Rocket rocket, float partReplaceDistance, int maxActiveParts)
        {
            _partReplaceDistance = partReplaceDistance;
            _activeParts = new Queue<EnvironmentPart>(maxActiveParts);
            _levelParts = levelParts.OrderBy(l => _random.Next()).ToArray();
            _rocket = rocket;
            _rocket.OnUpdate += CompareRocketDistance;
            _lastPart = startPart;
            SetStarterParts(maxActiveParts);
        }

        public void Dispose() => _rocket.OnUpdate -= CompareRocketDistance;

        private void CompareRocketDistance()
        {
            if (!(_rocket.transform.position.y > _lastPart.EndPosition.position.y - _partReplaceDistance)) return;
            PlacePartToEnd(GetUniqueRandomPart());
            _activeParts.Dequeue().Disable();
        }

        private void SetStarterParts(int maxActiveParts)
        {
            for (int i = 0; i < maxActiveParts; i++) 
                PlacePartToEnd(GetUniqueRandomPart());
        }

        private void PlacePartToEnd(EnvironmentPart part)
        {
            part.transform.position = _lastPart.EndPosition.position - part.BeginPosition.localPosition;
            _activeParts.Enqueue(part);
            _lastPart = part;
            part.Enable();
        }

        private EnvironmentPart GetUniqueRandomPart()
        {
            EnvironmentPart randomPart = GetRandomPart();
            while(_activeParts.Contains(randomPart)) randomPart = GetRandomPart();
            return randomPart;
        }

        private EnvironmentPart GetRandomPart() => _levelParts[_random.Next(0, _levelParts.Length)];
    }
}