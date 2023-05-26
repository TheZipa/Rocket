using Cinemachine;
using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Core.UI;
using UnityEngine;

namespace Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Prefabs Data", menuName = "Static Data/Prefabs Data")]
    public class PrefabsData : ScriptableObject
    {
        [Header("Gameplay")]
        public EnvironmentPart[] EnvironmentPartPrefabs;
        public EnvironmentPart StartEnvironmentPartPrefab;
        public Rocket RocketPrefab;
        public CinemachineVirtualCamera PlayerVirtualCameraPrefab;
        [Header("UI")]
        public GameObject RootCanvasPrefab;
        public GameOverWindow GameOverWindowPrefab;
    }
}