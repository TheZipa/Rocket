using UnityEngine;

namespace Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Main Configuration", menuName = "Static Data/Main Configuration")]
    public class MainConfiguration : ScriptableObject
    {
        [Header("General")]
        public float GameOverCameraOffset;
        [Header("Environment")]
        public int MaxLevelPartsCount;
        public float EnvironmentPartReplaceDistance;
        [Header("Rocket")] 
        public float RocketLaunchTime;
        public RocketData RocketData;
    }
}