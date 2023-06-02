using UnityEngine;

namespace Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Main Configuration", menuName = "Static Data/Main Configuration")]
    public class MainConfiguration : ScriptableObject
    {
        public float MaxRocketSpeed;
        [Range(1, 10)] public float ClampAngle;
    }
}