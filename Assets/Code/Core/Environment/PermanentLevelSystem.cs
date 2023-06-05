using System;
using System.Linq;
using Code.Services.EntityContainer;
using Code.Services.Factories.GameFactory;

namespace Code.Core.Environment
{
    public class PermanentLevelSystem : IFactoryEntity, IDisposable
    {
        private readonly IGameFactory _gameFactory;
        private readonly Random _random = new Random();
        private readonly EnvironmentPart _startPart;
        private readonly Rocket.Rocket _rocket;
        private readonly float _partReplaceDistance;

        private EnvironmentPart[] _levelParts;
        private EnvironmentPart _lastPart;

        public PermanentLevelSystem(EnvironmentPart[] levelParts, EnvironmentPart startPart, 
            Rocket.Rocket rocket, float partReplaceDistance)
        {
            _partReplaceDistance = partReplaceDistance;
            _levelParts = levelParts;
            _rocket = rocket;
            _rocket.OnUpdate += CompareRocketDistance;
            _lastPart = _startPart = startPart;
            ReplaceLevelPartsRandomly();
        }

        public void Dispose() => _rocket.OnUpdate -= CompareRocketDistance;

        private void CompareRocketDistance()
        {
            if (!(_rocket.transform.position.y > _lastPart.EndPosition.position.y - _partReplaceDistance)) return;
            
            if(_lastPart == _startPart)
                ReplaceLevelPartsRandomly();
            else
                PlacePartToEnd(_startPart);
        }

        private void ReplaceLevelPartsRandomly()
        {
            _levelParts = _levelParts.OrderBy(p => _random.Next()).ToArray();
            foreach (EnvironmentPart part in _levelParts) 
                PlacePartToEnd(part);
        }

        private void PlacePartToEnd(EnvironmentPart part)
        {
            part.transform.position = _lastPart.EndPosition.position - part.BeginPosition.localPosition;
            _lastPart = part;
        }
    }
}