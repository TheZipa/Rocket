using Code.Core.Environment.Obstacles;
using UnityEngine;

namespace Code.Core.Environment
{
    public class EnvironmentPart : MonoBehaviour
    {
        public Transform BeginPosition;
        public Transform EndPosition;

        [SerializeField] private LevelObstacle[] _obstacles;

        public void Enable()
        {
            foreach (LevelObstacle obstacle in _obstacles) 
                obstacle.Activate();
            gameObject.SetActive(true);
        }

        public void Disable() => gameObject.SetActive(false);
    }
}