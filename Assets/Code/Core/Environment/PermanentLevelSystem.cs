using System;
using System.Collections.Generic;
using System.Linq;
using Code.Services.Factories.UIFactory;

namespace Code.Core.Environment
{
    public class PermanentLevelSystem : IDisposable
    {
        private readonly IGameFactory _gameFactory;
        private readonly Random _random = new Random();
        private List<EnvironmentPart> _activeParts;
        private EnvironmentPart _startPart;
        private EnvironmentPart _lastPart;

        private const float RocketDistance = 20;
        private Rocket.Rocket _rocket;

        public PermanentLevelSystem(IGameFactory gameFactory) => _gameFactory = gameFactory;

        public void SetStarterParts()
        {
            _lastPart = _startPart = _gameFactory.CreateStartEnvironmentPart();
            _activeParts = _gameFactory.CreateEnvironmentParts();
            ReplaceLevelPartsRandomly();
        }

        public void SetRocketView(Rocket.Rocket rocket)
        {
            _rocket = rocket;
            _rocket.OnUpdate += CompareRocketDistance;
        }
        
        public void Dispose() => _rocket.OnUpdate -= CompareRocketDistance;

        private void CompareRocketDistance()
        {
            if (!(_rocket.transform.position.y > _lastPart.EndPosition.position.y - RocketDistance)) return;
            
            if(_lastPart == _startPart)
                ReplaceLevelPartsRandomly();
            else
                PlacePartToEnd(_startPart);
        }

        private void ReplaceLevelPartsRandomly()
        {
            _activeParts = _activeParts.OrderBy(p => _random.Next()).ToList();
            foreach (EnvironmentPart part in _activeParts) 
                PlacePartToEnd(part);
        }

        private void PlacePartToEnd(EnvironmentPart part)
        {
            part.transform.position = _lastPart.EndPosition.position - part.BeginPosition.localPosition;
            _lastPart = part;
        }
    }
}