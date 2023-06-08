using System.Collections.Generic;
using Code.Core.Collectables;
using Code.Data;
using Code.Data.Enums;
using Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Code.Services.CollectableService
{
    public interface ICollectableService : IService
    {
        CollectableProgressData CollectableProgressData { get; }
        void RegisterCollectables(IEnumerable<CollectableItem> collectables);
        CollectableType Collect(Collider collectableCollider);
        void CleanUp();
        void SaveCollectedItems();
    }
}