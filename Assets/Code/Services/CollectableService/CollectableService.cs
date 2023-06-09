using System;
using System.Collections.Generic;
using Code.Core.Collectables;
using Code.Core.Rocket;
using Code.Data;
using Code.Data.Enums;
using Code.Services.EntityContainer;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Services.CollectableService
{
    public class CollectableService : ICollectableService
    {
        public CollectableProgressData CollectableProgressData { get; private set; }
        private readonly IPersistentProgress _persistentProgress;
        private readonly IEntityContainer _entityContainer;
        private readonly Dictionary<Collider, CollectableItem> _collectables = new(50);
        private readonly Dictionary<CollectableType, Action> _collectActions;

        public CollectableService(IPersistentProgress persistentProgress, IEntityContainer entityContainer)
        {
            _persistentProgress = persistentProgress;
            _entityContainer = entityContainer;
            _collectActions = new()
            {
                [CollectableType.Coin] = AddCoin,
                [CollectableType.Gas] = FillFuel
            };
        }

        public void RegisterCollectables(IEnumerable<CollectableItem> collectables)
        {
            foreach (CollectableItem collectable in collectables) 
                _collectables.Add(collectable.Collider, collectable);
        }

        public CollectableType Collect(Collider collectableCollider)
        {
            CollectableItem collectableItem = _collectables[collectableCollider];
            collectableItem.Collect();
            _collectActions[collectableItem.CollectableType].Invoke();
            return collectableItem.CollectableType;
        }

        public void SaveCollectedItems()
        {
            _persistentProgress.SetCollectables(CollectableProgressData);
            CollectableProgressData = new CollectableProgressData();
        }

        public void CleanUp()
        {
            _collectables.Clear();
            CollectableProgressData = new CollectableProgressData();
        }

        private void AddCoin()
        {
            CollectableProgressData collectableProgressData = CollectableProgressData;
            collectableProgressData.Coins++;
            CollectableProgressData = collectableProgressData;
        }

        private void FillFuel() => _entityContainer.GetEntity<Rocket>().Fuel.RestoreFuelToMax();
    }
}