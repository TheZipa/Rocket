using Code.Core.Collectables;
using Code.Core.Environment.Obstacles;
using UnityEngine;

namespace Code.Core.Environment
{
    public class EnvironmentPart : MonoBehaviour
    {
        public Transform BeginPosition;
        public Transform EndPosition;
        public CollectableItem[] CollectableItems;

        [SerializeField] private LevelObstacle[] _obstacles;

        public void Enable()
        {
            gameObject.SetActive(true);
            foreach (LevelObstacle obstacle in _obstacles) 
                obstacle.Activate();
            foreach (CollectableItem collectableItem in CollectableItems) 
                collectableItem.Refresh();
        }

        public void Disable() => gameObject.SetActive(false);
    }
}