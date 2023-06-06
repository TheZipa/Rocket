using UnityEngine;

namespace Code.Core.Environment.Obstacles
{
    public class PathMovePlatformObstacle : LevelObstacle
    {
        [SerializeField] private Vector3[] _positions;
        [SerializeField] private float _speed;

        private int _targetIndex;

        public override void Activate() => _targetIndex = 0;

        private void Update()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _positions[_targetIndex], Time.deltaTime * _speed);
            if(Vector3.Distance(transform.localPosition, _positions[_targetIndex]) < 0.05f)
                SetNextPosition();
        }

        private void SetNextPosition() =>
            _targetIndex = _targetIndex == _positions.Length - 1 ? 0 : _targetIndex + 1;
    }
}